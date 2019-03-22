using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using ProtoBuf;
using System.Collections.Generic;
using System;
using Scripts;
using Scripts.Logic;
using Proto.Cell;

namespace Proto
{
    public class SocketManager : SingletonMono<SocketManager>
    {

        public delegate void CallBack(object _object, byte[] buffer);
        public void Start()
        {
            //InitSocket();
        }

        private Dictionary<ProtoCommand, List<CallBack>> callBackDic = new Dictionary<ProtoCommand, List<CallBack>>();
        public void addPortocolListen(CallBack callback, ProtoCommand cmd)
        {
            if (callBackDic.ContainsKey(cmd))
            {
                callBackDic[cmd].Add(callback);
            }
            else
            {
                callBackDic[cmd] = new List<CallBack>();
                callBackDic[cmd].Add(callback);
            }
        }

        //以下默认都是私有的成员
        Socket socket; //目标socket
        EndPoint serverEnd; //服务端
        IPEndPoint ipEnd; //服务端端口
        Thread connectThread; //连接线程
        Thread serverHeartThread;

        //初始化
        private void InitSocket()
        {
            CloseSocket();
            //定义连接的服务器ip和端口，可以是本机ip，局域网，互联网
            ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5501);
            //定义套接字类型,在主线程中定义
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //定义服务端
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            serverEnd = (EndPoint)sender;
            print("waiting for sending UDP dgram");

            //建立初始连接，这句非常重要，第一次连接初始化了serverEnd后面才能收到消息
            //CMD_TEST cmdTest = new CMD_TEST();
            //cmdTest.msg = System.Text.Encoding.Default.GetBytes("Hello Server!");
            //MemoryStream ms = new MemoryStream();
            //Serializer.Serialize<CMD_TEST>(ms, cmdTest);

            //SocketSend(ProtoCommand.ProtoCommand_TestModel, ms.ToArray());

            //开启一个线程连接，必须的，否则主线程卡死
            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();

            serverHeartThread = new Thread(new ThreadStart(SocketHeartSend));
            serverHeartThread.Start();
        }
        //连接关闭
        private void CloseSocket()
        {
            //关闭线程
            if (connectThread != null)
            {
                connectThread.Interrupt();
                connectThread.Abort();
            }

            //关闭心跳线程
            if (serverHeartThread != null)
            {
                serverHeartThread.Interrupt();
                serverHeartThread.Abort();
            }

            //最后关闭socket
            if (socket != null)
                socket.Close();
        }

        //定时发送服务器心跳
        void SocketHeartSend()
        {
            while (true)
            {
                CMD_HEART cmdHeart = new CMD_HEART();
                MemoryStream ms = new MemoryStream();
                Serializer.Serialize<CMD_HEART>(ms, cmdHeart);
                //SendPacket(ProtoCommand.ProtoCommand_Heart, ms.ToArray());
                Thread.Sleep(1000);
            }
        }

        //服务器接收
        void SocketReceive()
        {
            //进入接收循环
            while (true)
            {
                //对data清零
                byte[] recvData = new byte[1024];
                //获取客户端，获取服务端端数据，用引用给服务端赋值，实际上服务端已经定义好并不需要赋值
                int recvLen = socket.ReceiveFrom(recvData, ref serverEnd); //接收的数据长度
                print("message from: " + serverEnd.ToString()); //打印服务端信息
                                                                //输出接收到的数据
                                                                //recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                if(recvLen > 0 )
                {
                    MemoryStream ms1 = new MemoryStream(recvData);
                    ProtoBaseCmd p1 = Serializer.Deserialize<ProtoBaseCmd>(ms1);
                    foreach(CallBack call in callBackDic[p1.CmdHead])
                    {
                        call(SwitchToObject(p1.CmdHead, p1.CmdInfo),p1.buffer);
                    }
                }
                
            }
        }

        private System.Object SwitchToObject(ProtoCommand cmdHead, UInt32 cmd)
        {
            System.Object _object = null;
            switch (cmdHead)
            {
                case ProtoCommand.ProtoCommand_Room:
                    _object = Enum.ToObject(typeof(RoomBase.RoomCommand), cmd);
                    break;
            }

            return _object;
        }


        void OnApplicationQuit()
        {
            CloseSocket();
        }

        public void SendPacket(Cell_Base data)
        {
            MemoryStream ms = new MemoryStream();
            ProtoBaseCmd cmd = new ProtoBaseCmd();
            cmd.buffer = data.GetBuffer();
            cmd.CmdHead = data.Proto_Head;
            cmd.CmdInfo = data.Proto_Info;
            Serializer.Serialize<ProtoBaseCmd>(ms, cmd);
            //清空发送缓存
            byte[] sendData = new byte[1024];
            //数据类型转换
            sendData = ms.ToArray();
            //发送给指定服务端
            socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
        }
    }

}
