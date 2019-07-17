using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MyClass _MyClass;

        public MainWindow() {
            InitializeComponent();
            _MyClass = (MyClass)base.DataContext;
        }

        private void UpdateTime_Click(object sender, RoutedEventArgs e) {
            _MyClass.Time = DateTime.Now.ToString();
            this.lable1.Content = _MyClass.Time;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e) {
            this.lable1.Content = _MyClass.Time;
        }

        private void Show_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(_MyClass.Time);
        }
    }
}
