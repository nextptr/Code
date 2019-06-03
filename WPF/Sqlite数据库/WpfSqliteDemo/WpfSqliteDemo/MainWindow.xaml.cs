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

namespace WpfSqliteDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string fullPath = Directory.GetCurrentDirectory();
            string filePath = fullPath.Remove(fullPath.Length - 10, 10);
            //SQLiteHelper.Instance.CreateDB(filePath, "sqtest.db");
            SQLiteHelper.Instance.DeleteDB(filePath, "sqtest.db");
        }
    }
}
