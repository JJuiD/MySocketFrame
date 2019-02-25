using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.PVPGame
{
    public class UIPVPGameInit : BaseUI
    {
        private string WM_PNL_UISelfPlayer = "WN_PNL_UISelfPlayer";
        private string WM_BTN_UpdateReadyState = "WN_BTN_UpdateReadyState";

        public override void Open(params object[] _params)
        {
            UIManager.GetInstance().RegisterClickEvent(WM_BTN_UpdateReadyState, this, onClickUpdateReadyState);
        }

        private void onClickUpdateReadyState()
        {
            Text textNode = GetWMNode(WM_BTN_UpdateReadyState).Find("Text").GetComponent<Text>();
            if(textNode.text == "Ready")
            {
                textNode.text = "Cancel";
                return;
            }
            textNode.text = "Ready";
        }
    }
}
