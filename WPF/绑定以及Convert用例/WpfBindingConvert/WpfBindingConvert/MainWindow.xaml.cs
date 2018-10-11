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

namespace WpfBindingConvert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Item item = new Item();
        public MainWindow()
        {
            InitializeComponent();
            item.Color = 1;
            TxtBox.DataContext = item;
            labTest.DataContext = item;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            item.Color++;
            item.Color = item.Color % 5;
        }

        private void BtnSub_Click(object sender, RoutedEventArgs e)
        {
            item.Color--;
            if (item.Color == -1)
            {
                item.Color = 4;
            }
        }
    }
}
