using Common;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UdpTest
{
    /// <summary>
    /// Server.xaml 的交互逻辑
    /// </summary>
    public partial class Server:UserControl
    {
        _udpServer serv = null;
        public Server()
        {
            InitializeComponent();
            serv = new _udpServer();
            
            btn_Connect.Click += Btn_Connect_Click;
            btn_Send.Click += Btn_Send_Click;
        }
        private void Btn_Send_Click(object sender, RoutedEventArgs e)
        {
            serv.SendMsg(txt_box.Text);
            txt_box.Text = "";
        }
        private void Serv_ReceiveMsgEvent(string e)
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
            if (str == "开始监听")
            {
                btn_Connect.Content = "停止监听";
                lab_ip.Content = "IP:"+serv.IP_Address;//"IP:127.0.1";
                lab_port.Content = "PORT:8001";
                serv.Start();
                serv.ReceiveMsgEvent += Serv_ReceiveMsgEvent;
            }
            else
            {
                btn_Connect.Content = "开始监听";
                serv.Stop();
            }
        }
        public void Exit()
        {
            if (serv != null)
            {
                serv.Exit();
            }
        }
    }
}
