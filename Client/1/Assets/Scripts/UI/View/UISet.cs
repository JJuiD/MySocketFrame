using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UISet : BaseUI
    {
        private string WN_PNL_KeyList = "WN_PNL_KeyList";
        private string WN_BTN_CLOSE = "WN_BTN_CLOSE";
        private string WN_BTN_SAVE = "WN_BTN_SAVE";


        public override void Open(params object[] _params)
        {
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_SAVE, this, onClickSave);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_CLOSE, this, onClickClose);
            InitKeyCode();
        }

        private void InitKeyCode()
        {
            Transform KeyListNode = GetWMNode(WN_PNL_KeyList);
            foreach (Transform temp in KeyListNode)
            {
                string KeyName = temp.name;
                string _KeyCode = DataCenter.GetInstance().GetKeyValue(KeyName);

                //修改显示的按键
                Text holder = temp.Find("holder").GetComponent<Text>();
                holder.text = _KeyCode;
            }
        }

        private void onClickSave()
        {
            SaveToLocal();
        }

        private void SaveToLocal()
        {
            Transform KeyListNode = GetWMNode(WN_PNL_KeyList);
            foreach (Transform temp in KeyListNode)
            {
                string KeyName = temp.name;
                Text holder = temp.Find("holder").GetComponent<Text>();
                KeyCode _KeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), holder.text);
                DataCenter.GetInstance().SetKeyEvent(KeyName, _KeyCode);
            }
        }

        private void onClickClose()
        {
            UIMessageBox _UIMessageBox = UIManager.GetInstance().OpenNode<UIMessageBox>(
                UIConfig.UIMessageBox,"是否应用设置", "", "save|save_cancel");
            _UIMessageBox.AddConfrimBtnCallBack(onClickMessageBoxConfirm);
            _UIMessageBox.AddCancelBtnCallBack(onClickMessageBoxCancel);
        }

        private void onClickMessageBoxConfirm(string btnName)
        {
            if(btnName == "save")
            {
                SaveToLocal();
                this.Close();
            }
        }

        private void onClickMessageBoxCancel(string btnName)
        {
            if (btnName == "save_cancel")
            {
                this.Close();
            }
        }
    }
}
