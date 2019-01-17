using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sockets
{
    public class ServerSocket
    {
        public delegate void RecieveMsgHandler(object sender, ReceiveArgs e);//接受数据后，需要外部函数来处理
        public event RecieveMsgHandler ReceiveHandler; //接受数据事件

        private int _ip_port; //端口号
        private static Socket serverSocket = null;//监听socket
        private static Stack<Thread> threadBuf = new Stack<Thread>();//线程表

        private static Dictionary<Socket, Stopwatch> RemoteSocketLiveTime = new Dictionary<Socket, Stopwatch>();//远端socket连接计时器（心跳）
        private static Dictionary<IPEndPoint, Socket> RemotClientSockets = new Dictionary<IPEndPoint, Socket>();//远端socket

        private static object block = new object();//锁
        static int BUFFSIZE = 1024;

        public ServerSocket(int port = 10010)
        {
            _ip_port = port;
        }

        public string IP_Address
        {
            get
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
        }
        public int IP_Port
        {
            get
            {
                return _ip_port;
            }
        }

        public void Connect() //绑定端口
        {
            RemoteSocketLiveTime.Clear();
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, _ip_port));//监听所有ip
            serverSocket.Listen(10);
            //监听处理线程
            Thread ListenThread = new Thread(ListenClientThread);
            threadBuf.Push(ListenThread);
            ListenThread.Start();
        }

        public void Disconnect()//断开连接
        {
            SendMessage("AbortSocket");
            foreach (IPEndPoint ip in RemotClientSockets.Keys)
            {
                if (RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
                {
                    RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                    // tmp.Disconnect(true);
                }
                RemotClientSockets[ip].Close();
            }

            if (serverSocket.Connected)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                //serverSocket.Disconnect(true);
            }
            serverSocket.Close();
            while (threadBuf.Count > 0)
            {
                Thread tmp = threadBuf.Pop();
                if (tmp != null)
                {
                    tmp.Abort();
                    tmp = null;
                }
            }
        }

        private void ListenClientThread()//监听连接
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();//接收到新连接sockeet
                    IPEndPoint ip = (IPEndPoint)clientSocket.RemoteEndPoint;
                    RemotClientSockets[ip] = clientSocket; //添加到RemoteSocket表
                    Stopwatch sw = new Stopwatch();//启动RemoteSocket的计时器
                    RemoteSocketLiveTime.Add(clientSocket, sw);
                    RemoteSocketLiveTime[clientSocket].Restart();
                    Thread receiveThread = new Thread(ReceiveMessage);//启动对应RemoteSocket的数据接收线程
                    threadBuf.Push(receiveThread);
                    receiveThread.Start(clientSocket);
                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        private void ReceiveMessage(object clientsocket)//接收远端socket数据处理函数
        {
            Socket Connection = (Socket)clientsocket;
            IPEndPoint ip = (IPEndPoint)Connection.RemoteEndPoint;
            byte[] result = new byte[BUFFSIZE];
            try
            {
                while (true)
                {
                    int receiveNumber = Connection.Receive(result);//接收数据s
                    if (receiveNumber > 0)
                    {
                        string recStr = Encoding.UTF8.GetString(result, 0, receiveNumber);
                        string[] recBuf = recStr.Split('_');
                        if (recBuf[0] == "AbortSocket")//
                        {
                            // dicClientSockets[ip].Shutdown(SocketShutdown.Both);
                        }
                        else if (recBuf[0] == "CheckConnect")
                        {
                            Connection.Send(Encoding.UTF8.GetBytes("CheckConnect_True"));
                            RemoteSocketLiveTime[Connection].Restart();
                        }
                        else if (ReceiveHandler != null)
                        {
                            ReceiveHandler(this, new ReceiveArgs(recStr));
                        }

                    }

                    if (RemoteSocketLiveTime[Connection].ElapsedMilliseconds > 15 * 1000)
                    {
                        ReceiveHandler(this, new ReceiveArgs("超时断开连接"));
                        RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SendMessage(string str)//发送数据
        {
            lock (block)
            {

                foreach (IPEndPoint ip in RemotClientSockets.Keys)
                {
                    try
                    {
                        if (RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
                        {
                            RemotClientSockets[ip].Send(Encoding.UTF8.GetBytes(str));
                        }
                    }
                    catch (Exception ex)
                    {
                        RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                    }

                }
            }
        }
    }
}
