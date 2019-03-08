using Scripts.Logic;
using Scripts.Logic.PVPGame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.PVPGame
{
    public class UIPVPGameInit : BaseUI
    {
        private string WN_PNL_UISelfPlayer = "WN_PNL_UISelfPlayer";
        private string WN_BTN_UpdateReadyState = "WN_BTN_UpdateReadyState";
        private string WN_PNL_Weapon_Info = "WN_PNL_Weapon_Info";

        private PVPGameLogic gamelogic;
        private int heroId = 0;
        private int weaponId = 0;

        public override void Open(params object[] _params)
        {
            heroId = DataCenter.GetInstance().GetUserDefault().GetUserDefaultValue<int>("DEFAULT_HERO");
            weaponId = DataCenter.GetInstance().GetUserDefault().GetUserDefaultValue<int>("DEFAULT_WEAPON");
            gamelogic = GameController.GetInstance().GetLogic<PVPGameLogic>();
            SetSelfHeroUI(heroId);
            SetSelfWeaponUI(weaponId);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_UpdateReadyState, this, OnClickUpdateReadyState);
        }

        private void SetSelfHeroUI(int id)
        {
            HeroUnit data = gamelogic.GetHeroInfo(id);
            Transform ImgHead = GetWMNode(WN_PNL_UISelfPlayer).Find("IMG_HeroHead");
            Transform TxtName = GetWMNode(WN_PNL_UISelfPlayer).Find("TXT_HeroName");
            ImgHead.GetComponent<Image>().sprite = data.headImage;
            TxtName.GetComponent<Text>().text = data.heroName;
        }

        private void SetSelfWeaponUI(int id)
        {
            WeanponUnit data = gamelogic.GetWeaponInfo(id);
            Transform ImgHead = GetWMNode(WN_PNL_UISelfPlayer).Find("IMG_HeroHead");
            Transform TxtName = GetWMNode(WN_PNL_UISelfPlayer).Find("TXT_HeroName");
            ImgHead.GetComponent<Image>().sprite = data.sprite;
            TxtName.GetComponent<Text>().text = data.weaponName;

        }

        private void SetWeaponInfo(List<SkillUnit> skillUnits)
        {
            Transform weaponInfoNode = GetWMNode(WN_PNL_Weapon_Info);
            Transform txtInfoNode = weaponInfoNode.Find("Text");
            weaponInfoNode.gameObject.SetActive(false);
            //foreach(var temp in skillUnits)
            //{
            //    string.Format()
            //}
        }

        private void OnClickUpdateReadyState()
        {
            Text textNode = GetWMNode(WN_BTN_UpdateReadyState).Find("Text").GetComponent<Text>();
            if (textNode.text == "Ready")
            {
                if (!GameController.GetInstance().GetLineNetState())
                {
                    this.Close();
                    GameController.GetInstance().GetLogic<PVPGameLogic>().StartGame();
                    PVPGamePlayerLogic selfplayerlogic = GameController.GetInstance().GetHero<PVPGamePlayerLogic>();
                    selfplayerlogic.GetView().SetLocalPlayerData(heroId, weaponId);
                    return;
                }
                textNode.text = "Cancel";
                return;
            }
            textNode.text = "Ready";
        }
    }
}
