using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLifeGame
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public Cells cells;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Loaded += MainWindow_Loaded;
            btn_Start_stop.Click += Btn_Start_stop_Click;
            btn_ReStart.Click += Btn_ReStart_Click;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitCells(50);
            InitBinding(50);
        }

        private void Btn_ReStart_Click(object sender, RoutedEventArgs e)
        {
            cells.Stop();
            cells.ReSet();
            btn_Start_stop.Content = "启动";
        }

        private void Btn_Start_stop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "启动")
            {
                btn.Content = "暂停";
                cells.Start();
            }
            else
            {
                btn.Content = "启动";
                cells.Stop();
            }
        }

        public void InitCells(int count)
        {
            //动态生成布局器
            for (int i = 0; i < count; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength();
                gridboard.RowDefinitions.Add(rd);
            }
            for (int i = 0; i < count; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength();
                gridboard.ColumnDefinitions.Add(cd);
            }
            //初始化cell
            cells = new Cells(count);
        }

        public void InitBinding(int count)
        {
            int wid = (int)gridboard.ActualHeight/ count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    //动态绑定
                    Binding binding = new Binding();
                    binding.Source = cells.cells[i][j];
                    binding.Path = new PropertyPath("LiveNow");
                    binding.Mode = BindingMode.TwoWay;

                    ToggleButton btn = new ToggleButton();
                    btn.Style= this.FindResource("GreenToggleButton") as Style;
                    btn.ApplyTemplate();
                    btn.Height = wid;
                    btn.Width = wid;
                    btn.Tag = i.ToString() + "_" + j.ToString();
                    btn.SetBinding(ToggleButton.IsCheckedProperty, binding);

                    gridboard.Children.Add(btn);
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);
                }
            }
        }

    }
}
