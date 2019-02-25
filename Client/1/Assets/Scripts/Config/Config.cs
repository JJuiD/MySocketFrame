using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{
    public static class Config
    {

        #region LocalCmd
        //事件类型
        public const string KEY_EVENT_TAG = "KEY_EVENT_TAG"; //默认按键

        //Xml文件
        public const string XML_USERDEFAULT = "UserDefault.xml";
        public const string XML_UIDEFAULT   = "UIDefault.xml";

        public const string XML_PVPGAME_USERDEFAULT = "PVPGame/PVPGameUserDefault.xml";
        //public const string XML_CARDDEFAULT = "CardDefault.xml";


        //事件名
        public const string KEY_UP = "KEY_UP";
        public const string KEY_LEFT = "KEY_LEFT";
        public const string KEY_DOWN = "KEY_DOWN";
        public const string KEY_RIGHT = "KEY_RIGHT";

        public const string KEY_ATTACK = "KEY_ATTACK";

        //场景
        public const string LobbyScene = "LobbyScene";
        public const string PVPGameScene = "PVPGameScene";
        public const string TowerDefenseScene = "TowerDefenseScene";


        #endregion
    }
}
