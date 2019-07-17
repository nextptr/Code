using Common.ComPort;
using System.Windows;


namespace SerialPort
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComSeriaPort comSeriaPort = new ComSeriaPort();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

            connectStatus.DataContext = comSeriaPort;
            btnConnect.Click += BtnConnect_Click;
        }

     

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsVisible)
            {
                ComPort.Items.Clear();
                foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
                {
                    ComPort.Items.Add(s);
                }
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (comSeriaPort.ComConnected == true)
            {
                comSeriaPort.Close();
            }
        }



        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (btnConnect.IsChecked == true)
            {
                btnConnect.Content = "断开";
                if (comSeriaPort.ComConnected == true)
                {
                    comSeriaPort.Close();
                }
                comSeriaPort.ComBaud = ComSeriaPort.GetBaud(ComBaud.SelectedIndex);
                comSeriaPort.ComName = ComPort.SelectedItem.ToString();
                comSeriaPort.Open();
            }
            else
            {
                btnConnect.Content = "连接";
                if (comSeriaPort.ComConnected == true)
                {
                    comSeriaPort.Close();
                }
            }
        }
    }
}
