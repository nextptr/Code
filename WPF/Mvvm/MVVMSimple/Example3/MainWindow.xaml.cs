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
        Employe _MyClass;
        public MainWindow() {
            InitializeComponent();
            _MyClass = (Employe)this.DataContext;
        }

        private void AddDepartment_Click(object sender, RoutedEventArgs e) {
            _MyClass.Employees.Add(this.textBox1.Text);
        }
    }
}
