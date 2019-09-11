using AcsController;
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
    /// AcsPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AcsPanel : UserControl
    {
        public AcsPanel()
        {
            InitializeComponent();
            btn_cn.Click += Btn_cn_Click;
            btn_red.Click += Btn_red_Click;
            btn_set.Click += Btn_set_Click;
        }
        public static void Uninit()
        {
            AcsMotionControllor.Instance.Disconnect();
        }

        private void Btn_cn_Click(object sender, RoutedEventArgs e)
        {
            string ip = txt_ip.Text;
            AcsMotionControllor.Instance.ConnectIP(ip);
            if (AcsMotionControllor.Instance.IsConnect)
            {
                lab_ip.Content = "IP(已连接):";
            }
            else
            {
                lab_ip.Content = "IP(连接失败):";
            }
        }
        private void Btn_red_Click(object sender, RoutedEventArgs e)
        {
            string ver = txt_redKey.Text;
            double val = AcsMotionControllor.Instance.ReadInt(ver);
            txt_redVal.Text = val.ToString();
        }
        private void Btn_set_Click(object sender, RoutedEventArgs e)
        {
            string ver = txt_setKey.Text;
            double val = double.Parse(txt_setVal.Text);
            AcsMotionControllor.Instance.WriteVariable(ver, val);
        }
    }
}
