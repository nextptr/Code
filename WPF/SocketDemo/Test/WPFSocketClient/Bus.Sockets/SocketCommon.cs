using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Sockets
{
    public class SocketCommon
    {
        public const int SEND_BUFSIZE = 1024 * 20;
        public const int RECV_BUFSIZE = 1024 * 20;
    }
    public delegate void ReceiveMsgHandler(object sender, string msg);
}
