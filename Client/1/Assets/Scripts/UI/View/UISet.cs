using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI
{
    public class UISet : BaseUI
    {
        private string WN_PNL_KeyList = "WN_PNL_KeyList";
        private string WN_BTN_CLOSE = "WN_BTN_CLOSE";
        private string WN_BTN_SAVE = "WN_BTN_SAVE";


        public override void onEnter()
        {
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_SAVE, this, onClickSave);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_CLOSE, this, onClickClose);
            InitKeyCode();
        }

        private void InitKeyCode()
        {
            Transform KeyListNode = UIManager.GetInstance().GetWNTransform(WN_PNL_KeyList, this);
            foreach (Transform temp in KeyListNode)
            {
                string KeyName = temp.name;
                string _KeyCode = DataCenter.GetInstance().GetKeyValue(KeyName);

                //修改显示的按键
            }
        }

        private void onClickSave()
        {

        }

        private void onClickClose()
        {

        }
    }
}
