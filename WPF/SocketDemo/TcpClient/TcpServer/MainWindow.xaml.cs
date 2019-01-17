using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using tcpSockets;

namespace TcpServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow:Window
    {
        private int port;
        static bool islisten = false;

        private tcpServerSocket mySeverSocket;
        public MainWindow()
        {
            InitializeComponent();


            btnConnect.Click += BtnConnect_Click;
            btnSend.Click += BtnSend_Click;
            btnClean.Click += BtnClean_Click;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mySeverSocket != null)
            {
                mySeverSocket.StopAcceptLoopThread();
            }
        }

        //监听
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (islisten == false)
            {
                islisten = true;
                port = int.Parse(labPort.Text);
                mySeverSocket = new tcpServerSocket();
                mySeverSocket.Connect(port);
                mySeverSocket.Event_ReceiveMsg += ReceieMsg;
                labIpAdd.Content = mySeverSocket.IP_Address;
                btnConnect.Content = "停止服务";
            }
            else
            {
                btnConnect.Content = "开启服务";
                mySeverSocket.StopAcceptLoopThread();
                islisten = false;
            }
        }
        //群发
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string dataTime = DateTime.Now.ToString("yyyy/MM/dd/HH:mm ");
            string sendstr = txtSendMessage.Text;
            TcpPocket pocket=new TcpPocket();
            mySeverSocket.SendEverSocketMessage(pocket.ConStructCommand(SocketCommand.ActMsg, SocketCommand.True, SocketCommand.IdfServer, sendstr));
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

        private void MySeverSocket_Event_ClientRegister(int count)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                labUserCount.Content = count;
            }));
        }

        private void ReceieMsg(object sender, ReceiveArgs e)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                if (e.Action == SocketCommand.ActConnected)
                {
                    int count = int.Parse(labUserCount.Content.ToString());
                    count++;
                    labUserCount.Content = count;
                }
                if (e.Action == SocketCommand.ActAbortSocket)
                {
                    int count = int.Parse(labUserCount.Content.ToString());
                    count--;
                    labUserCount.Content = count;
                }
                if (e.Action == SocketCommand.ActMsg)
                {
                    if (ListViwe.Items.Count > 200)
                    {
                        ListViwe.Items.Clear();
                    }
                    ListViewItem item = new ListViewItem();
                    item.Content = e.Message;
                    item.Background = Brushes.LawnGreen;
                    ListViwe.Items.Add(item);
                }
                //如果有其他操作也可以写在这里
            }));
        }
    }
}
