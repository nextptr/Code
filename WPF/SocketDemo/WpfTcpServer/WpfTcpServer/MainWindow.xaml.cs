using Sockets;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTcpServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        private int port;
        IPAddress ipaddress = IPAddress.Loopback;
        static bool isliten = false;

        private ServerSocket mySeverSocket;

        public MainWindow()
        {
            InitializeComponent();
            btnConnect.Click += BtnConnect_Click;
            btnSend.Click += BtnSend_Click;
            btnClean.Click += BtnClean_Click;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mySeverSocket.Disconnect();
        }

        //监听
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (isliten == false)
            {
                isliten = true;

                ipaddress = IPAddress.Parse(labIpAdd.Content.ToString());
                port = int.Parse(labPort.Text);

                mySeverSocket = new ServerSocket(port);
                mySeverSocket.Connect();
                mySeverSocket.Event_ReceiveMsg += ShowMesg;
                mySeverSocket.Event_ClientRegister += MySeverSocket_Event_ClientRegister;
                btnConnect.Content = "关闭监听";
            }
            else
            {
                btnConnect.Content = "开始监听";
                mySeverSocket.Disconnect();
                isliten = false;
            }
        }

        private void MySeverSocket_Event_ClientRegister(int count)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                labUserCount.Content = count;
            }));
        }

        //群发
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string dataTime = DateTime.Now.ToString("yyyy/MM/dd/HH:mm ");
            string sendstr = "管理员：" + txtSendMessage.Text;
            mySeverSocket.SendMessage(sendstr);
            ListViewItem item = new ListViewItem();
            item.Content = sendstr;
            item.Background = Brushes.LawnGreen;
            ListViwe.Items.Add(item);
            txtSendMessage.Clear();
        }
        //清屏
        private void BtnClean_Click(object sender, RoutedEventArgs e)
        {
            ListViwe.Items.Clear();
        }


        private void ShowMesg(object sender, ReceiveArgs e)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                if (ListViwe.Items.Count > 200)
                {
                    ListViwe.Items.Clear();
                }
                ListViewItem item = new ListViewItem();
                item.Content = e.Message;
                item.Background = Brushes.LawnGreen;
                ListViwe.Items.Add(item);

                //如果有其他操作也可以写在这里
            }));
           
        }
    }
}
