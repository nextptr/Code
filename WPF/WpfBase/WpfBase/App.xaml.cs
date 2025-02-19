﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfBase.Parameter;

namespace WpfBase
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ParameterInstance.Init();
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            ParameterInstance.Uninit();
            base.OnExit(e);
        }
    }
}
