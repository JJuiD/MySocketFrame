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
        public override ProtoCommand Proto_Head { get { return ProtoCommand.ProtoCommand_Game; } }
        public override uint Proto_Info { get { return (uint)GPGameCMD.CMD_GAME_REQSTARTGAME; } }
        public override void SaveData(params object[] args)
        {
            GPCMD_ReqStartGame data = new GPCMD_ReqStartGame();
            data.roomid = Convert.ToUInt32(args[0]); 
            this.buffer = Packet<GPCMD_ReqStartGame>(data);
        }
    }

    public class CellUpdateGameStep : Cell_Base
    {
        public override ProtoCommand Proto_Head { get { return ProtoCommand.ProtoCommand_Game; } }
        public override uint Proto_Info { get { return (uint)GPGameCMD.CMD_GAME_UPDATESTEP; } }
        public override void SaveData(params object[] args)
        {
            GPCMD_UpdateStep data = new GPCMD_UpdateStep();
            data.step = Convert.ToUInt32(args[0]); ;
            this.buffer = Packet<GPCMD_UpdateStep>(data);
        }
    }

    public class CellPlayerInfo : Cell_Base
    {
        public override ProtoCommand Proto_Head { get { return ProtoCommand.ProtoCommand_Game; } }
        public override uint Proto_Info { get { return (uint)GPGameCMD.CMD_GAME_PLAYERINFO; } }
        public override void SaveData(params object[] args) // 单机
        {
            GPCMD_PlayerInfo data = new GPCMD_PlayerInfo();
            data.seat = Convert.ToUInt32(args[0]);
            data.name = args[1].ToString();
            this.buffer = Packet<GPCMD_PlayerInfo>(data);
        }
    }

    public class CellUpdatePlayerState : Cell_Base
    {
        public override ProtoCommand Proto_Head { get { return ProtoCommand.ProtoCommand_Game; } }
        public override uint Proto_Info { get { return (uint)GPGameCMD.CMD_GAME_UPDATEPLAYERSTATE; } }
        public override void SaveData(params object[] args) // 单机
        {
            GPCMD_UpdatePlayerState data = new GPCMD_UpdatePlayerState();
            data.seat = Convert.ToUInt32(args[0]);
            data.state = Convert.ToUInt32(args[1]);
            this.buffer = Packet<GPCMD_UpdatePlayerState>(data);
        }
    }

    public class CellRespGameStart : Cell_Base
    {
        public override ProtoCommand Proto_Head { get { return ProtoCommand.ProtoCommand_Game; } }
        public override uint Proto_Info { get { return (uint)GPGameCMD.CMD_GAME_RESPSTARTGAME; } }
        public override void SaveData(params object[] args) // 单机
        {
            return;
        }
    }
}
 