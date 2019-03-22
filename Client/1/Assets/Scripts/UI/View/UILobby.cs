using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.UI
{
    public class UILobby : BaseUI
    {
        private string WN_BTN_GameList = "WN_BTN_GameList";
        private string WN_BTN_Set = "WN_BTN_Set";
        private string WN_BTN_GP = "WN_BTN_GP";

        public override void Open(params object[] _params)
        {
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_Set, this, onClickSet);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_GP, this, onClickGPStart);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_GameList, this, onClickShowGameList);
        }

        private void onClickShowGameList()
        {
            Transform gameListPnl = GetWNode(WN_BTN_GameList).Find("PNL_GameList");
            Debug.Log("onClickStart onClickShowGameList");
            //UIManager.GetInstance().LoadScene(Config.GP);
            gameListPnl.gameObject.SetActive(!gameListPnl.gameObject.activeSelf);
        }

        private void onClickGPStart()
        {
            //Logic.GameController.GetInstance().StartGame(Config.GP);
            UIManager.GetInstance().OpenNode<UIRoomMenu>(UIConfig.UIRoomMenu,Config.GP);
        }

        private void onClickSet()
        {
            Debug.Log("onClickSet Set");
            //UIManager.GetInstance().OpenNode<UISet>(UIConfig.UISet);
        }
    }
}
