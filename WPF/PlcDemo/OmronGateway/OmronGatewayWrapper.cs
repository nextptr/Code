using OMRON.Compolet.CIPCompolet64;
using System;
using System.Collections;

namespace OmronGateway
{
    public class OmronGatewayWrapper
    {
        public const int CIPMessageTimeOutException = -2146232832; // OMRON.CIP.Messaging.CIPMessageTimeOutException

        public string[] VariableNames { get { return _compolet.VariableNames; } }
        public string[] SystemVariableNames { get { return _compolet.SystemVariableNames; } }

        public bool Active
        {
            get { return _compolet.Active; }
            set { _compolet.Active = value; }
        }

        public bool IsConnected
        {
            get { return _compolet.IsConnected; }
        }

        public string PeerAddress
        {
            get { return _compolet.PeerAddress; }
        }

        private NJCompolet _compolet;

        public OmronGatewayWrapper(string ip)
        {
            _compolet = new NJCompolet(null);

            _compolet.ConnectionType = ConnectionType.UCMM;
            _compolet.UseRoutePath = false;
            _compolet.LocalPort = 2;
            _compolet.PeerAddress = ip;
            _compolet.RoutePath = @"2%192.168.250.1\1%0";
            _compolet.UseRoutePath = false;
        }

        public object ReadVariable(string variableName)
        {
            return _compolet.ReadVariable(variableName);
        }
        public Hashtable ReadVariableMultiple(string[] variableNames)
        {
            return _compolet.ReadVariableMultiple(variableNames);
        }
        public VariableInfo GetVariableInfo(string variableName)
        {
            return _compolet.GetVariableInfo(variableName);
        }
        public string GetValueOfVariables(object val)
        {
            string valStr = string.Empty;
            if (val.GetType().IsArray)
            {
                Array valArray = val as Array;
                if (valArray.Rank == 1)
                {
                    valStr += "[";
                    foreach (object a in valArray)
                    {
                        valStr += this.GetValueString(a) + ",";
                    }
                    valStr = valStr.TrimEnd(',');
                    valStr += "]";
                }
                else if (valArray.Rank == 2)
                {
                    for (int i = 0; i <= valArray.GetUpperBound(0); i++)
                    {
                        valStr += "[";
                        for (int j = 0; j <= valArray.GetUpperBound(1); j++)
                        {
                            valStr += this.GetValueString(valArray.GetValue(i, j)) + ",";
                        }
                        valStr = valStr.TrimEnd(',');
                        valStr += "]";
                    }
                }
                else if (valArray.Rank == 3)
                {
                    for (int i = 0; i <= valArray.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j <= valArray.GetUpperBound(1); j++)
                        {
                            valStr += "[";
                            for (int z = 0; z <= valArray.GetUpperBound(2); z++)
                            {
                                valStr += this.GetValueString(valArray.GetValue(i, j, z)) + ",";
                            }
                            valStr = valStr.TrimEnd(',');
                            valStr += "]";
                        }
                    }
                }
            }
            else
            {
                valStr = this.GetValueString(val);
            }
            return valStr;
        }
        private string GetValueString(object val)
        {
            if (val is float || val is double)
            {
                return string.Format("{0:R}", val);
            }
            else
            {
                return val.ToString();
            }
        }

        public void WriteVariable(string variableName, object writeData)
        {
            _compolet.WriteVariable(variableName, writeData);
        }


        private string ByteArrayToString(byte[] ba)
        {
            if (ba.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return BitConverter.ToString(ba);
            }
        }
        private byte[] StringToByteArray(string hex)
        {
            if (hex == String.Empty)
            {
                return new Byte[0];
            }

            int byteNumber = hex.Length / 2;
            byte[] bytes = new byte[byteNumber];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
        private byte[] ObjectToByteArray(object obj)
        {
            if (obj is Array)
            {
                Array arr = obj as Array;
                Byte[] bin = new Byte[arr.Length];
                for (int i = 0; i < bin.Length; i++)
                {
                    bin[i] = Convert.ToByte(arr.GetValue(i));
                }
                return bin;
            }
            else
            {
                return new Byte[1] { Convert.ToByte(obj) };
            }
        }
        public object RemoveBrackets(string val)
        {
            object obj = string.Empty;
            if (val.IndexOf("[") >= 0)
            {
                string str = val.Trim('[', ']');
                str = str.Replace("][", ",");
                obj = str.Split(',');
            }
            else
            {
                obj = val;
            }
            return obj;
        }

        public void Connect()
        {
            _compolet.Active = true;
            if (!_compolet.IsConnected)
            {
                _compolet.Active = false;
            }
        }
    }
}
