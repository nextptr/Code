using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static readonly ILog loger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            loger.Info("==StartUp==============================>>>");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            loger.Info("<<<===================================End==");
            base.OnExit(e);
        }

    }
}
