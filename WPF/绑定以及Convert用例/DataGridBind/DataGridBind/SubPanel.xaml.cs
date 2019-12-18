using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataGridBind
{
    /// <summary>
    /// SubPanel.xaml 的交互逻辑
    /// </summary>
    public partial class SubPanel : UserControl
    {
        public ObservableCollection<MyData> CutListItem => dgvRecipeItems.ItemsSource as ObservableCollection<MyData>;

        public SubPanel()
        {
            InitializeComponent();
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            btnDeleteAll.Click += BtnDeleteAll_Click;
        }
        private void BtnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (CutListItem != null)
            {
                CutListItem.Clear();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CutListItem != null)
            {
                if (dgvRecipeItems.SelectedIndex < 0)
                {
                    MessageBox.Show("请先选中一条信息再删除", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                CutListItem.RemoveAt(dgvRecipeItems.SelectedIndex);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            GreenCuttingItem item = new GreenCuttingItem();
            item.CutMode = CuttingMode.Full;
            item.EndX = 1000.000;
            item.EndY = 1000.000;
            item.Frequency = 1000.000;
            item.JumpDelay = 1000.000;
            item.JumpSpeed = 1000.000;
            item.LaserOffDelay = 1000.000;
            item.LaserOnDelay = 1000.000;
            item.MarkDelay = 1000.000;
            item.MarkSpeed = 1000.000;
            item.OffsetX = 1000.000;
            item.OffsetY = 1000.000;
            item.PolyDelay = 1000.000;
            item.Power = 1000.000;
            item.RepeatCount = 5000;
            item.StartX = 1000.000;
            item.StartY = 1000.000;
            item.WorkSpeed = 1000.000;

            if (CutListItem != null)
            {
                CutListItem.Add(item);
            }
        }
    }
}
