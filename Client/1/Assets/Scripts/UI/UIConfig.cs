using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scripts.UI
{
    public static class UIConfig
    {
        

        //UI
        public const string UILobby      = "UILobby";          //大厅界面
        public const string UISet = "UISet";          //大厅设置界面
        public const string UIRoomMenu = "UIRoomMenu"; //房间选择界面

        public const string UIGP    = "UIGP";              //游戏界面
        public const string UIGPInit = "UIGPInit";      //游戏人物地图设置界面

        /// <summary>
        /// (string,string,string,bool)
        /// (内容,标题,按钮,关闭按钮)
        /// 按钮: Btn_1|Btn_2 
        /// </summary>
        public const string UIMessageBox = "UIMessageBox";   //弹窗界面
    }
}
