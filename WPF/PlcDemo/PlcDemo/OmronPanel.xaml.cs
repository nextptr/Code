using OmronGateway;
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

namespace PlcDemo
{
    /// <summary>
    /// OmronPanel.xaml 的交互逻辑
    /// </summary>
    public partial class OmronPanel : UserControl
    {
        OmronGatewayWrapper _og;
        bool tog = false;

        public OmronPanel()
        {
            InitializeComponent();
            btn_cn.Click += Btn_cn_Click;
            btn_red.Click += Btn_red_Click;
            btn_set.Click += Btn_set_Click;
            btn_setVal.Click += Btn_setVal_Click;
        }
        private void Btn_setVal_Click(object sender, RoutedEventArgs e)
        {
            if (btn_setVal.IsChecked == true)
            {
                tog = true;
                btn_setVal.Content = "1";
            }
            else
            {
                tog = false;
                btn_setVal.Content = "0";
            }
        }
        private void Btn_cn_Click(object sender, RoutedEventArgs e)
        {
            string ip = txt_ip.Text;
            _og = new OmronGatewayWrapper(ip);
            _og.Connect();
            if (_og.IsConnected == true)
            {
                lab_ip.Content = "IP(已连接)";
            }
            else
            {
                lab_ip.Content = "IP(连接失败)";
            }
        }
        private void Btn_red_Click(object sender, RoutedEventArgs e)
        {
            string ver = txt_redKey.Text;
            txt_redVal.Text = _og.ReadVariable(ver).ToString();
        }
        private void Btn_set_Click(object sender, RoutedEventArgs e)
        {
            string ver = txt_setKey.Text;
            _og.WriteVariable(ver, tog);
        }
    }
}
