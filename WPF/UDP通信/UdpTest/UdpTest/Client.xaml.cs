using Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UdpTest
{
    /// <summary>
    /// Client.xaml 的交互逻辑
    /// </summary>
    public partial class Client:UserControl
    {
        _udpClient client = null;
        public Client()
        {
            InitializeComponent();
            btn_Connect.Click += Btn_Connect_Click;
            btn_Send.Click += Btn_Send_Click;
            txt_port.Text = "8001";
            txt_ip.Text = "127.0.0.1";
        }
        private void Btn_Send_Click(object sender, RoutedEventArgs e)
        {
            client.SendMsg(txt_box.Text);
            txt_box.Text = "";
        }
        private void Client_ReceiveMsgEvent(string e)
        {
            ls.Dispatcher.Invoke(new Action(() =>
            {
                ListViewItem item = new ListViewItem();
                item.Content = e;
                ls.Items.Insert(0, item);
            }));
        }
        private void Btn_Connect_Click(object sender, RoutedEventArgs e)
        {
            string str = btn_Connect.Content.ToString();
            if (str == "连接")
            {
                string ip = txt_ip.Text;
                int port = int.Parse(txt_port.Text);
                client = new _udpClient(ip, port);
                client.SendMsg("你好");
                btn_Connect.Content = "断开";
                client.Start();
                client.ReceiveMsgEvent += Client_ReceiveMsgEvent;
            }
            else
            {
                client.Stop();
                btn_Connect.Content = "连接";
            }
        }
        public void Exit()
        {
            if (client != null)
            {
                client.Exit();
            }
        }
    }
}
