using System;

namespace MSI.Keyboard.Backlight.Manager.Jobs.BacklightTaskbarDependent
{
    public struct AppBarData
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public Rect rc;
        public IntPtr lParam;
    }
}
