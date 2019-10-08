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

namespace WpfCti
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += CtiScanPanel_Unloaded;
            btn_key.Click += Btn_key_Click;
            btn_scan.Click += Btn_scan_Click;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ScanDeviceController.Instance.Initialize();
        }

        private void CtiScanPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            //退出时断开振镜卡连接
            DisConnect();
        }
        private void DisConnect()
        {
            RuntimeScan scan = RuntimeScan.Instance;
            scan.macAddress = ScanDeviceController.Instance.GetUniqueName("SMC [192.168.250.11]");
            scan.Disconnect();
            laser_status.Fill = Brushes.Red;
            group1.IsEnabled = false;
            group2.IsEnabled = false;
            group1.Opacity = 0.5;
            group2.Opacity = 0.5;
        }


        private void Btn_key_Click(object sender, RoutedEventArgs e)
        {
            if (btn_key.IsChecked == true) //连接
            {
                RuntimeScan scan = RuntimeScan.Instance;
                scan.macAddress = ScanDeviceController.Instance.GetUniqueName("10.0.0.119");
                if (!scan.isConnected)
                {
                    if (!scan.Connect())
                    {
                        DisConnect();
                        lab_con.Content = "连接失败,请重新连接";
                        btn_key.IsChecked = false;
                    }
                    else
                    {
                        lab_con.Content = "已连接";
                        laser_status.Fill = Brushes.LimeGreen;
                        group1.IsEnabled = true;
                        group2.IsEnabled = true;
                        group1.Opacity = 1;
                        group2.Opacity = 1;
                    }
                }
            }
            else //断开连接
            {
                DisConnect();
                lab_con.Content = "已断开连接";
            }
        }
        private void Btn_scan_Click(object sender, RoutedEventArgs e)
        {
            Point sta_pos = new Point();
            Point end_pos = new Point();
            sta_pos.X = double.Parse(txt_sta_x.Text);
            sta_pos.Y = double.Parse(txt_sta_y.Text);
            end_pos.X = double.Parse(txt_end_x.Text);
            end_pos.Y = double.Parse(txt_end_y.Text);
            double power = double.Parse(txt_power.Text);
            Thread t = new Thread(() =>
            {
                CtiScanMotion.Instance.PointToPointCut(sta_pos, end_pos, power);
            });
        }
    }
}
