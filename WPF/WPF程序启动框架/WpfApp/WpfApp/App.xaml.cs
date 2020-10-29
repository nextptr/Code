using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();


            //进程名称被修改
            Process processes = Process.GetCurrentProcess();
            if (processes.ProcessName != assembly.Name)
            {
                MessageBox.Show("错误:进程名称和程序集名称不统一!");
                System.Environment.Exit(0);
            }

            //进程重复启动
            Process[] myProcesses = Process.GetProcessesByName(assembly.Name);//获取指定的进程名
            if (myProcesses.Length > 1)
            {
                MessageBox.Show("错误:程序已经启动!");
                System.Environment.Exit(0);
            }

            //程序运行中
            bool createNew = false;
            Semaphore singleInstanceWatcher = new Semaphore(1, 1, assembly.Name, out createNew);
            if (!createNew)
            {
                MessageBox.Show("错误:程序运行中!");
                System.Environment.Exit(0);
            }

            //parameter.Init();//parameter数据加载
            bool checkUser = true;  //用户登陆和密码验证
            if (checkUser)
            {
                if (File.Exists(@"C:\Runtime"))
                {
                    //_initHardware(); //初始化硬件
                    //
                }
            }
            
        }
    }
}
