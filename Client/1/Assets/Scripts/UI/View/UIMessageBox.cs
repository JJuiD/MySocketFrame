using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIMessageBox : BaseUI
    {
        private GameObject Btn_Confirm;
        private GameObject Btn_Cancel;
        private GameObject Btn_Close;
        private Text Txt_Info; //显示的内容
        private Text Txt_Head; //标题

        public UnityAction<string> ConfirmAction;
        public UnityAction<string> CancelAction;
        public UnityAction<string> CloseAction;

        
        public override void Open(params object[] _params)
        {
            string InfoStr = _params.Length >= 1 ? _params[0].ToString() : "";
            InitInfo(InfoStr);
            string HeadStr = _params.Length >= 2 ? _params[1].ToString() : "";
            InitHead(HeadStr);
            string BtnStr = _params.Length >= 3 ? _params[2].ToString() : "";
            InitBtn(BtnStr);
            bool IsShow = _params.Length >= 4 ? (bool)_params[3] : false;
            InitCloseBtn(IsShow);
        }

        private void InitInfo(string info)
        {
            if(info == "")
            {
                Debug.LogError("UIMessageBox Info is Null");
                return;
            }
            Txt_Info = this.transform.Find("TXT_Info").GetComponent<Text>();
            Txt_Info.text = info;
        }

        private void InitHead(string head)
        {
            if (head == "") return;
            Txt_Head = this.transform.Find("TXT_Head").GetComponent<Text>();
            Txt_Head.text = head;
        }

        private void InitBtn(string strBtn)
        { 
            if(strBtn == "") return ;
            string[] strBtns = strBtn.Split('|');
            Btn_Confirm = this.transform.Find("BTN_Confirm").gameObject;
            Btn_Cancel = this.transform.Find("BTN_Cancel").gameObject;

            InitBtnMsg(Btn_Confirm, strBtns, 0);
            InitBtnMsg(Btn_Cancel, strBtns, 1);

            AddClickFunc(Btn_Confirm, onClickClose);
            AddClickFunc(Btn_Cancel, onClickClose);

            if (strBtns.Length == 1 )
            {
                UIManager.GetInstance().SetUIXPosition(Btn_Confirm.transform,0);
            }
        }

        private void InitCloseBtn(bool isShow = false)
        {
            Btn_Close = this.transform.Find("BTN_Close").gameObject;
            Btn_Close.SetActive(false);
            AddClickFunc(Btn_Close, onClickClose, CloseAction);
        }

        private void onClickClose()
        {
            this.Close();
        }

        private void AddClickFunc(GameObject node,UnityAction defaultAction, UnityAction<string> Action = null)
        {
            if (!node.activeSelf) return;
            if (Action == null)
            {
                UIManager.GetInstance().RegisterClickEvent(node, defaultAction);
                return;
            }
            UIManager.GetInstance().RemoveAllClickListener(node);
            defaultAction = delegate ()
            {
                Action(node.name);
                onClickClose();
            };
            UIManager.GetInstance().RegisterClickEvent(node, defaultAction);
        }

        private void InitBtnMsg(GameObject btn,string[] array,int index)
        {
            if (index >= array.Length) return;
            btn.name = array[index];
            btn.SetActive(true);
        }

        public void AddConfrimBtnCallBack(UnityAction<string> Action)
        {
            AddClickFunc(Btn_Confirm, onClickClose, Action);
        }

        public void AddCancelBtnCallBack(UnityAction<string> Action)
        {
            AddClickFunc(Btn_Cancel, onClickClose, Action);
        }
    }
}
