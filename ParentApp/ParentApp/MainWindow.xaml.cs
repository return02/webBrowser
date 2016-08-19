using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using ChildApp;
namespace ParentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Args = null;
        private string ChildAppName = "ChildApp";
        private string childAppPath = "..\\..\\..\\..\\childApp\\childApp\\bin\\Debug\\ChildApp.exe";
        IntPtr MainWindowHwnd = new IntPtr(0);
        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x004A)
            {
                CopyDataStruct cds = (CopyDataStruct)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                MainWindowHwnd = cds.dwData;
                
            }
            return hwnd;
        }//get ChildApp's Handle

        void MainWindowLoaded()
        {
            (PresentationSource.FromVisual(this) as HwndSource).AddHook(new HwndSourceHook(this.WndProc));
        }//register WndProc
        public MainWindow()
        {
            InitializeComponent();
            string[] tmpArgs = Environment.GetCommandLineArgs();
            for(int i=1;i<tmpArgs.Length;++i)
            {
                Args = Args + tmpArgs[i] + " ";
            }
            Loaded += (o, e) =>
            {
                MainWindowLoaded();
            };
        }
        private void onLaunchClicked(object sender, RoutedEventArgs e)
        {
            var processName = Process.GetProcessesByName(ChildAppName);
            if (processName.FirstOrDefault() == null)
            {
                try
                {
                    Process.Start(childAppPath, Args);
                }
                catch (Exception)
                {
                    MessageBox.Show("ChildApp failed!");
                }
                OnRadio.IsEnabled = true;
                OffRadio.IsEnabled = true;
            }//start a new process of ChildApp
            else
            {
                NativeWindowApiHelper.ShowWindow(MainWindowHwnd, NativeWindowApiHelper.SW_RESTORE);

            }
        }
        private void onHideClicked(object sender, RoutedEventArgs e)
        {
            if(MainWindowHwnd != IntPtr.Zero)
                NativeWindowApiHelper.ShowWindow(MainWindowHwnd, NativeWindowApiHelper.SW_MINIMIZE);
        }
        private void onOnClicked(object sender, RoutedEventArgs e)//set TopMost
        {
            if(MainWindowHwnd != IntPtr.Zero)
                NativeWindowApiHelper.SetWindowPos(MainWindowHwnd, NativeWindowApiHelper.HWND_TOPMOST, 1, 1, 1, 1, NativeWindowApiHelper.SWP_NOMOVE | NativeWindowApiHelper.SWP_NOSIZE);
        }
        private void onOffClicked(object sender, RoutedEventArgs e)//cancel TopMost
        {
            if (MainWindowHwnd != IntPtr.Zero)
                NativeWindowApiHelper.SetWindowPos(MainWindowHwnd, NativeWindowApiHelper.HWND_NOTOPMOST, 1, 1, 1, 1, NativeWindowApiHelper.SWP_NOMOVE | NativeWindowApiHelper.SWP_NOSIZE);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var processes = Process.GetProcessesByName(ChildAppName);
            if(processes.FirstOrDefault() != null)
            {
                foreach(var process in processes)
                {
                    process.Kill();
                }
            }
            base.OnClosing(e);
        }
    }
}