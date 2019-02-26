using Scripts.Logic;
using Scripts.Logic.PVPGame;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts.UI.PVPGame
{
    public class UIPVPGameInit : BaseUI
    {
        private string WM_PNL_UISelfPlayer = "WN_PNL_UISelfPlayer";
        private string WM_BTN_UpdateReadyState= "WN_BTN_UpdateReadyState";

        private PVPGameLogic gamelogic;

        public override void Open(params object[] _params)
        {
            List<INTValue> intValues = GameController.GetInstance().GetGameUserDefault()._INTValues;
            int defaultHeroId = 0;
            int defaultWeaponId = 0;
            foreach (var temp in intValues)
            {
                if(temp.name == "DEFAULT_HERO")
                {
                    defaultHeroId = temp.value;
                }
                else if (temp.name == "DEFAULT_WEAPON")
                {
                    defaultWeaponId = temp.value;
                }
            }
            gamelogic = GameController.GetInstance().GetLogic<PVPGameLogic>();
            SetSelfHeroUI(gamelogic.GetHeroInfo(defaultHeroId));
            UIManager.GetInstance().RegisterClickEvent(WM_BTN_UpdateReadyState, this, onClickUpdateReadyState);
        }

        private void SetSelfHeroUI(Dictionary<string,string> dic)
        {

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
