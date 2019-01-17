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
        public Cells ActionCells;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
            cbx_Model.SelectionChanged += Cbx_Model_SelectionChanged;
            cbx_SaveCells.SelectionChanged += Cbx_SaveCells_SelectionChanged;

            btn_Start_stop.Click += Btn_Start_stop_Click;
            btn_Step.Click += Btn_Step_Click;
            btn_Save.Click += Btn_Save_Click;
            btn_Delete.Click += Btn_Delete_Click;
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = cbx_SaveCells.SelectedItem as ComboBoxItem;
            string tmp = item.Content.ToString();
            CellsParameter.Instance.DeletePattern(tmp);
            cbx_SaveCells.Items.Remove(item);
        }

        private void Cbx_SaveCells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cbx_SaveCells.SelectedItem as ComboBoxItem;
            ActionCells.Suspand();
            ActionCells.ReSet();
            string tmp = item.Content.ToString();
            foreach (var ptn in CellsParameter.Instance.patterns)
            {
                if (ptn.PatternName == tmp)
                {
                    ActionCells.SetPattern(ptn);
                    return;
                }
            }
            btn_Start_stop.Content = "启动";
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            ActionCells.Suspand();
            string saveName = txtSaveName.Text;
            if (CellsParameter.Instance.IsExitPattern(saveName))
            {
                MessageBox.Show("样式已存在请重新命名！");
                return;
            }
            CellsParameter.Instance.AddPattern(saveName, ActionCells.cells);
            cbx_SaveCells.Items.Add(saveName);
        }

        public void test()
        {
            byte a = 2;
            string str = "";
            for (int i = 0; i < 8; i++)
            {
                if ((a & (1 << i)) == (1 << i))
                {
                    str += "1";
                }
                else
                {
                    str += "0";
                }
            }
            showTxt.Text = str;
        }

        private void Btn_Step_Click(object sender, RoutedEventArgs e)
        {
            int step = 0;
            int.TryParse(txtStepBox.Text.ToString() ,out step);
            if (step <= 0)
            {
                return;
            }
            ActionCells.StepTo(step);
        }

        private void Cbx_Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cbx_Model.SelectedItem as ComboBoxItem;
            ActionCells.Suspand();
            ActionCells.ReSet();
            ActionCells.setMode(int.Parse(item.Tag.ToString()));
            btn_Start_stop.Content = "启动";
        }

        //private void modelBarButton_Checked(object sender, RoutedEventArgs e)
        //{
        //    RadioButton b = sender as RadioButton;
        //    cells.setMode(int.Parse(b.Tag.ToString()));

        //    cells.Suspand();
        //    cells.ReSet();
        //    btn_Start_stop.Content = "启动";
        //}

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ActionCells.Stop();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitCells(50);
            InitBinding(50);
        }

        private void Btn_Start_stop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == "启动")
            {
                ActionCells.Start();
                btn.Content = "暂停";
            }
            else
            {
                btn.Content = "启动";
                ActionCells.Suspand();
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
            ActionCells = new Cells(count);
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
                    binding.Source = ActionCells.cells[i][j];
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
