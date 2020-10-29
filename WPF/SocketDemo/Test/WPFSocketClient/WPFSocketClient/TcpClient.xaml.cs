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

namespace WPFSocketClient
{
    /// <summary>
    /// TcpClient.xaml 的交互逻辑
    /// </summary>
    public partial class TcpClient : UserControl
    {
        ClientSocket clientSocket = null;
        public TcpClient()
        {
            InitializeComponent();
            btn_clear.Click += Btn_clear_Click;
            btn_cont.Click += Btn_cont_Click;
            btn_send.Click += Btn_send_Click;
        }
        private void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            string input = txt_box.Text;
            if (clientSocket != null)
            {
                clientSocket.SendMessage(input);
            }
        }

        private void Btn_cont_Click(object sender, RoutedEventArgs e)
        {
            if (btn_cont.IsChecked == true)
            {
                clientSocket = new ClientSocket("127.0.0.1", 10001);

                clientSocket.OnServerClose -= ClientSocket_OnServerClose;
                clientSocket.OnServerClose += ClientSocket_OnServerClose;
                clientSocket.ReceiveData -= ClientSocket_ReceiveData;
                clientSocket.ReceiveData += ClientSocket_ReceiveData;

                if (clientSocket.Connect())
                {
                    MSG("服务器已经连接");
                }
                else
                {
                    MSG("服务器连接失败");
                    btn_cont.IsChecked = false;
                }
            }
            else
            {
                clientSocket.Disconnect();
                MSG("服务器已经断开连接");
            }
        }

        private void ClientSocket_ReceiveData(object sender, string str)
        {
            MSG(str);
        }

        private void ClientSocket_OnServerClose(object sender, string msg)
        {
            MSG("服务器关闭");
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
