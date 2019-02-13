using System.Collections.Generic;
using UnityEngine;
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

        /// <summary>
        /// (string,int,string)
        /// </summary>
        /// <param name="_params"></param>
        public override void Open(params object[] _params)
        {
            InitBtn();
            Txt_Info = this.transform.Find("Txt_Info").GetComponent<Text>();
            Txt_Head = this.transform.Find("Txt_Head").GetComponent<Text>();
        }

        private void InitBtn()
        {
            Btn_Confirm = this.transform.Find("Btn_Confirm");
            Btn_Cancel = this.transform.Find("Btn_Cancel");
            Btn_Click = this.transform.Find("Btn_Click");

            UIManager.GetInstance().RegisterClickEvent(Btn_Confirm, this, onClickConfirm);
            UIManager.GetInstance().RegisterClickEvent(Btn_Cancel, this, onClickCancel);
            UIManager.GetInstance().RegisterClickEvent(Btn_Click, this, onClickClose);
        }

        private void onClickCancel()
        {

        }

        private void onClickConfirm()
        {

        }

        private void onClickClose()
        {
            this.Close();
        }
    }
}
