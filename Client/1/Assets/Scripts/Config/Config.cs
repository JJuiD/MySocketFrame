using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{
    public class PlayerInfo
    {
        public string name;
        public uint seat;
        public uint state { get; set; }

        public PlayerInfo()
        {
            name = "";
            seat = 0;
            state = 0;
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
