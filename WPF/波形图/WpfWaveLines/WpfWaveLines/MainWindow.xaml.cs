using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using System.Xml.Serialization;

namespace WpfWaveLines
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _time = new DispatcherTimer();
        private List<double> testValues = new List<double>();
        private Dictionary<int, double> testVal = new Dictionary<int, double>();
        private int index = 0;
        private int indexCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            _time.Tick += _time_Tick;
            _time.Interval = new TimeSpan(0, 0, 0, 0, 200);
            btnStart.Click += BtnStart_Click;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ReadData();
            this.Loaded -= MainWindow_Loaded;
        }

        private void _time_Tick(object sender, EventArgs e)
        {
            index++;
            index = index % indexCount;
            WaveLine.Add(testVal[index]);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (btnStart.Content.ToString() == "开始")
            {
                _time.Start();
                btnStart.Content = "暂停";

            }
            else
            {
                _time.Stop();
                btnStart.Content = "开始";
            }
        }

        public void ReadData()
        {
            string fileName = Directory.GetCurrentDirectory() + "\\testValue.xml";
            try
            {
                if (File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    XmlSerializer ser = new XmlSerializer(testValues.GetType());
                    testValues = ser.Deserialize(fs) as List<double>;
                    indexCount = testValues.Count;
                    for (int i = 0; i < indexCount; i++)
                    {
                        testVal[i] = testValues[i];
                    }
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取文件失败:" + e.Message);
            }
            return;
        }
    }
}
