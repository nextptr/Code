using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

namespace CsBase.Common
{
    public class Parameters
    {
        private static Parameters _instance = null;
        public static Parameters Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new Parameters();
                }
                return _instance;
            }
        }


        private Parameters()
        {
            string[] parameterNames = new string[]
            {
                "Maxwell.LaserCutter.Parameter.AreascanCamera.AreaScanCameraParameter",
                "Maxwell.LaserCutter.Parameter.Maintance.AxisParameter",
                "Maxwell.LaserCutter.Parameter.ScanDevice.ScanDeviceParameter"
                //"Maxwell.LaserCutter.Parameter.Maintance.TeachParameter",
                //"Maxwell.LaserCutter.Parameter.Maintance.CuttingTestParameter"
            };
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;  //获取当前的程序集 Maxwell.LaserCutter.Parameter
            _parameters = new List<IParameter>();         //IParameter是参数管理类的基类，使用基类的集合来管理所有派生的参数类
            for (int i = 0; i < parameterNames.Length; ++i)
            {
                IParameter p = CreateInstance(assemblyName, parameterNames[i]);
                _parameters.Add(p);
            }
        }
        private List<IParameter> _parameters;
        private IParameter CreateInstance(string assemblyName, string parameterName)
        {
            //assemblyName(当前的程序集),parameterName(派生出的参数类名,包含程序集及类名)
            //CreateInstance() //类似工厂模式返回对应的实例对象
            ObjectHandle handle = Activator.CreateInstance(assemblyName, parameterName);
            return handle.Unwrap() as IParameter;
        }

        public void Read()
        {
            for (int i = 0; i < _parameters.Count; ++i)
            {
                _parameters[i].Read();
            }
        }
        public void Write()
        {
            for (int i = 0; i < _parameters.Count; ++i)
            {
                _parameters[i].Write();
            }
        }
        protected T Get<T>() where T : IParameter
        {
            for (int i = 0; i < _parameters.Count; ++i)
            {
                IParameter p = _parameters[i];
                if (p is T)
                {
                    return (T)p;
                }
            }
            return null;
        }
    }
}
