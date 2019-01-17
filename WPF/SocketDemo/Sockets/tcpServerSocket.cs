using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace tcpSockets
{
    public class tcpServerSocket
    {
        public delegate void RecieveMsgEventHandler(object sender, ReceiveArgs e);
        public event RecieveMsgEventHandler Event_ReceiveMsg; //接受数据事件

        private int _ip_port; //端口号
        private static Socket serverSocket = null;//监听socket
        private static Thread localThread = null; //监听线程
        private static Dictionary<IPEndPoint, Thread> threadBuf = new Dictionary<IPEndPoint, Thread>();//线程表

        private static Dictionary<IPEndPoint, Stopwatch> RemoteSocketLiveTime = new Dictionary<IPEndPoint, Stopwatch>();//远端socket连接计时器（心跳）
        private static Dictionary<IPEndPoint, Socket> RemotClientSockets = new Dictionary<IPEndPoint, Socket>();//远端socket
        private static Dictionary<IPEndPoint, string> RemotClientUser = new Dictionary<IPEndPoint, string>();   //远端用户表

        static int BUFFSIZE = 1024; //数据接收buffer
        static int LISTNCOUNT = 30; //监听连接数量

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
        public tcpServerSocket()
        {
        }

        public void Connect(int port = 10010) //绑定端口
        {
            _ip_port = port;
            RemoteSocketLiveTime.Clear(); //计时器清零
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, _ip_port));//监听所有可用ip
            serverSocket.Listen(LISTNCOUNT);
            //监听处理线程
            Thread ListenThread = new Thread(StartAcceptLoopThread);
            localThread = ListenThread;
            ListenThread.Start();
        }
        private void StartAcceptLoopThread()//监听连接
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();//接收到远端socket连接请求
                    IPEndPoint ip = (IPEndPoint)clientSocket.RemoteEndPoint; //远端socket的ip
                    RemotClientSockets[ip] = clientSocket; //添加到远端socket表
                    Stopwatch sw = new Stopwatch();
                    RemoteSocketLiveTime.Add(ip, sw);  //添加RemoteSocket的计时器
                    RemoteSocketLiveTime[ip].Restart();//启动RemoteSocket的计时器
                    Thread receiveThread = new Thread(ConnectLoopThread);
                    threadBuf[ip] = receiveThread;    //添加远端数据接收线程
                    receiveThread.Start(clientSocket);//启动远端数据接收线程
                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }
        public void StopAcceptLoopThread()//停止监听连接
        {
            TcpPocket pocket = new TcpPocket();
            SendEverSocketMessage(pocket.ConStructCommand(SocketCommand.ActAbortSocket, SocketCommand.True, SocketCommand.IdfServer, "断开")); //通知remot
            //清理remote
            foreach (var tmp in threadBuf.ToList())
            {
                if (tmp.Value != null)
                {
                    tmp.Value.Abort();
                }
            }
            RemoteSocketLiveTime.Clear();
            RemotClientSockets.Clear();
            threadBuf.Clear();
            //清理本地
            if (serverSocket != null )
            { //serverSocket在localThread线程中循环阻塞式的等待socket连接，如果serverSocket没有close()
              //localThread就无法退出,localThread.abort()函数会卡死
                serverSocket.Close();
            }
            serverSocket = null;
            if (localThread != null)
            {
                localThread.Abort();
            }
            localThread = null;
        }
        private void ConnectLoopThread(object clientsocket)//接收远端socket数据处理函数
        {
            Socket Connection = (Socket)clientsocket;
            IPEndPoint ip = (IPEndPoint)Connection.RemoteEndPoint;
           // bool registerFlag = false;   //注册标志
            byte[] result = new byte[BUFFSIZE];
            TcpPocket pocket = new TcpPocket();
            try
            {
                while (true)
                {
                    int receiveNumber = Connection.Receive(result);//接收数据
                    if (receiveNumber > 0)
                    {
                        string recStr = Encoding.UTF8.GetString(result, 0, receiveNumber);
                        if (!pocket.AnalyseCommand(recStr))
                        {
                            continue;
                        }

                        if (pocket.Command == SocketCommand.ActConnected)//连接
                        {
                            if (Event_ReceiveMsg != null)
                            {
                                Event_ReceiveMsg(this, new ReceiveArgs(pocket.Command, SocketCommand.True, pocket.Identify, pocket.ConStructShowMsg()));
                            }
                            Connection.Send(Encoding.UTF8.GetBytes(pocket.ConStructCommand(SocketCommand.ActConnected, SocketCommand.True, SocketCommand.IdfServer,"欢迎")));
                        }
                        else if (pocket.Command == SocketCommand.ActAbortSocket)//退出
                        {
                            if (Event_ReceiveMsg != null)
                            {
                                Event_ReceiveMsg(this, new ReceiveArgs(pocket.Command, pocket.Result, pocket.Identify, pocket.ConStructShowMsg()));
                            }
                            break;
                        }
                        else if (pocket.Command == SocketCommand.ActALive)//心跳
                        {
                            RemoteSocketLiveTime[ip].Restart();
                        }
                        //else if (recBuf[0] == SocketCommand.Register)//注册
                        //{
                        //    registerFlag = RemoteClientRegister(ip, recBuf[1]);
                        //    Connection.Send(Encoding.UTF8.GetBytes(SocketCommand.Register + "_"+ registerFlag.ToString()));
                        //}
                        //else if (recBuf[0] == SocketCommand.MSG)//消息
                        //{
                        //    if (true == registerFlag)
                        //    {
                        //        for (index = 1; index < recBuf.Length; index++)
                        //        {
                        //            receiveMsg += recBuf[index];
                        //            if ((index + 1) != recBuf.Length)
                        //            {
                        //                receiveMsg += "_";
                        //            }
                        //        }
                        //        RemoteClientMsg(receiveMsg);
                        //        if (Event_ReceiveMsg != null)
                        //        {
                        //            Event_ReceiveMsg(this, new ReceiveArgs(receiveMsg));
                        //        }
                        //    }
                        //}
                        else if (pocket.Command == SocketCommand.ActMsg)//消息
                        {
                            SendAnotherSocketMessage(ip, pocket.ConStructCommand());
                            if (Event_ReceiveMsg != null)
                            {//触发外部的事件
                                Event_ReceiveMsg(this, new ReceiveArgs(pocket.Command, pocket.Result, pocket.Identify, pocket.ConStructShowMsg()));
                            }
                        }
                    }
                    if (RemoteSocketLiveTime[ip].ElapsedMilliseconds > SocketCommand.ServerHeartbeatClock)
                    {//心跳机制
                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
            }
            //退出
            threadBuf[ip] = null;
            if (RemotClientSockets.Count!=0&&RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
            {
                RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                RemotClientSockets[ip].Close();
                RemotClientSockets[ip] = null;
            }
            if (RemoteSocketLiveTime.Count!=0&&RemoteSocketLiveTime[ip] != null)
            {
                RemoteSocketLiveTime[ip] = null;
            }
        }
        //private void DisAConnectThread(IPEndPoint ip)
        //{
        //    //清理线程
        //    if (threadBuf[ip] != null)
        //    {
        //        threadBuf[ip].Abort();
        //        threadBuf[ip] = null;
        //    }
        //    //清理socket
        //    if (RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
        //    {
        //        RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
        //        RemotClientSockets[ip].Close();
        //        RemotClientSockets[ip] = null;
        //    }
        //    if (RemoteSocketLiveTime[ip] != null)
        //    {
        //        RemoteSocketLiveTime[ip] = null;
        //    }
        //}
        //public void DisAllRemoteSocketConnect()//断开连接
        //{
        //    TcpPocket pocket = new TcpPocket();
        //    SendEverSocketMessage(pocket.ConStructCommand(SocketCommand.ActAbortSocket, SocketCommand.True, SocketCommand.IdfServer, "断开")); //通知remot
        //    //清理remote
        //    foreach (IPEndPoint ip in RemotClientSockets.Keys) 
        //    {
        //        //清理socket
        //        if (RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
        //        {
        //            RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
        //            // tmp.Disconnect(true);
        //        }
        //        RemotClientSockets[ip].Close(); //清理资源             
        //        if (threadBuf[ip] != null)//清理线程
        //        {
        //            threadBuf[ip].Abort();
        //            threadBuf[ip] = null;
        //        }
        //    }
        //    RemotClientSockets.Clear();
        //    threadBuf.Clear();
        //    //清理本地
        //    if (serverSocket != null&& serverSocket.Connected)
        //    {
        //        serverSocket.Shutdown(SocketShutdown.Both);
        //        //serverSocket.Disconnect(true);
        //        serverSocket.Close(); //清理资源
        //    }
        //    serverSocket = null;
        //    if (localThread != null)
        //    {
        //        localThread.Abort();
        //        localThread = null;
        //    }
        //}

        public void SendEverSocketMessage(string str)//发送数据
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
                    //RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                }
            }
        }
        public void SendAnotherSocketMessage(IPEndPoint localip, string str)//发送数据
        {
            TcpPocket pocket = new TcpPocket();
            foreach (IPEndPoint ip in RemotClientSockets.Keys)
            {
                try
                {
                    if (localip != ip)
                    {
                        if (RemotClientSockets[ip] != null && RemotClientSockets[ip].Connected)
                        {
                            RemotClientSockets[ip].Send(Encoding.UTF8.GetBytes(pocket.ConStructCommand(SocketCommand.ActMsg, SocketCommand.True, SocketCommand.IdfServer, str)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //RemotClientSockets[ip].Shutdown(SocketShutdown.Both);
                }
            }
        }
        public void SendExistClientMessage(string str)//发送数据
        {
            foreach (IPEndPoint ip in RemotClientUser.Keys)
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
