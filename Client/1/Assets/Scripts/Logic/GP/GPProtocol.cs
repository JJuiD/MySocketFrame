using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proto;
using Proto.Cell;
using Proto.GPGameProto;

namespace Scripts.Logic.GP
{
    public class CellReqGameStart : Cell_Base
    {
        public override ProtoCommand Proto_Head => ProtoCommand.ProtoCommand_Game;
        public override uint Proto_Info => (uint)GameCMD.CMD_GAME_REQSTARTGAME;
        public override void SaveData(params object[] args)
        {
            CMD_ReqStartGame data = new CMD_ReqStartGame();
            data.roomid = (uint)args[0];
            this.buffer = Packet<CMD_ReqStartGame>(data);
        }
    }

    public class CellUpdateGameStep : Cell_Base
    {
        public override ProtoCommand Proto_Head => ProtoCommand.ProtoCommand_Game;
        public override uint Proto_Info => (uint)GameCMD.CMD_GAME_UPDATESTEP;
        public override void SaveData(params object[] args)
        {
            CMD_UpdateStep data = new CMD_UpdateStep();
            data.step = (uint)args[0];
            this.buffer = Packet<CMD_UpdateStep>(data);
        }
    }

    public class CellPlayerInfo : Cell_Base
    {
        public override ProtoCommand Proto_Head => ProtoCommand.ProtoCommand_Game;
        public override uint Proto_Info => (uint)GameCMD.CMD_GAME_PLAYERINFO;
        public override void SaveData(params object[] args) // 单机
        {
            CMD_PlayerInfo data = new CMD_PlayerInfo();
            data.seat = (uint)args[0];
            data.name = args[1].ToString();
            this.buffer = Packet<CMD_PlayerInfo>(data);
        }
        public override T ParseData<T>(byte[] buffer)
        {
            CMD_PlayerInfo data = unPacket<CMD_PlayerInfo>(buffer);
            return (T)data;
        }
    }

    public class CellRespGameStart : Cell_Base
    {
        public override ProtoCommand Proto_Head => ProtoCommand.ProtoCommand_Game;
        public override uint Proto_Info => (uint)GameCMD.CMD_GAME_RESPSTARTGAME;
        public override void SaveData(params object[] args) // 单机
        {
            return;
        }
    }
}
 