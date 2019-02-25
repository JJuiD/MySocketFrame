using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.UI
{
    public class UILobby : BaseUI
    {
        private string WN_BTN_START = "WN_BTN_START";
        private string WN_BTN_SET = "WN_BTN_SET";

        public override void Open(params object[] _params)
        {
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_SET, this, onClickSet);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_START, this, onClickStart);
        }

        private void onClickStart()
        {
            Debug.Log("onClickStart Start");
            UIManager.GetInstance().LoadScene(Config.PVPGameScene);
        }

        private void onClickSet()
        {
            Debug.Log("onClickSet Start");
            UIManager.GetInstance().OpenNode<UISet>(UIConfig.UISet);
        }
    }
}
