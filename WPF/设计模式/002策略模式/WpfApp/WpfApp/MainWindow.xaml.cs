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

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private int count = 0;
        private double price = 0.0;
        private double rebate = 0.0;
        private double condin = 0.0;
        private double retn = 0.0;
        private double total = 0.0;
        private void getTxt()
        {
            int.TryParse(txt_count.Text, out count);
            double.TryParse(txt_price.Text, out price);
            int.TryParse(txt_count.Text, out count);
            int.TryParse(txt_count.Text, out count);
            int.TryParse(txt_count.Text, out count);
            int.TryParse(txt_count.Text, out count);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Button btn = sender as Button;
            switch (btn.Tag.ToString())
            {
                case "factory":

                    break;
                case "strategy":

                    break;
            }
        }
    }
}
