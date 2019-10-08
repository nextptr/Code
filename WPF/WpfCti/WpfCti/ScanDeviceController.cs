using Cti.Hardware.ScanDevice;
using Cti.Hardware.ScanDevice.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCti
{
    public class ScanDeviceController
    {
        public event EventHandler<ScanDeviceGatewayFailedEventArgs> ScanDeviceGatewayFailed;
        public event EventHandler DeviceListChanged;
        public event EventHandler<DeviceStatusChangedEventArgs> DeviceStatusChanged;
        public event EventHandler<DocumentScanningStatusEventArgs> DocumentScanningStatusChanged;


        private Dictionary<string, string> _deviceNames;
        private Dictionary<string, ScanDocument> _scanDocs;
        private ScanDeviceManager _scanDevMgr;
        private bool _initialized = false;
        private bool _scriptIsWork = false;
        public bool ScriptIsWork
        {
            get
            {
                return _scriptIsWork;
            }
        }


        private static ScanDeviceController _instance = null;
        public static ScanDeviceController Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new ScanDeviceController();
                }
                return _instance;
            }
        }
        public ScanDeviceManager ScanDeviceManager { get { return _scanDevMgr; } }
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _deviceNames.Clear();

            _scanDevMgr = new ScanDeviceManager();

            _scanDevMgr.DeviceListChanged += ScanDevMgr_DeviceListChanged;
            _scanDevMgr.ScanDeviceGatewayFailed += ScanDevMgr_ScanDeviceGatewayFailed;
            _scanDevMgr.DeviceStatusChanged += ScanDevMgr_DeviceStatusChanged;
            _scanDevMgr.EnabledStatusCategories |= DeviceStatusCategories.ConnectionStatus | DeviceStatusCategories.ScanningStatus;

            try
            {
                _scanDevMgr.LoadConfiguration();
                _scanDevMgr.InitializeHardware();
                _initialized = true;
            }
            catch (ConfigurationLoadingException ex1)
            {
                MessageBox.Show(ex1.Message);
                //throw ex1;
            }
            catch (ScanDeviceGatewayLoadingException ex2)
            {
                MessageBox.Show(ex2.Message, "警告");
                //throw ex2;
            }
        }
        private ScanDeviceController()
        {
            _deviceNames = new Dictionary<string, string>();
            _scanDocs = new Dictionary<string, ScanDocument>();
        }


        private void ScanDevMgr_DeviceListChanged(object sender, EventArgs e)
        {
            RefreshDeviceList();
            DeviceListChanged?.Invoke(sender, e);
        }
        private void ScanDevMgr_ScanDeviceGatewayFailed(object sender, ScanDeviceGatewayFailedEventArgs e)
        {
            ScanDeviceGatewayFailed?.Invoke(sender, e);
        }
        private void ScanDevMgr_DeviceStatusChanged(object sender, DeviceStatusChangedEventArgs e)
        {
            DeviceStatusChanged?.Invoke(sender, e);
        }


        private void RefreshDeviceList()
        {
            string[] deviceList = _scanDevMgr.GetDeviceList();

            for (int i = 0; i < deviceList.Length; ++i)
            {
                string unique = deviceList[i];
                string friendly = _scanDevMgr.GetDeviceFriendlyName(unique);

                _deviceNames[unique] = friendly;
            }
        }
        public void Close()
        {
            string[] deviceList = _scanDevMgr.GetDeviceList();
            foreach (string unique in deviceList)
            {
                DeviceStatusSnapshot status = _scanDevMgr.GetDeviceStatusSnapshot(unique);
                if (status.ConnectionStatus == ConnectionStatus.Connected)
                {
                    _scanDevMgr.Disconnect(unique);
                }
            }
        }


        public string GetUniqueName(string friendly)
        {
            string unique = _deviceNames.FirstOrDefault(x => x.Value == friendly).Key;
            return unique;
        }
        public string GetFriendlyName(string unique)
        {
            return _scanDevMgr.GetDeviceFriendlyName(unique);
        }
        public string[] GetUniqueNames()
        {
            return _deviceNames.Keys.ToArray();
        }
        public string[] GetFriendlyNames()
        {
            return _deviceNames.Values.ToArray();
        }
        public ScanDocument GetScanDocument(string uniqueName)
        {
            return _scanDocs[uniqueName];
        }
        public DeviceStatusSnapshot GetDeviceStatusSnapshot(string deviceUniqueName)
        {
            return _scanDevMgr.GetDeviceStatusSnapshot(deviceUniqueName);
        }
        private ScanDocument GetScanDocument(string uniqueName, DistanceUnit unit)
        {
            if (!_scanDocs.ContainsKey(uniqueName))
            {
                try
                {
                    ScanDocument scanDoc = _scanDevMgr.CreateScanDocument(uniqueName, unit);

                    scanDoc.DocumentScanningStatusChanged += ScanDocument_ScanningStatusChanged;
                    scanDoc.ScriptMessageReceived += ScanDocument_ScriptMessageReceived;

                    _scanDocs.Add(uniqueName, scanDoc);

                    return scanDoc;
                }

                catch (DeviceNotFoundException)
                {
                    _scanDocs.Remove(uniqueName);
                    MessageBox.Show("Device could not be found", "错误");
                    return null;
                }
                catch (DeviceAlreadyInUseException)
                {
                    _scanDocs.Remove(uniqueName);
                    MessageBox.Show("Device already in use", "错误");
                    return null;
                }
            }
            else
            {
                return _scanDocs[uniqueName];
            }
        }


        public bool StartScanning(string uniqueName, string scriptText)
        {
            _scriptIsWork = true;
            return StartScanning(uniqueName, DistanceUnit.Millimeters, AddWaitScript(scriptText));
        }
        public bool StartScanning(string uniqueName, DistanceUnit unit, string scriptText)
        {
            ScanDocument scanDoc = GetScanDocument(uniqueName, unit);

            if (scanDoc != null)
            {
                try
                {
                    scanDoc.Scripts.Clear();
                    scanDoc.Scripts.Add(new ScanningScriptChunk("userScript", scriptText));
                    scanDoc.StartScanning();
                    return true;
                }
                catch (DeviceNotConnectedException)
                {
                    MessageBox.Show("Device is not connected. Please connect to the device before start making", "错误");
                    return false;
                }
                catch (DeviceCommunicationFailureException exp1)
                {
                    MessageBox.Show("Communication failure occurred.\nMessage: " + exp1.DeviceMessage, "错误");
                    return false;
                }
                catch (DeviceFailureException exp2)
                {
                    MessageBox.Show("Device failed to start marking.\nMessage: " + exp2.DeviceMessage, "错误");
                    return false;
                }
            }

            return false;
        }
        private string AddWaitScript(string scriptText)
        {
            string newScript = "";
            newScript += "Report(\"Script_On\")\n";
            newScript += "eventWait = Events.CreateWaitEvent()\n";
            newScript += scriptText;
            newScript += "eventWait.Schedule()\n";
            newScript += "eventWait.Wait()\n";
            newScript += "Report(\"Script_Off\")\n";
            return newScript;
        }


        public bool SetLaserPowerPercentage(string deviceName, float power)
        {
            try
            {
                ScanDocument scanDocument = null;
                DistanceUnit distanceUnit = DistanceUnit.Millimeters;
                scanDocument = _scanDevMgr.CreateScanDocument(deviceName, distanceUnit);

                scanDocument.PreviewInfo.Enabled = false;
                DistanceUnit scanningUnits = DistanceUnit.Millimeters;
                DistanceUnit imageDistanceUnit = scanningUnits;


                VectorImage vectorImage = scanDocument.CreateVectorImage("vectorImage1", imageDistanceUnit);
                vectorImage.VariablePolyDelayEnabled = true;
                vectorImage.SetLaserPowerPercentage(power);

            }
            catch (Exception ex)
            {
                //Database.Access.Instance.Insert(new LogItem("通信", ex.Message.ToString()));
                return false;
            }
            return true;
        }
        private void ScanDocument_ScanningStatusChanged(object sender, DocumentScanningStatusEventArgs e)
        {
            DocumentScanningStatusChanged?.Invoke(sender, e);
        }
        private void ScanDocument_ScriptMessageReceived(object sender, ScriptMessageEventArgs e)
        {
            // MessageBox.Show("message=" + e.ScriptMessage.ToString(), "消息");
            if (e.ScriptMessage.ToString() == "Script_On")
            {
                _scriptIsWork = true;
            }
            if (e.ScriptMessage.ToString() == "Script_Off")
            {
                _scriptIsWork = false;
            }
        }
    }
}
