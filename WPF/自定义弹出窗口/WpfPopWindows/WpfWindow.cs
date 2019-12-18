using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace WpfPopWindows
{
    public static class WpfWindow
    {
        public static IntPtr FindWindow(string title)
        {
            return Win32Api.FindWindow(null, title);
        }

        public static Window FromHwnd(IntPtr hWnd)
        {
            HwndSource hs = HwndSource.FromHwnd(hWnd);
            Window wnd = (Window)hs.RootVisual;
            return wnd;
        }

        public static IntPtr GetWindowHandle(Window wnd)
        {
            WindowInteropHelper interHelper = new WindowInteropHelper(wnd);
            return interHelper.Handle;
        }
    }
}
