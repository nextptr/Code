using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCti
{
    public class CtiScanMotion
    {
        private static CtiScanMotion _instance =null;
        public static CtiScanMotion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CtiScanMotion();
                }
                return _instance;
            }
        }


        public void PointToPointCut(Point start_point, Point end_point, double power)
        {
            try
            {
                //振镜是否连接
                RuntimeScan scan = RuntimeScan.Instance;
                //移动位置，开始运行脚本
                Point[] points = new Point[2];

                scan.ScanStart(GetScript(start_point, end_point, power));
                while (ScanDeviceController.Instance.ScriptIsWork)
                {
                    Thread.Sleep(100);
                }
            }
            catch
            {
                MessageBox.Show("Cti振镜打标失败");
            }

        }
        private string GetScript(Point sta, Point end, double power)
        {
            string script = string.Empty;
            script += GetScriptGeneral(power);
            script += "\n";
            script += GetScriptQuality();
            script += "\n";

            script += "Image.Line3D(" +
                             sta.X.ToString("#0.00000") + ", " +
                             sta.Y.ToString("#0.00000") + ", 0, " +
                             end.X.ToString("#0.00000") + ", " +
                             end.Y.ToString("#0.00000") + ", 0" +
                             ")\n";
            script += "\n";
            return script;
        }

        private string GetScriptGeneral(double power)
        {
            string script = string.Empty;
            double MarkSpeed = 5000;
            double JumpSpeed = 5000;

            script += "SetUnits(Units.Millimeters)\n";
            script += "Laser.Power = " + power + "\n";
            script += "Laser.MarkSpeed = " + MarkSpeed + "\n";
            script += "Laser.JumpSpeed = " + JumpSpeed + "\n";
            script += "\n";
            script += "Laser.Frequency = 80" + "\n";
            script += "Laser.DutyCycle1 = " + power + "\n";
            script += "Laser.DutyCycle2 = " + power + "\n";

            return script;
        }
        private string GetScriptQuality()
        {
            string script = string.Empty;
            double JumpDelay = 700; //700
            double LaserOnDelay = 200; //200
            double LaserOffDelay = 200; //200
            double MarkDelay = 100;
            double PolyDelay = 100; //200
            double LaserPipelineDelay = 0;

            script += "Laser.JumpDelay = " + JumpDelay + "\n";
            script += "Laser.LaserOnDelay = " + LaserOnDelay + "\n";
            script += "Laser.LaserOffDelay = " + LaserOffDelay + "\n";
            script += "Laser.MarkDelay = " + MarkDelay + "\n";
            script += "Laser.PolyDelay = " + PolyDelay + "\n";
            script += "Laser.LaserPipeLineDelay = " + LaserPipelineDelay + "\n";
            script += "Laser.SetVelocityCompensation(0, 50, 1200)" + "\n";

            return script;
        }
    }
}
