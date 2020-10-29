using Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AccessDataBase
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            StartProcess();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            System.Environment.Exit(0);
        }

        private void StartProcess()
        {
            DatabaseHelper.CreateFile(Access.Instance.Dir + "\\" + Access.Instance.UserFile);
            Access.Instance.CreateTableUser();

            DatabaseHelper.CreateFile(Access.Instance.Dir + "\\" + Access.Instance.ActiveFile);
            Access.Instance.CreateTableLog();
            Access.Instance.Open();


        }
    }
}
