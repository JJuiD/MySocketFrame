using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class UIMessageBox : BaseUI
    {
        private Transform Btn_Confirm;
        private Transform Btn_Cancel;
        private Transform Btn_Click;
        private Text Txt_Info; //显示的内容
        private Text Txt_Head; //标题

        public UnityAction ConfirmAction;
        public UnityAction CancelAction;
        public UnityAction CloseAction;

        /// <summary>
        /// (string,string,string)
        /// (内容,标题,
        /// </summary>
        /// <param name="_params"></param>
        public override void Open(params object[] _params)
        {
            string InfoStr = _params[0].ToString();
            InitInfo(InfoStr);
            string HeadStr = _params[1].ToString();
            InitHead(HeadStr);
            string BtnStr = _params[2].ToString();
            InitBtn(BtnStr);
            
            
        }

        private void InitInfo(string info)
        {
            if(info == "")
            {
                Debug.LogError("UIMessageBox Info is Null");
                return;
            }
            Txt_Info = this.transform.Find("Txt_Info").GetComponent<Text>();
            Txt_Info.text = info;
        }

        private void InitHead(string head)
        {
            if (head == "") return;
            Txt_Head = this.transform.Find("Txt_Head").GetComponent<Text>();
            Txt_Head.text = head;
        }

        private void InitBtn(string strBtn)
        { 
            if(strBtn == "") return ;
            string[] strBtns = strBtn.Split('|');
            Btn_Confirm = this.transform.Find("Btn_Confirm");
            Btn_Cancel = this.transform.Find("Btn_Cancel");
            Btn_Click = this.transform.Find("Btn_Click");

            AddClickFunc(Btn_Confirm, onClickConfirm, ConfirmAction);
            AddClickFunc(Btn_Cancel, onClickCancel, CancelAction);
            AddClickFunc(Btn_Click, onClickClose, CloseAction);
        }

        private void onClickConfirm()
        {
            this.Close();
        }

        private void onClickCancel()
        {
            this.Close();
        }

        private void onClickClose()
        {
            this.Close();
        }

        private void AddClickFunc(Transform node,UnityAction defaultAction,UnityAction Action = null)
        {
            if (Action == null)
            {
                UIManager.GetInstance().RegisterClickEvent(node, defaultAction);
                return;
            }
            UIManager.GetInstance().RegisterClickEvent(node, Action);
        }
    }
}
