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
        public void StartGame(string GameName,bool isLineNet = false)
        {
            Debug.Log("GameController Init " + GameName);
            ResetData();
            SetLineNetState(isLineNet);
            switch (GameName)
            {
                case Config.PVPGame:
                    DataCenter.GetInstance().InsertUserDefault(GameName,Config.XML_PVPGAME_USERDEFAULT);
                    Logic = new PVPGame.PVPGameLogic();
                    //SetPlayerCount(1);
                    break;
            }
            Logic.InitData();
            UIManager.GetInstance().LoadScene(GameName);
        }

        public void ExitGame()
        {
            Logic.ExitGame();
            ResetData();
            UIManager.GetInstance().LoadScene(Config.Lobby);
        }

        private void ResetData()
        {
            Logic = null;
            playerList = new Dictionary<int, BasePlayer>();
            isLineNet = false;
        }

        #region Logic
        private BaseLogic Logic;
        public T GetLogic<T>() where T : BaseLogic { return (T)Logic; }

        //private int maxPlayer = 0;
        //public void SetPlayerCount(int n) { maxPlayer = n; }
        //public int GetPlayerCount() { return maxPlayer; }
        #endregion

        #region 网络状态
        private bool isLineNet = false;
        public bool GetLineNetState() { return isLineNet; }
        private void SetLineNetState(bool isLineNet) { this.isLineNet = isLineNet; }
        #endregion

        #region 玩家相关
        private Dictionary<int, BasePlayer> playerList;
        public int GetPlayerCount() { return playerList.Count; }
        public void AddPlayer(BasePlayer player)
        {
            if (playerList.Count == 0)
            {
                playerList.Add(player.GetServerSeat(), player);
                return;
            }

            if (GetPlayerBySeat<BasePlayer>(player.GetServerSeat()) != null)
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
        public T GetHero<T>() where T : BasePlayer
        {
            return (T)GetPlayerByLocalSeat<T>(0);
        }
        #endregion


        //游戏地图

        private void FixedUpdate()
        {
            Logic.LogicFixedUpdate();
        }

        
    }
}

