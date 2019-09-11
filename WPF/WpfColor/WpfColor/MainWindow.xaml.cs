using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace WpfColor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    public class BrushItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Brush brush;
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        public BrushItem(string nam,Brush bsh)
        {
            Name = nam;
            Brush = bsh;
        }
    }

    public partial class MainWindow : Window
    {
        public List<BrushItem> ls = new List<BrushItem>();
        public MainWindow()
        {
            InitializeComponent();

            PropertyInfo[] pi= typeof(Brushes).GetProperties();
            int count = pi.Length;
            double tmp = Math.Sqrt(count);
            if (tmp % 1 > 0)
            {
                tmp++;
            }
            int sz = (int)tmp;

            // 获取所有静态公共属性
            foreach (PropertyInfo item in pi)
            {
                ls.Add(new BrushItem(item.Name, (Brush)item.GetValue(null, null)));
            }

            InitCells(sz);

            InitBinding(sz, ls);

        }
        public void InitCells(int count)
        {
            //动态生成布局器
            for (int i = 0; i < count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength();
                gd.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < count; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength();
                gd.ColumnDefinitions.Add(cd);
            }
        }
        public void InitBinding(int count, List<BrushItem> ls)
        {
            double hei = (this.Height-30) / count;
            double wid = this.Width  / count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    int idx = i * count + j;
                    if (idx >= ls.Count)
                    {
                        break;
                    }

                    Button btn = new Button();
                    btn.ApplyTemplate();
                    btn.Height = hei;
                    btn.Width = wid;
                    btn.Content= ls[idx].Name;
                    btn.Background= ls[idx].Brush;
                    btn.Click += Btn_Click;

                    Label lab = new Label();
                    lab.Content = ls[idx].Name;
                    lab.Background = ls[idx].Brush;
                    lab.Width = wid;
                    lab.Height = hei;

                    gd.Children.Add(btn);
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            //if (files == null) return;
            //IDataObject data = new DataObject(DataFormats.FileDrop, files);
            //MemoryStream memo = new MemoryStream(4);
            //byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
            //memo.Write(bytes, 0, bytes.Length);
            //data.SetData("PreferredDropEffect", memo);
            //Clipboard.SetDataObject(data, false);

            //string[] file = new string[1];
            //file[0] = "d://logo.png";
            //DataObject dataObject = new DataObject();
            //dataObject.SetData(DataFormats.FileDrop, file);
            //Clipboard.SetDataObject(dataObject, true);


            //Button btn = sender as Button;
            //Clipboard.SetText(btn.Content.ToString());
            //剪切板打开失败的相关博客，https://www.cnblogs.com/guogangj/p/7465951.html


            Button btn = sender as Button;
            Clipboard.SetDataObject(btn.Content.ToString());
        }
    }
}
