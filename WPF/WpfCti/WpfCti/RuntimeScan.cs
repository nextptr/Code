using Cti.Hardware.ScanDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCti
{
    public class RuntimeScan
    {
        public string _name;
        protected ScanMode scanMode;
        public string ipAddress;
        public string macAddress;
        public string _errorMsg;
        public int _index = 0;
        public bool isConnected = false;
        private ScanType _scanType;
        private double _focusHeight;
        private DocumentScanningStatus _scanningStatus = DocumentScanningStatus.NotScanning;


        private static RuntimeScan _instance = null;
        public static RuntimeScan Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RuntimeScan("Cti_uv_scan1", "SMC [192.168.250.11]");
                }
                return _instance;
            }
        }

        private RuntimeScan(string uniqueName, string ipAddress)
        {
            this._name = uniqueName;
            this.ipAddress = ipAddress;
            this.macAddress = ScanDeviceController.Instance.GetUniqueName(this.ipAddress);
            ScanDeviceController.Instance.DocumentScanningStatusChanged += Instance_DocumentScanningStatusChanged;
        }

        private void Instance_DocumentScanningStatusChanged(object sender, DocumentScanningStatusEventArgs e)
        {
            _scanningStatus = e.ScanningStatus;
            if (e.ScanningStatus == DocumentScanningStatus.Scanning)
            {

            }
            else if (e.ScanningStatus == DocumentScanningStatus.NotScanning)
            {

            }
        }
        private void ScanDevMgr_DeviceStatusChanged(object sender, DeviceStatusChangedEventArgs e)
        {
            DeviceStatusSnapshot status = ScanDeviceController.Instance.GetDeviceStatusSnapshot(macAddress);
            if (status.ConnectionStatus == ConnectionStatus.Connected)
            {
                ScanStart(GetLineScript(10, 2, 1.2, 3.0, 2.0, 5.0));
            }
        }

        public DocumentScanningStatus ScanningStatus
        {
            get
            {
                return _scanningStatus;
            }
        }
        public ScanType ScanType
        {
            set
            {
                this._scanType = value;
            }
            get
            {
                return this._scanType;
            }
        }
        public double FocusHeight
        {
            set
            {
                this._focusHeight = value;
            }
            get
            {
                return this._focusHeight;
            }
        }

     
        public void Uninit()
        {
            ScanDeviceController.Instance.ScanDeviceManager.Disconnect(this.macAddress);
        }
        public bool Connect(string ipadd)
        {
            try
            {
                ScanDeviceController.Instance.ScanDeviceManager.Connect(this.macAddress);
                return true;
            }

            catch (DeviceNotFoundException)
            {
                MessageBox.Show("Device could not be found", "错误");
                return false;
            }
            catch (DeviceFailureException ex2)
            {
                MessageBox.Show("Device failure occurred. Could not disconnect.\nMessage: " + ex2.DeviceMessage, "错误");
                return false;
            }
        }
        public bool Connect()
        {
            try
            {
                ScanDeviceController.Instance.ScanDeviceManager.Connect(this.macAddress);
                return true;
            }

            catch (DeviceNotFoundException ex1)
            {
                MessageBox.Show(ex1.ToString(), "错误");
                return false;
            }
            catch (DeviceFailureException ex2)
            {
                MessageBox.Show("Device failure occurred. Could not disconnect.\nMessage: " + ex2.DeviceMessage, "错误");
                return false;
            }
        }
        public bool IsConnect()
        {
            if (!string.IsNullOrEmpty(this.macAddress))
            {
                DeviceStatusSnapshot status = ScanDeviceController.Instance.GetDeviceStatusSnapshot(this.macAddress);
                if (status.ConnectionStatus == ConnectionStatus.Connected)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Disconnect()
        {
            try
            {
                ScanDeviceController.Instance.ScanDeviceManager.Disconnect(this.macAddress);
                return true;

            }
            catch (DeviceNotFoundException)
            {
                _errorMsg = "Device could not be found";
                return false;

            }
            catch (DeviceFailureException ex2)
            {
                _errorMsg = "Device failure occurred. Could not disconnect.\nMessage: " + ex2.DeviceMessage;
                return false;
            }

        }



        public void ScanStart(string scriptText)
        {
            ScanDeviceController.Instance.StartScanning(this.macAddress, scriptText);
        }
        public void ScanPause()
        {
            //    ScanDeviceController.Instance.ScanDocument.PauseScanning();

        }
        public void ScanStop()
        {
            //    ScanDeviceController.Instance.ScanDocument.StopScanning();
        }

        public void ScanLine(double x1, double y1, double x2, double y2, double power, double space)
        {
            ScanDeviceController.Instance.StartScanning(this.macAddress, GetLineScript(power, space, x1, y1, x2, y2));
        }
        public string GetScript(double power, double space, double x1, double y1, double x2, double y2)
        {
            string script = "--MOTF\nSetUnits(Units.Millimeters)\n" +
            "SetAngleUnits(AngleUnits.Degrees)\n" +
            "--initialize the MOTF\n" +
            "MOTF.Mode = Encoder.ExternalSingleAxis\n" +
            "MOTF.Direction = Direction.LeftToRight\n" +
            "MOTF.CalFactor = 14.5646\n" +
            "MOTF.Initialize()\n" +
            "MOTF.ResetTracking()\n" +
            "--Set Image Units\n" +
            "SetUnits(Units.Millimeters)\n" +
            "--Set Image Laser Properties\n" +
            "Laser.Power = " + power.ToString() + "\n" +
            "Laser.MarkSpeed = 200\n" +
            "Laser.JumpSpeed = 10000\n" +
            "Laser.JumpDelay = 300\n" +
            "Laser.LaserOnDelay = 50\n" +
            "Laser.LaserOffDelay = 75\n" +
            "Laser.MarkDelay = 300\n" +
            "Laser.PolyDelay = 50\n" +
            "Laser.LaserPipeLineDelay = 0\n" +
            "Laser.SetVelocityCompensation(0, 50, 1200)\n" +
            "Laser.Frequency = 80\n" +
            "Laser.DutyCycle1 = 20\n" +
            "Laser.DutyCycle2 = 20\n\n" +

            "spacing = " + space.ToString() + "\n\n" +

            "MOTF.ResetTracking()\n" +
            "MOTF.StartTracking()\n" +
            "Motf.WaitForDistance(100)\n" +
            "Image.Line3D(0, 0, 0, 70, 0, 0)\n" +

            "MOTF.ResetTracking()\n" +
            "MOTF.StartTracking()\n" +
            "Motf.WaitForDistance(spacing)\n" +
            //  "Image.Line3D(0, 0, 0," + length.ToString() + ", 0, 0)\n\n" +
            "Image.Line3D(" + x1 + ", " + y1 + ", 0, " + x2 + ", " + y2 + ", 0" + ")\n" +
            "MOTF.StopTrackingAndJump(0, 0, 0, 300)";
            return script;
        }
        public string GetLineScript(double power, double space, double x1, double y1, double x2, double y2)
        {
            string scrpt = "SetUnits(Units.Millimeters)\n"
            + "Laser.Power = " + power.ToString() + "\n"
            + "Laser.MarkSpeed = 1000\n"
            + "Laser.JumpSpeed = 2000\n"
            + "\n"
            + "Laser.Frequency = 80\n"
            + "Laser.DutyCycle1 = 50\n"
            + "Laser.DutyCycle2 = 50\n"
            + "\n"
            + "Laser.JumpDelay = 100\n"
            + "Laser.LaserOnDelay = 50\n"
            + "Laser.LaserOffDelay = 75\n"
            + "Laser.MarkDelay = 100\n"
            + "Laser.PolyDelay = 50\n"
            + "Laser.LaserPipeLineDelay = 0\n"
            + "Laser.SetVelocityCompensation(0, 50, 1200)\n"
            + "\n"
            + "Image.Line3D(" + x1 + ", " + y1 + ", 0, " + x2 + ", " + y2 + ", 0" + ")\n";
            return scrpt;


        }
        public string GetCrossScript(double power, double x1, double y1, double x2, double y2)
        {
            StringBuilder sb = new StringBuilder("SetUnits(Units.Millimeters)");
            sb.Append(" Laser.Power = 50\n");
            sb.Append("Laser.MarkSpeed = 1000\n");
            sb.Append("Laser.JumpSpeed = 2000\n");
            sb.Append("Laser.JumpDelay = 100\n");
            sb.Append("Laser.LaserOnDelay = 50\n");
            sb.Append("Laser.LaserOffDelay = 75\n");
            sb.Append("Laser.MarkDelay = 100\n");
            sb.Append("Laser.PolyDelay = 50\n");
            sb.Append("Laser.LaserPipeLineDelay = 0\n");
            sb.Append("Laser.SetVelocityCompensation(Disabled, 50, 1200)\n");

            sb.Append("Laser.WobbleOverlap = 80\n");
            sb.Append("Laser.Frequency = 40\n");
            sb.Append("Laser.DutyCycle1 = 10\n");
            sb.Append("Laser.DutyCycle2 = 10\n\n");


            sb.Append("Laser.Power =  " + power.ToString() + "\n");

            sb.Append("Laser.DutyCycle1 = 12\n");
            sb.Append("Laser.DutyCycle2 = 12\n");

            sb.Append("Image.Line3D(" + y1 + ", " + x1 + ", 0, " + y2 + ", " + x2 + ", 0" + ")\n");
            sb.Append("Image.Line3D(" + x1 + ", " + y1 + ", 0, " + x2 + ", " + y2 + ", 0" + ")\n");
            return sb.ToString();
        }

        public void SetLaserPowerPercentage(float power)
        {
            ScanDeviceController.Instance.SetLaserPowerPercentage(this.macAddress, power);
            //OmronPlcGateway.OmronGatewayWrapper1.WriteVariable()
        }

    }

    public enum ScanType
    {
        CO2 = 0,
        UV,

    }

    public enum ScanMode
    {
        Fly,
        StepByStep,

    }
}
