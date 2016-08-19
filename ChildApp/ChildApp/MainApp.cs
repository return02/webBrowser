using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
namespace ChildApp
{
    class MainApp
    {
        private static string processName = "ChildApp";
        [STAThread]
        public static void Main(string[] args)
        {
            var tmpProcess = Process.GetProcessesByName(processName);
            if (tmpProcess.Length > 1) {
                MessageBox.Show("Child process is running!");
                return;
            }
            App app = new App();
            app.InitializeComponent();
            app.Run();
           
        }
    }
}
