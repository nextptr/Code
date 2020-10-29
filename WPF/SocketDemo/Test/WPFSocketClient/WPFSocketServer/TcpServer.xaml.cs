using Bus.Sockets;
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

namespace WPFSocketServer
{
    /// <summary>
    /// TcpServer.xaml 的交互逻辑
    /// </summary>
    public partial class TcpServer : UserControl
    {
        ServerSocket serverSocket = null;
        public TcpServer()
        {
            InitializeComponent();
            btn_clear.Click += Btn_clear_Click;
            btn_cont.Click += Btn_cont_Click;
            btn_send.Click += Btn_send_Click;
        }
        private void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            string input = txt_box.Text;
            if (serverSocket != null)
            {
                serverSocket.SendMessage(input);
            }
        }

        private void Btn_cont_Click(object sender, RoutedEventArgs e)
        {
            if (btn_cont.IsChecked == true)
            {
                serverSocket = new ServerSocket(10001);

                serverSocket.AsyncDataReceive -= OnReceiveData;
                serverSocket.AsyncDataReceive += OnReceiveData;

                serverSocket.Connect();
                MSG("服务开启");
            }
            else
            {
                serverSocket.Disconnect();
                MSG("服务断开");
            }
        }

        private void OnReceiveData(object sender, string msg)
        {
            MSG(msg);
        }

        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            ls_box.Items.Clear();
        }

        private void MSG(object obj)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ls_box.Items.Insert(0, obj);
            }));
        }
    }
}
