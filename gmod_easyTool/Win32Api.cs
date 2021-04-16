using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace gmod_easyTool
{
    public class Win32Api
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        //------------
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out System.Drawing.Point lpPoint);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(System.Drawing.Point p);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Rect lpPoint);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);


        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public static void Drawing(IntPtr intPtr)
        {

           
            IntPtr hwnd_p = GetParent(intPtr);
            Rectangle rect = new Rectangle(22,22,55,55);

                
            Graphics g = System.Drawing.Graphics.FromHwnd(hwnd_p);
            using (g)
            {
                //рисуем прямоугольник
                g.DrawRectangle(Pens.Red, rect);
            }
        }

        public static void HideFromAltTabAndTopMost(Window w)
        {
            IntPtr Handle = new WindowInteropHelper(w).Handle;
            SetWindowLong(Handle,
            GWL_EXSTYLE,
            GetWindowLong(Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }


    }
    public static class Proc
    {
        private static readonly List<string> WindowTitles = new List<string>
        {
            "gmod",
            "Garry's Mod (64)",
            "Garry's Mod (32)",
            "Garry's Mod",
        };
        public static List<Process> find(string s)
        {
            return Process.GetProcesses().Where(b => b.ProcessName.StartsWith(s) && WindowTitles.Any(title => b.MainWindowTitle.Contains(title))).ToList();
        }
        public static string GetMainWindowTitle()
        {
            string Name;
            // получаем хендел активного окна
            IntPtr hWnd = Win32Api.GetForegroundWindow();
            
            int pid;
            //получаем pid потока активного окна
            Win32Api.GetWindowThreadProcessId(hWnd, out pid);
            // ввыводим в listbox PID процесса и имя процесса
            using (Process p = Process.GetProcessById(pid))
            {
                Name = p.MainWindowTitle;
                hWnd = IntPtr.Zero;
            }
            return Name;
        }


    }
}
