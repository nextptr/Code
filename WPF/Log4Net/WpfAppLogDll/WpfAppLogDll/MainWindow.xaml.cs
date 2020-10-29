using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using threadLib;
using WpfApp;

namespace WpfAppLogDll
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
            btn_parallelTest.Click += Btn_parallelTest_Click;
        }

      

        private void Btn_logInf_Click(object sender, RoutedEventArgs e)
        {
            string msg = txt_inf.Text;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                LogHelper.wrLt(msg + "5");
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
            //string msg = txt_inf.Text;
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(700);
            //    LogHelper.wrLt(msg + "7");
            //});
            threadObj thObj = new threadObj();
            for (int i = 0; i < 5000; i++)
            {
                thObj.func();
            }
        }
        private void Btn_parallelTest_Click(object sender, RoutedEventArgs e)
        {
            LogHelper.wrLt("beg");
            Parallel.Invoke(() =>
            {
                Thread.Sleep(5000);
            },
            ()=>
            {
                Thread.Sleep(7000);
            }
            );
            LogHelper.wrLt("end");
        }
    }
}
