using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
    struct KEY_STATE
    {
        KeyCode down;
        KeyCode up;
    }

    public class GameController : SingletonMono<GameController>
    {
        public string GameName {get ; set;}

        private UserDefault gameUserDefault;
        
        private Dictionary<string, int> IntUesrDefault;
        private Dictionary<string, float> FltUserDefault;
        private Dictionary<string, string> StrUserDefault;
        private BaseLogic Logic;

        private int maxPlayer = 4;
        public void SetMaxPlayer(int n ) { maxPlayer = n; }
        public int GetMaxPlayer() { return maxPlayer; }

        public T GetLogic<T>() where T : BaseLogic { return (T)Logic; }
        public UserDefault GetGameUserDefault() { return gameUserDefault; }

        public void Init()
        {
            Debug.Log("GameController Init " + GameName);
            switch(GameName)
            {
                case Config.PVPGameScene:
                    gameUserDefault = FileUtils.LoadFromXml<UserDefault>(Config.XML_PVPGAME_USERDEFAULT);
                    Logic = new PVPGame.PVPGameLogic();
                    playerObject = Resources.Load<GameObject>(Config.PVPGame_PlayerObject);
                    SetMaxPlayer(4);
                    break;
            }
            SaveUserDefault();
            Logic.Init();
            Logic.LoadKeyEventDic(gameUserDefault);
        }

        //游戏地图
        private void InitMapData(int mapIndex = 0)
        {
            Logic.InitMapData(mapIndex);
        }

        #region UserDefault
        private void SaveUserDefault()
        {
            FltUserDefault = new Dictionary<string, float>();
            foreach (var temp in gameUserDefault._FLTValues)
            {
                FltUserDefault.Add(temp.name, temp.value);
            }

            IntUesrDefault = new Dictionary<string, int>();
            foreach (var temp in gameUserDefault._INTValues)
            {
                IntUesrDefault.Add(temp.name, temp.value);
            }

            StrUserDefault = new Dictionary<string, string>();
            foreach (var temp in gameUserDefault._STRValues)
            {
                StrUserDefault.Add(temp.name, temp.value);
            }
        }
        public T GetUserDefault<T>(string index) 
        {
            if(typeof(T) == typeof(int) && IntUesrDefault.ContainsKey(index))
            {
                return (T)((object)IntUesrDefault[index]);
            }
            else if (typeof(T) == typeof(float) && FltUserDefault.ContainsKey(index))
            {
                return (T)((object)FltUserDefault[index]);
            }
            else if (typeof(T) == typeof(string) && StrUserDefault.ContainsKey(index))
            {
                return (T)((object)StrUserDefault[index]);
            }

            throw new NotImplementedException();
        }
        #endregion

        public void JoinGame()
        {
            this.Logic.JoinGame();
        }

        public void Clear()
        {
            gameUserDefault = null;
            Logic = null;
        }

        private void FixedUpdate()
        {
            Logic.LogicFixedUpdate();
        }

        #region 玩家相关
        private GameObject playerObject;
        private Dictionary<int, BasePlayer> playerList;
        public void AddPlayer(BasePlayer player)
        {
            if (playerList.Count == 0)
            {
                playerList.Add(player.GetServerSeat(),player);
                return;
            }

            if(GetPlayerBySeat<BasePlayer>(player.GetServerSeat()))
            {
                Debug.LogError("[ERROR] 存在同样的用户");
                return;
            }
        }

        public T GetPlayerBySeat<T>(Int16 seat) where T : BasePlayer
        {
            if (playerList.Count == 0) { return null; }
            if (playerList.ContainsKey(seat)) return (T)playerList[seat];
            Debug.LogError("[ERROR] GetPlayerBySeat : " + seat.ToString() + " 不存在");
            return null;
        }

        public T GetPlayerByLocalSeat<T>(Int16 localseat) where T : BasePlayer
        {
            if (playerList.Count == 0) { return null; }
            foreach (var temp in playerList)
            {
                if (temp.Value.GetLocalSeat() == localseat)
                {
                    return (T)temp.Value;
                }
            }
            return null;
        }
        #endregion
    }
}

