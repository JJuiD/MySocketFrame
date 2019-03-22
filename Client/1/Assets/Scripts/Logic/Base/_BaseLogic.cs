﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
     
    public abstract class BasePlayer 
    {
        protected PlayerInfo playerInfo;
        public void SetServerPlayerData(PlayerInfo playerinfo)
        {
            if (playerInfo == null) playerInfo = new PlayerInfo();
            playerInfo.name = playerinfo.name;
            playerInfo.seat = playerinfo.seat;
            playerInfo.localSeat = playerinfo.localSeat;
            playerInfo.SetPlayerState(PlayerServerState.FREE);
        }
        public Int16 GetServerSeat() { return playerInfo.seat; }
        public Int16 GetLocalSeat() { return playerInfo.localSeat; }
    }

    public abstract class LogicBase
    {
        public LogicBase() { }
        ~LogicBase() {  }
        //数据初始化
        public abstract void Start();
        //进入,离开游戏
        public abstract void StartGame();
        public abstract void ExitGame();
        //本地更新
        public abstract void LogicFixedUpdate();
        //按键绑定
        public abstract void InitKeyEventDic();
        //游戏公共包
        public abstract void RecvGPBuffer(object _object, byte[] buffer);
        //不同游戏的包
        public abstract void RecvGSBuffer(object _object, byte[] buffer);
    }

}