using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace ChildApp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CopyDataStruct
    {
        public IntPtr dwData;
        public int cbData;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }
    class WindowsApi
    {
        public const int WM_COPYDATA = 0x004A;

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            int hWnd,
            int Msg,
            int wParam,
            ref CopyDataStruct lParam);

        public static void SendMessageByProcess(string processName, string strMsg, IntPtr hwndMsg)
        {
            Console.WriteLine("in sendMessageByProcess");
            if (strMsg == null) return;
            var process = Process.GetProcessesByName(processName);
            if (process.FirstOrDefault() == null)
                return;

            Console.WriteLine("start sending");
            var hwnd = process.FirstOrDefault().MainWindowHandle;

            if (hwnd == IntPtr.Zero)
            {
                Console.WriteLine("can not sending!");
                return;
            }

            if (hwnd != IntPtr.Zero)
            {
                Console.WriteLine("is sending!");
                CopyDataStruct cds;

                cds.dwData = hwndMsg;
                cds.lpData = strMsg;

                cds.cbData = System.Text.Encoding.Default.GetBytes(strMsg).Length + 1;

                int fromWindowHandler = 0;
                Console.WriteLine(WM_COPYDATA.ToString());
                SendMessage(hwnd.ToInt32(), WM_COPYDATA, fromWindowHandler, ref cds);

            }
        }
    }
}
