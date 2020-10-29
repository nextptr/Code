using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btn_logInf.Click += Btn_logInf_Click;
            btn_logErr.Click += Btn_logErr_Click;
            //Log4Helper.SetConfig();
        }

        private void Btn_logInf_Click(object sender, RoutedEventArgs e)
        {
            string msg = txt_inf.Text;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                LogHelper.wrLt(msg+"5");
            });
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                LogHelper.wrLt(msg + "10");
            });
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(600);
                LogHelper.wrLt(msg + "6");
            });
        }
        private void Btn_logErr_Click(object sender, RoutedEventArgs e)
        {
            string msg = txt_inf.Text;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(700);
                LogHelper.wrLt(msg + "7");
            });
        }
    }
}
