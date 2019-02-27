using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{
    public enum PlayerGameState
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
        private PlayerGameState state;

        public PlayerInfo()
        {
            name = "";
            seat = 0;
            localSeat = 0;
            state = PlayerGameState.FREE;
        }

        public void SetPlayerState(PlayerGameState state)
        {
            this.state = state;
        }
    }

    public static class Config
    {

        #region LocalCmd
        //事件类型
        public const string KEY_EVENT_TAG = "KEY_EVENT_TAG"; //默认按键

        //Xml文件
        public const string XML_USERDEFAULT = "UserDefault.xml";
        public const string XML_UIDEFAULT   = "UIDefault.xml";

        public const string XML_PVPGAME_USERDEFAULT = "PVPGame/PVPGameUserDefault.xml";
        public const string XML_PVPGAME_HERODEFAULT = "PVPGame/PVPGameHeroDefault.xml";
        public const string XML_PVPGAME_WEAPONDEFAULT = "PVPGame/PVPGameWeaponDefault.xml";
        public const string XML_PVPGAME_SKILLDEFAULT = "PVPGame/PVPGameSkillDefault.xml";
        //public const string XML_CARDDEFAULT = "CardDefault.xml";




        //场景
        public const string LobbyScene = "LobbyScene";
        public const string PVPGameScene = "PVPGameScene";
        public const string TowerDefenseScene = "TowerDefenseScene";


        #endregion
    }
}
