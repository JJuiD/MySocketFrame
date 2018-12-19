using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Proto;
using ProtoBuf;
using UnityEngine.UI;

namespace socket
{
    public class test : MonoBehaviour
    {

        //以下默认都是私有的成员
        Socket socket; //目标socket
        EndPoint serverEnd; //服务端
        IPEndPoint ipEnd; //服务端端口
        string recvStr; //接收的字符串
        string sendStr; //发送的字符串
        byte[] recvData = new byte[1024]; //接收的数据，必须为字节
        byte[] sendData = new byte[1024]; //发送的数据，必须为字节
        int recvLen; //接收的数据长度
        Thread connectThread; //连接线程\

        public void Start()
        {
            InitSocket();
        }


        //初始化
        public void InitSocket()
        {
            //定义连接的服务器ip和端口，可以是本机ip，局域网，互联网
            ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5501);
            //定义套接字类型,在主线程中定义
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //定义服务端
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            serverEnd = (EndPoint)sender;
            print("waiting for sending UDP dgram");

            //建立初始连接，这句非常重要，第一次连接初始化了serverEnd后面才能收到消息
            CMD_TEST cmdTest = new CMD_TEST();
            cmdTest.msg = System.Text.Encoding.Default.GetBytes("Hello Server!");
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize<CMD_TEST>(ms, cmdTest);

            SocketSend(ProtoCommand.ProtoCommand_TestModel, ms.ToArray());

            //开启一个线程连接，必须的，否则主线程卡死
            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();
        }

        public void SocketSend(ProtoCommand cmdHead, byte[] buffer)
        {
            MemoryStream ms = new MemoryStream();
            ProtoBaseCmd cmd = new ProtoBaseCmd();
            cmd.buffer = buffer;
            cmd.CmdHead = cmdHead;
            Serializer.Serialize<ProtoBaseCmd>(ms, cmd);
            //清空发送缓存
            sendData = new byte[1024];
            //数据类型转换
            sendData = ms.ToArray();
            //发送给指定服务端
            socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
        }

        //服务器接收
        void SocketReceive()
        {
            //进入接收循环
            while (true)
            {
                //对data清零
                recvData = new byte[1024];
                //获取客户端，获取服务端端数据，用引用给服务端赋值，实际上服务端已经定义好并不需要赋值
                recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
                print("message from: " + serverEnd.ToString()); //打印服务端信息
                                                                //输出接收到的数据
                                                                //recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);

                MemoryStream ms1 = new MemoryStream(recvData);
                ProtoBaseCmd p1 = Serializer.Deserialize<ProtoBaseCmd>(ms1);
                switch (p1.CmdHead)
                {
                    case ProtoCommand.ProtoCommand_TestModel:
                        MemoryStream ms2 = new MemoryStream(p1.buffer);
                        CMD_TEST p2 = Serializer.Deserialize<CMD_TEST>(ms2);
                        print(System.Text.Encoding.Default.GetString(p2.msg));
                        return;
                }
            }
        }

        //返回接收到的字符串  
        public string GetRecvStr()
        {
            string returnStr;
            //加锁防止字符串被改  
            lock (this)
            {
                returnStr = recvStr;
            }
            return returnStr;
        }

        //连接关闭
        public void SocketQuit()
        {
            //关闭线程
            if (connectThread != null)
            {
                connectThread.Interrupt();
                connectThread.Abort();
            }
            //最后关闭socket
            if (socket != null)
                socket.Close();
        }

        void OnApplicationQuit()
        {
            SocketQuit();
        }

    }

}
