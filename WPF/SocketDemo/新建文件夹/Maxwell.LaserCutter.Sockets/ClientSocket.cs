using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sockets
{

    public class ClientSocket
    {
        protected int Port;//需要监听的本机端口号
        public IPAddress Ip; //需要连接的远程服务器ip
        protected Socket clientSocket = null;
        public bool bExitReconnect = false;

        public delegate void RecieveMsgHandler(object sender, ReceiveArgs e);//接收数据事件
        public event RecieveMsgHandler OnRecieve;
        public delegate void AbortSocketHandler(object sender, ReceiveArgs e);//socket退出事件
        public event AbortSocketHandler OnAbortSocketEvent;

        private AutoResetEvent OnCheckConnectEvent=new AutoResetEvent(true);
        Thread receiveThread = null;//监听线程
        private Thread checkConnetThread = null;
        private Thread reconnectThread = null;

        private static object block = new object();

        public ClientSocket(string ip,int port)
        {
            Ip = IPAddress.Parse(ip);
            Port = port;
        }

        ~ClientSocket()
        {
            if (clientSocket.Connected)
            {
                Disconnect();
            }
        }

        public bool IsConnect
        {
            get
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    return true;
                }
                return false;
            }
        }

        public void CheckConnectThread()
        {
            if (checkConnetThread != null)
            {
                
                checkConnetThread.Abort();
                checkConnetThread = null;
            }
            bExitReconnect = false;
            checkConnetThread = new Thread(CheckConnect);
            checkConnetThread.Start();
        }

        public void ExitCheckConnectThread()
        {
            if (checkConnetThread != null)
            {
                bExitReconnect = true;
            }
        }

        public bool Connect() //连接服务器
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(Ip, Port));
                if (clientSocket != null && clientSocket.Connected)
                {
                    if(receiveThread!=null)
                    {
                        receiveThread.Abort();
                        receiveThread = null;
                    }
                    receiveThread = new Thread(ReceiveMessage);
                    receiveThread.Start(clientSocket);
                    SendMessage("连接成功");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Disconnect()//断开连接
        {
            try
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    SendMessage("AbortSocket_" + Ip.ToString());
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();                                     
                }
                if (receiveThread != null)
                {
                    receiveThread.Abort();
                    reconnectThread = null;
                }

                if (reconnectThread != null)
                {
                    reconnectThread.Abort();
                    reconnectThread = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        public void SendMessage(string str)//发送数据到客户端
        {
            lock(block)
            {
                if (clientSocket.Connected)
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes(str));
                }
            }
        }

        private void ReceiveMessage(object clientsocket) //接收数据
        {
            Socket connection = (Socket)clientsocket;
            while (connection.Connected)
            {
                try
                {
                    byte[] recBufff = new byte[1024];
                    int receiveNumber = connection.Receive(recBufff);
                    string recStr = Encoding.UTF8.GetString(recBufff, 0, receiveNumber);
                    if(receiveNumber>0)
                    {
                        if (recStr == "AbortSocket")
                        {
                            if (OnAbortSocketEvent != null)
                            {
                                OnAbortSocketEvent(this, new ReceiveArgs(recStr + "_" + Ip.ToString()));
                            }
                        }
                        else if (recStr == "CheckConnect_True")
                        {
                            OnCheckConnectEvent.Set();
                        }
                        else
                        {
                            if (OnRecieve != null)
                            {
                                OnRecieve(this, new ReceiveArgs(recStr));
                            }
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    break;
                }
                Thread.Sleep(5);
            }
        }

        private void CheckConnect()
        {
            try
            {
                while (!bExitReconnect)
                {
                    SendMessage("CheckConnect");
                    OnCheckConnectEvent.Reset();
                    if (!OnCheckConnectEvent.WaitOne(5000))
                    {
                        if (clientSocket != null && clientSocket.Connected)
                        {
                            clientSocket.Shutdown(SocketShutdown.Both);
                           
                        }
                        if (receiveThread != null)
                        {
                            receiveThread.Abort();
                            reconnectThread = null;
                        }
                        Connect();
                    }
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Connect();
            }
        }
    }
}
