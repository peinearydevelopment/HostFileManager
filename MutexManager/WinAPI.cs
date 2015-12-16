﻿using System;
using System.Runtime.InteropServices;

namespace MutexManager
{

    /// <summary>
    /// A wrapper for various WinAPI functions.
    /// </summary>
    /// <remarks>
    /// This class is just a wrapper for your various WinApi functions.
    /// In this sample only the bare essentials are included.
    /// In my own WinApi class, I have all the WinApi functions that any
    /// of my applications would ever need.
    /// 
    /// From http://www.codeproject.com/KB/cs/SingleInstanceAppMutex.aspx
    /// </remarks>
    public static class WinApi
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        internal static int RegisterWindowMessage(string format, params object[] args)
        {
            var message = string.Format(format, args);
            return RegisterWindowMessage(message);
        }

        internal const int HwndBroadcast = 0xffff;
        internal const int SwShownormal = 1;

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        internal static void ShowToFront(IntPtr window)
        {
            ShowWindow(window, SwShownormal);
            SetForegroundWindow(window);
        }
    }
}