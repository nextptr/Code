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

namespace WpfWriteableBitmap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            WriteableBitmap wb = new WriteableBitmap((int)this.ActualWidth,(int)this.ActualHeight, 96, 96, PixelFormats.Bgr32, null);
            byte blue ;
            byte green;
            byte red  ;
            byte alpha;
            int strid;
            Int32Rect Rect;
            Random rd;
            rd = new Random();
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                for (int y = 0; y < wb.PixelHeight; y++)
                {
                    blue = (byte)rd.Next(0, 256);
                    green = (byte)rd.Next(0, 256);
                    red = (byte)rd.Next(0, 256);
                    alpha = (byte)rd.Next(0, 256);
                    byte[] colorData = { blue, green, red, alpha };
                    Rect = new Int32Rect(x, y, 1, 1);
                    strid = wb.PixelWidth * wb.Format.BitsPerPixel / 8;
                    wb.WritePixels(Rect, colorData, strid, 0);
                }
            }
            for (int x = 100; x <110; x++)
            {
                for (int y = 0; y < wb.PixelHeight; y++)
                {
                    blue  = (byte)21;
                    green = (byte)215;
                    red   = (byte)128;
                    alpha = (byte)111;
                    byte[] colorData = { blue, green, red, alpha };
                    Rect = new Int32Rect(x, y, 1, 1);
                    strid = wb.PixelWidth * wb.Format.BitsPerPixel / 8;
                    wb.WritePixels(Rect, colorData, strid, 0);
                }
            }
            for (int x = 0; x < wb.PixelWidth; x++)
            {
                for (int y = 100; y < 110; y++)
                {
                    blue = (byte)21;
                    green = (byte)215;
                    red = (byte)128;
                    alpha = (byte)111;
                    byte[] colorData = { blue, green, red, alpha };
                    Rect = new Int32Rect(x, y, 1, 1);
                    strid = wb.PixelWidth * wb.Format.BitsPerPixel / 8;
                    wb.WritePixels(Rect, colorData, strid, 0);
                }
            }
            imgPanel.Source = wb;
        }

    }
}
