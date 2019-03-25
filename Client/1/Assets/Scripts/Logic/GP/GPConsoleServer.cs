using Proto;
using Proto.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proto.GPGameProto;

namespace Scripts.Logic.GP
{
    public class GPConsoleServer : SingletonMono<GPConsoleServer>
    {
        public float fixedTime = 0.01f;
        private GameStep state = GameStep.NONE;
        private Random randseed = new Random();
        private List<CallBack> call_list = new List<CallBack>();

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int GetRandNum(int begin,int end)
        {
            return randseed.Next(begin, end);
        }

        public void FixedUpdate()
        {
            GameController.GetInstance().Logic.LogicFixedUpdate();
        }

        public void OnPacketDealCenter(Cell_Base cell)
        {
            if (cell.Proto_Head != ProtoCommand.ProtoCommand_Game) return;
            Object _object = null;
            byte[] buffer = new byte[1024];
            switch ((GPGameCMD)cell.Proto_Info)
            {
                case GPGameCMD.CMD_GAME_REQSTARTGAME:
                    //添加玩家信息
                    _object = Enum.ToObject(typeof(GPGameCMD), (int)GPGameCMD.CMD_GAME_PLAYERINFO);
                    CellPlayerInfo cellPlayer = new CellPlayerInfo();
                    cellPlayer.SaveData(0,DataCenter.GetInstance().LocalName);
                    GameController.GetInstance().Logic.OnRecivePacket(_object, cellPlayer.GetBuffer());
                    //显示英雄选择界面
                    _object = Enum.ToObject(typeof(GPGameCMD),(int)GPGameCMD.CMD_GAME_RESPSTARTGAME);
                    state = GameStep.READY;
                    GameController.GetInstance().Logic.OnReciveConsole(_object, buffer);
                    break;
                case GPGameCMD.CMD_GAME_UPDATEPLAYERSTATE:
                    //隐藏英雄选择界面，开始游戏
                    _object = Enum.ToObject(typeof(GPGameCMD), (int)GPGameCMD.CMD_GAME_UPDATESTEP);
                    CellUpdateGameStep cellStep = new CellUpdateGameStep();
                    cellStep.SaveData(GameStep.START);
                    GameController.GetInstance().Logic.OnRecivePacket(_object, cellStep.GetBuffer());
                    break;
            }

            List<int> del_f = new List<int>();
            for(int i = 0;i < call_list.Count;++i)
            {
                if (call_list[i](_object, buffer))
                {
                    del_f.Add(i);
                }
            }
            for (int i = 0; i < del_f.Count; ++i)
            {
                call_list.RemoveAt(del_f[i] - i);
            }
            
        }
        public void addPortocolListen(CallBack callback)
        {
            call_list.Add(callback);
        }
    }
}
