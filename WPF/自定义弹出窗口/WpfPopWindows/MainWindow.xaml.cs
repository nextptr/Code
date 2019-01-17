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

namespace WpfPopWindows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private PopWindow popWindow = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPop_Click(object sender, RoutedEventArgs e)
        {
            if (popWindow == null)
            {
                popWindow = new PopWindow();
                popWindow.Closed -= PopWindow_Closed;
                popWindow.Closed += PopWindow_Closed;
                popWindow.Owner = Application.Current.MainWindow;
                popWindow.Show();
            }
            else if (!popWindow.IsActive)
            {
                popWindow.WindowState = WindowState.Normal;
                popWindow.Activate();
            }
        }

        private void PopWindow_Closed(object sender, EventArgs e)
        {
            popWindow = null;
        }
    }
}
