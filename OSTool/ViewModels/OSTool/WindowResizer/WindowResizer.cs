using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace OSTool
{
    public class WindowResizer
    {
        #region Private

        private Window wWindow;
        private IntPtr wLastScreen;
        private Matrix wTransformToDevice;
        private Rect wScreenSize = new Rect();

        #endregion

        #region Dll Imports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out MonPoint lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MonInfo lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(MonPoint pt, MonOptions dwFlags);

        #endregion

        public WindowResizer(Window window)
        {
            wWindow = window;
            GetTransform();

            wWindow.SourceInitialized += (sender, e) => 
            {
                var handle = new WindowInteropHelper(wWindow).Handle;
                var handleSource = HwndSource.FromHwnd(handle);

                if (handleSource == null)
                    return;

                handleSource.AddHook(WindowProc);
            };
        }

        #region Windows Proc

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    MinMaxInfo(lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        #endregion

        #region Voids

        private void GetTransform()
        {
            var source = PresentationSource.FromVisual(wWindow);

            wTransformToDevice = default(Matrix);

            if (source == null)
                return;

            wTransformToDevice = source.CompositionTarget.TransformToDevice;
        }

        private void MinMaxInfo(IntPtr lParam)
        {
            GetCursorPos(out MonPoint lMousePosition);
            var primaryinfo = new MonInfo();

            var primary = MonitorFromPoint(new MonPoint(0, 0), MonOptions.Primary);
            var current = MonitorFromPoint(lMousePosition, MonOptions.Nearest);

            if (GetMonitorInfo(primary, primaryinfo) == false)
                return;

            if (current != wLastScreen || wTransformToDevice == default(Matrix))
                GetTransform();

            wLastScreen = current;

            var minmax = (MinMax)Marshal.PtrToStructure(lParam, typeof(MinMax));

            if (primary.Equals(current) == true)
            {
                minmax.MaxPosition.X = primaryinfo.Work.Left;
                minmax.MaxPosition.Y = primaryinfo.Work.Top;
                minmax.MaxSize.X = primaryinfo.Work.Right - primaryinfo.Work.Left;
                minmax.MaxSize.Y = primaryinfo.Work.Bottom - primaryinfo.Work.Top;
            }
            else
            {
                minmax.MaxPosition.X = primaryinfo.Monitor.Left;
                minmax.MaxPosition.Y = primaryinfo.Monitor.Top;
                minmax.MaxSize.X = primaryinfo.Monitor.Right - primaryinfo.Monitor.Left;
                minmax.MaxSize.Y = primaryinfo.Monitor.Bottom - primaryinfo.Monitor.Top;
            }

            var minSize = wTransformToDevice.Transform(new Point(wWindow.MinWidth, wWindow.MinHeight));

            minmax.MinTrackSize.X = (int)minSize.X;
            minmax.MinTrackSize.Y = (int)minSize.Y;

            wScreenSize = new Rect(minmax.MaxPosition.X, minmax.MaxPosition.Y, minmax.MaxSize.X, minmax.MaxSize.Y);

            Marshal.StructureToPtr(minmax, lParam, true);
        }

        #endregion
    }

    #region Structs

    enum MonOptions : uint
    {
        Default = 0x00000000,
        Primary = 0x00000001,
        Nearest = 0x00000002
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left, Top, Right, Bottom;

        public Rectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MonInfo
    {
        public int Size = Marshal.SizeOf(typeof(MonInfo));
        public Rectangle Monitor = new Rectangle();
        public Rectangle Work = new Rectangle();
        public int Flags = 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MinMax
    {
        public MonPoint Reserved;
        public MonPoint MaxSize;
        public MonPoint MaxPosition;
        public MonPoint MinTrackSize;
        public MonPoint MaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct MonPoint
    {
        public int X;
        public int Y;

        public MonPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    #endregion
}