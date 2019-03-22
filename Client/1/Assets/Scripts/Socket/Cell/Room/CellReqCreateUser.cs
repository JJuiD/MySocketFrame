using System;

namespace Proto.Cell.Room
{
    //class CellReqCreateUser : CellBase
    //{
        //public override void Start(params object[] _params)
        //{
        //    UserProto.ReqCreateUser packet = new UserProto.ReqCreateUser();
        //    packet.name = _params[0].ToString();
        //    byte[] buffer = Packet<UserProto.ReqCreateUser>(packet);
        //    SocketManager.GetInstance().addPortocolListen(OnReceivePacket, ProtoCommand.ProtoCommand_Room);
        //    SocketManager.GetInstance().SendRoomPacket(buffer);
        //}

        //public override void OnReceivePacket(object _object, byte[] buffer)
        //{
        //    if ((RoomBase.RoomCommand)_object != RoomBase.RoomCommand.CMDR_REQCREATEUSER) return;
        //    UserProto.RespCreateUser packet = unPacket<UserProto.RespCreateUser>(buffer);
        //    if(packet.data.flag == ProtoConfig.PROTO_FLAG_SUCCESS)
        //    {
        //        Console.WriteLine(string.Format("ID:%s,Name:%s", packet.userinfo.id, packet.userinfo.name));
        //        //FileUtils.writeToXml<string>("ID", packet.userinfo.id);
        //        //FileUtils.writeToXml<string>("NAME", packet.userinfo.name);
        //    }
        //}
   // }
}

