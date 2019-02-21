using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scripts.UI
{
    public static class UIConfig
    {
        public const string LobbyScene = "LobbyScene";
        public const string PVPGameScene = "PVPGameScene";
        public const string TowerDefenseScene = "TowerDefenseScene";

        //UI
        public const string UILobby      = "UILobby";          //大厅界面
        public const string UIPVPGame = "UIPVPGame";              //游戏界面

        public const string UISet        = "UISet";          //大厅设置界面
        /// <summary>
        /// (string,string,string,bool)
        /// (内容,标题,按钮,关闭按钮)
        /// 按钮: Btn_1|Btn_2 
        /// </summary>
        public const string UIMessageBox = "UIMessageBox";   //弹窗界面
    }
}
