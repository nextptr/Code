using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace tcpSockets
{
    public class tcpClientSocket
    {
        public delegate void RecieveMsgEventHandler(object sender, ReceiveArgs e);//接收数据事件
        public event RecieveMsgEventHandler Event_RecieveMsg;

        protected int Port;     //端口号
        protected IPAddress Ip; //ip
        protected string UserName = "zjw";//用户名
        protected Socket clientSocket = null; //本机socket
        protected Thread workThread = null;//监听线程
        private static int BUFFSIZE = 1024;   //接受数据buffer长度
        protected bool RegisterFlag = false;  //用户注册标志
        private DispatcherTimer _time = new DispatcherTimer();

        public tcpClientSocket(string ip, int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
            _time.Tick += _time_Tick;
            _time.Interval = new TimeSpan(0, 0, 0, 0, SocketCommand.ClientHeartbeatClock);
        }
        private void _time_Tick(object sender, EventArgs e)
        {
            TcpPocket pocket = new TcpPocket();
            SendMessage(pocket.ConStructCommand(SocketCommand.ActALive, SocketCommand.True, UserName,"心跳"));
        }
        ~tcpClientSocket()
        {
            Disconnect();
        }


        public bool Connect() //连接服务器
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TcpPocket pocket = new TcpPocket();
            try
            {
                clientSocket.Connect(new IPEndPoint(Ip, Port));
                if (clientSocket.Connected)
                {
                    if (workThread != null)
                    {
                        workThread.Abort();
                        workThread = null;
                    }
                    workThread = new Thread(ConnectionThread);
                    workThread.Start(clientSocket);
                    if (clientSocket.Connected)
                    {//测试连接
                        clientSocket.Send(Encoding.UTF8.GetBytes(pocket.ConStructCommand(SocketCommand.ActConnected, SocketCommand.True, SocketCommand.IdfRemoter,"testConnect")));
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Disconnect()//断开连接
        {
            TcpPocket pocket = new TcpPocket();
            SendMessage(pocket.ConStructCommand(SocketCommand.ActAbortSocket, SocketCommand.True, UserName, "断开"));
            if (clientSocket != null)
            {
                clientSocket = null;
            }
            if (workThread != null)
            {
                workThread.Abort();
                workThread = null;
            }
        }
        private void ConnectionThread(object clientsocket) //接收数据
        {
            Socket connection = (Socket)clientsocket;
            byte[] recBufff = new byte[BUFFSIZE];
            TcpPocket pocket = new TcpPocket();
            while (connection.Connected)
            {
                try
                {
                    int receiveNumber = connection.Receive(recBufff);
                    string recStr = Encoding.UTF8.GetString(recBufff, 0, receiveNumber);
                    if (!pocket.AnalyseCommand(recStr))
                    {
                        continue;
                    }
                    if (pocket.Command == SocketCommand.ActConnected)
                    {
                        if (pocket.Result == SocketCommand.True)
                        {
                            Event_RecieveMsg(this, new ReceiveArgs(pocket.Command, pocket.Result, pocket.Identify, "服务器连接成功!"));
                           // _time.Start();
                        }
                    }
                    else if (pocket.Command == SocketCommand.ActAbortSocket)//退出
                    {
                        if (Event_RecieveMsg != null)
                        {
                            Event_RecieveMsg(this, new ReceiveArgs(pocket.Command, pocket.Result, pocket.Identify, "断开连接"));
                        }
                        break;  //跳出当前循环,线程退出
                    }
                    else if (pocket.Command == SocketCommand.ActMsg)//消息
                    {
                        if (Event_RecieveMsg != null)
                        {
                            Event_RecieveMsg(this, new ReceiveArgs(pocket.Command, pocket.Result, pocket.Identify, pocket.ConStructShowMsg()));
                        }
                    }
                }
                catch (Exception e)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            //退出
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            clientSocket = null;
            workThread = null;
        }
        private void SendMessage(string str)//发送数据
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(str));
            }
        }

        public void SendMsg(string str)//发送数据
        {
            //if (true == RegisterFlag)
            //{
            //    sendStr = SocketCommand.MSG + "_" + UserName + "_" + str;
            //    _sendMessage(sendStr);
            //}
            TcpPocket pocket=new TcpPocket();
            SendMessage(pocket.ConStructCommand(SocketCommand.ActMsg, SocketCommand.True , UserName , str));
        }
    }
}
