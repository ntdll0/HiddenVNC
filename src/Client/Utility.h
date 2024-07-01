#pragma once
#include <Windows.h>
#include <vector>
#include <string>
#include <iostream>
#include "Configuration.h"

namespace HVNC {

    // Global desktop list, used by EnumDesktops function
    extern std::vector<std::wstring> DesktopList;
    class WindowInformation {
    public:
        static POINT lastCoords;
        static POINT lastDimensions;
        static BOOL  isMoving;
        static HWND  windowToMove; // To prevent accidentally moving other window
        static HWND  workingWindow;
    };

    BOOL Win8Higher();
    int GetWindowZIndex(HWND hwnd);
    BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lParam);
    POINT ScreenToWindow(int sX, int sY, int wX, int wY, int wWidth, int wHeight);
    BOOL MouseClick(bool isLeft, bool isUp, int x, int y);
    int Key(bool isUp, int key);
    BOOL CALLBACK EnumDesktopsProc(LPTSTR lpszDesktop, LPARAM lParam);
    std::vector<std::wstring> EnumerateDesktops();
}