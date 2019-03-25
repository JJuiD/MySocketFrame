using Proto;
using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{

    public class GameController : Singleton<GameController>
    {
        /// <summary>
        /// 游戏逻辑
        /// </summary>
        public LogicBase Logic {get;set;}
        private int playerCount = 0;
        /// <summary>
        /// 是否为单机
        /// </summary>
        private bool isConsole;
        /// <summary>
        /// <服务器标识,玩家信息>
        /// </summary>
        private Dictionary<uint, PlayerInfo> dic_playerinfo = new Dictionary<uint, PlayerInfo>();
        private uint lerpTag = 0; //服务器标识和本地的偏差值
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="name"></param>
        /// <param name="count">玩家人数</param>
        /// <param name="isConsole"></param>
        public void StartGame(string name,int count ,bool isConsole = true)
        {
            Debug.Log("GameController Init " + name);
            playerCount = count;
            SetLineNetState(isConsole);
            switch (name)
            {
                case Config.GP:
                    Logic = new GP.GPLogic();
                    break;
            }
            Logic.InitData();
            
        }
        /// <summary>
        /// 获取逻辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>


        #region 网络状态
        /// <summary>
        /// 获取网络状态 (True: 离线)
        /// </summary>
        /// <returns></returns>
        public bool GetLineNetState() { return isConsole; }
        private void SetLineNetState(bool isConsole) { this.isConsole = isConsole; }
        #endregion


        #region 玩家相关
        // localTag = 0:玩家本人
        public void AddPlayer(PlayerInfo playerInfo)
        {
            if (GetPlayerByTag(playerInfo.seat) != null) return;
            //是否是本人
            if(playerInfo.name == DataCenter.GetInstance().LocalName)
            {
                dic_playerinfo.Add(0,playerInfo);
                lerpTag = playerInfo.seat;
            }
            else
            {
                uint localTag = Tag2Local(playerInfo.seat);
                dic_playerinfo.Add(localTag, playerInfo);
            }
        }
        public void UpdatePlayerState(uint tag,uint state)
        {
            dic_playerinfo[tag].state = state;
        }
        public PlayerInfo GetPlayerByTag(uint tag)
        {
            if (dic_playerinfo.Count == 0) { return null; }
            if (dic_playerinfo.ContainsKey(tag)) return dic_playerinfo[tag];
            return null;
        }
        public PlayerInfo GetPlayerByLocalTag(uint localTag)
        {
            uint tag = Local2Tag(localTag);
            return GetPlayerByTag(tag);
        }
        public PlayerInfo GetSelfPlayerInfo()
        {
            return GetPlayerByLocalTag(0);
        }
        public uint Tag2Local(uint tag) { return (uint)((tag + lerpTag) % playerCount); }
        public uint Local2Tag(uint localtag) { return (uint)((localtag + lerpTag) % playerCount); }
        public int GetPlayerCount() { return playerCount; }
        #endregion

    }
}

