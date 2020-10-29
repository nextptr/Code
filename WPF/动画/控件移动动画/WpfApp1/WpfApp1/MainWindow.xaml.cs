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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard storyboard = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        //开始
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MotionControl.Instance.SetFrameWork(tb1, new Point(10, 10), new Point(400, 600));
            MotionControl.Instance.Start1();

            tb2.Start(new Point(10, 10), new Point(500, 600),1000);
        }

        //暂停
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MotionControl.Instance.Pause();

            tb2.RePlase(new Point(500, 600), new Point(30, 200));
        }

        //继续
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            tb2.SetPose(new Point( 200, 30));
        }

        //结束
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            MotionControl.Instance.Stop();
        }
    }
}
