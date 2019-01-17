using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sockets
{
    public class SocketCommand
    {
        public static readonly string AbortSocket = "AbortSocket";
        public static readonly string IsConnect = "IsConnect";
        public static readonly string  ConnectTrue = "Connect_True";
        public static readonly string ConnectFalse = "Connect_False";
    }

    public class ReceiveArgs
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        public ReceiveArgs(string Msg)
        {
            _message = Msg;
        }
    }
}
