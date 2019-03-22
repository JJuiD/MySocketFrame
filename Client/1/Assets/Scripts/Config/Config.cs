﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{
    public enum PlayerServerState
    {
        FREE,
        READY,
        INGAME,
    }

    public class PlayerInfo
    {
        public string name;
        public Int16 seat;
        public Int16 localSeat;
        private PlayerServerState state;

        public PlayerInfo()
        {
            name = "";
            seat = 0;
            localSeat = 0;
            state = PlayerServerState.FREE;
        }

        public void SetPlayerState(PlayerServerState state)
        {
            this.state = state;
        }
    }

    public static class Config
    {

        #region LocalCmd
        //事件类型
        public const string KEY_TAG = "KEY_TAG"; //默认按键

        //Xml文件
        public const string XML_USERDEFAULT = "UserDefault.xml";
        public const string XML_UIDEFAULT   = "UIDefault.xml";

        


        //场景
        public const string Lobby = "Lobby";
        public const string GP = "GP";
        public const string TowerDefense = "TowerDefense";

         

        #endregion
    }
}
