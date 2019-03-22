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
        private GameState state;
        private Random randseed ;

        public void Init()
        {
            randseed = new Random();
            state = GameState.NONE;
        }
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
            GameController.GetInstance().GetLogic<GPLogic>().LogicFixedUpdate();
        }

        public void OnPacketDealCenter(Cell_Base cell)
        {
            if (cell.Proto_Head != ProtoCommand.ProtoCommand_Game) return;
            Object _object = null;
            byte[] buffer = new byte[1024];
            switch ((GameCMD)cell.Proto_Info)
            {
                case GameCMD.CMD_GAME_REQSTARTGAME:
                    _object = Enum.ToObject(typeof(GameCMD), (int)GameCMD.CMD_GAME_PLAYERINFO);
                    CellPlayerInfo cellPlayer = new CellPlayerInfo();
                    cellPlayer.SaveData(0,DataCenter.GetInstance().LocalName);
                    GameController.GetInstance().Logic.OnReciveConsole(_object, cellPlayer.GetBuffer());

                    _object = Enum.ToObject(typeof(GameCMD),(int)GameCMD.CMD_GAME_RESPSTARTGAME);
                    state = GameState.READY;
                    break;
            }
            GameController.GetInstance().Logic.OnReciveConsole(_object, buffer);
        }
    }
}
