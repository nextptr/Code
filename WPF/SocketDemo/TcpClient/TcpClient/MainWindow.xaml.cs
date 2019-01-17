using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using tcpSockets;

namespace TcpClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Ip = null;
        private int Port;
        private tcpClientSocket ClientSocket;


        public MainWindow()
        {
            InitializeComponent();
            btnConnect.Click += BtnConnect_Click;
            btnClean.Click += BtnClean_Click;
            btnSend.Click += BtnSend_Click;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientSocket.Disconnect();
        }


        //连接
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (btnConnect.Content.ToString() == "连接服务器")
            {
                Ip = labIpAdd.Text;
                Port = int.Parse(labPort.Text);

                ClientSocket = new tcpClientSocket(Ip, Port);
                ClientSocket.Connect();
                ClientSocket.Event_RecieveMsg += ClientSocket_Event_RecieveMsg;
                btnConnect.Content = "断开连接";
                btnSend.IsEnabled = true;
            }
            else
            {
                btnConnect.Content = "连接服务器";
                btnSend.IsEnabled = false;
                ClientSocket.Disconnect();
            }
        }
        //注册
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
        }
        //清理
        private void BtnClean_Click(object sender, RoutedEventArgs e)
        {
            ListViwe.Items.Clear();
        }
        //发送
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ClientSocket.SendMsg(txtSendMessage.Text);
            txtSendMessage.Clear();
        }

        private void ClientSocket_Event_RecieveMsg(object sender, ReceiveArgs e)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                string str = "";
                if (e.Action == SocketCommand.ActConnected)
                {
                    str = "连接成功";
                }
                if (e.Action == SocketCommand.ActAbortSocket)
                {
                    str = "服务器断开连接";
                }
                if (e.Action == SocketCommand.ActMsg)
                {
                    str = e.Message;
                }
                if (ListViwe.Items.Count > 200)
                {
                    ListViwe.Items.Clear();
                }
                ListViewItem item = new ListViewItem();
                item.Content = str;
                item.Background = Brushes.LawnGreen;
                ListViwe.Items.Add(item);
            }));
        }
        private void ClientSocket_Event_RegisterClient(object sender, bool falg)
        {
            if (true == falg)
            {
                btnSend.IsEnabled = true;
                ShowMsg("注册成功!");
            }
            else
            {
                ShowMsg("注册失败，请重试");
            }
        }
        public void ShowMsg(string str)
        {
            Dispatcher.Invoke(new Action(delegate
            {
                ListViewItem item = new ListViewItem();
                item.Content = str;
                item.Background = Brushes.LawnGreen;
                ListViwe.Items.Add(item);

                //如果有其他操作也可以写在这里
            }));
        }
    }
}
