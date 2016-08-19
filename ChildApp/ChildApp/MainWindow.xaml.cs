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
using System.Windows.Interop;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace ChildApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Url = "https://www.google.com";
        private static double getCor(string tmp, double limit)//set ChildApp's size
        {
            double ret = 0.0;
            try
            {
                ret = Convert.ToDouble(tmp);
            }
            catch (FormatException)
            {
                return -1.0;

            }
            catch (OverflowException)
            {
                return -1.0;

            }

            if (ret < 0.0 || ret > limit)
                return -1.0;

            return ret;
        }
        private void runNavigate(string url)
        {
            try
            {
                WebContentBrowser.Source = new Uri(url);
            }
            catch (UriFormatException ex)
            {
                string tmpWebUrl = "http://" + url;
                try
                {
                    WebContentBrowser.Source = new Uri(tmpWebUrl);
                }
                catch (UriFormatException ex2)
                {
                    MessageBox.Show("The Url is invalid.");
                    this.Close();
                }
            }

        }
        private void GoToPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void GoToPage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.runNavigate(WebUrl.Text);//if fail
        }
        private void WebUrlDisplay(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            WebUrl.Text = e.Uri.OriginalString;
        }
        private void onWebUrlKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.runNavigate(WebUrl.Text);
            }

        }
        public MainWindow()
        {
            string[] args = Environment.GetCommandLineArgs();
            double tmpWidth = SystemParameters.PrimaryScreenWidth;
            double tmpHeight = SystemParameters.PrimaryScreenHeight;

            double retTop = 0.0;
            double retLeft = 0.0;


            double retWidth = tmpWidth - retLeft;
            double retHeight = tmpHeight - retTop;
            string retUrl = "https://www.google.com";
            if (args.Length > 1) {
                //execute with parameters
                for (var i = 1; i < args.Length; ++i)
                {
                    if (args[i] == "-f")//full screen
                    {
                        retTop = 0.0;
                        retLeft = 0.0;

                        retWidth = tmpWidth - retLeft;
                        retHeight = tmpHeight - retTop;
                    }
                    else if (args[i] == "-s")//set the left and top attributes
                    {
                        if (args.Length < i + 2 + 1)
                        {
                            //throw new ParametersException("The number of Parameters is not correct!");
                            MessageBox.Show("The number of Parameters is not correct!");
                            Environment.Exit(-1);
                        }
                        double tmpx = MainWindow.getCor(args[i + 1], tmpWidth);// left
                        double tmpy = MainWindow.getCor(args[i + 2], tmpHeight);//top
                        if (tmpx < 0.0)
                        {
                            //throw new ParametersException("Parameter x is not correct!");
                            MessageBox.Show("Parameter x is not correct!");
                            Environment.Exit(-1);
                        }
                        if (tmpy < 0.0)
                        {
                            //throw new ParametersException("Parameter y is not correct!");
                            MessageBox.Show("Parameter y is not correct!");
                            Environment.Exit(-1);
                        }
                        retTop = tmpx;
                        retLeft = tmpy;

                        retWidth = tmpWidth - retLeft;
                        retHeight = tmpHeight - retTop;

                        i += 2;
                    }
                    else if (args[i] == "-u")//set the initial url
                    {
                        if (args.Length < i + 2)
                        {
                            //throw new ParametersException("The number of Parameters is not correct!");
                            MessageBox.Show("The number of Parameters is not correct!");
                            Environment.Exit(-1);
                        }
                        retUrl = args[i + 1];
                        i += 1;

                    }
                    else
                    {
                        //throw new ParametersException("Parameters wrong!");
                        MessageBox.Show("Parameters wrong!");
                        Environment.Exit(-1);
                    }
                }
            }
            
            InitializeComponent();
            this.Left = retLeft;
            this.Top = retTop;
            this.Width = retWidth;
            this.Height = retHeight;
            WebUrl.Text = retUrl;
            Loaded += (o, e) =>
            {
                IntPtr Hwnd = Process.GetCurrentProcess().MainWindowHandle;
                WindowsApi.SendMessageByProcess("ParentApp", "MainWindowHandle", Hwnd);
            };//set MainWindowHandle to ParentApp
            
        }
        
    }
}
