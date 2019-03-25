using Proto.GPGameProto;
using Scripts.Logic;
using Scripts.Logic.GP;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.GP
{
    public class UIGPInit : BaseUI
    {
        private string WN_PNL_UISelfPlayer = "WN_PNL_UISelfPlayer";
        private string WN_BTN_UpdateReadyState = "WN_BTN_UpdateReadyState";
        private string WN_PNL_Weapon_Info = "WN_PNL_Weapon_Info";

        private GPLogic gamelogic;
        private int heroId = 0;
       // private int weaponId = 0;

        public override void Open(params object[] _params)
        {
            heroId = DataCenter.GetInstance().GetUserDefault().GetUserDefaultValue<int>("DEFAULT_HERO");
           // weaponId = DataCenter.GetInstance().GetUserDefault().GetUserDefaultValue<int>("DEFAULT_WEAPON");
            gamelogic = GameController.GetInstance().Logic as GPLogic ;
            SetSelfHeroUI(heroId);
            //SetSelfWeaponUI(weaponId);
            UIManager.GetInstance().RegisterClickEvent(WN_BTN_UpdateReadyState, this, OnClickUpdateReadyState);
        }

        private void SetSelfHeroUI(int id)
        {
            HeroInfo data = gamelogic.GetHeroInfo(id);
            Transform ImgHead = GetWNode(WN_PNL_UISelfPlayer).Find("IMG_HeroHead");
            Transform TxtName = GetWNode(WN_PNL_UISelfPlayer).Find("TXT_HeroName");
            ImgHead.GetComponent<Image>().sprite = data.headImage;
            TxtName.GetComponent<Text>().text = data.heroName;
        }

        //private void SetSelfWeaponUI(int id)
        //{
        //    WeanponUnit data = gamelogic.GetWeaponInfo(id);
        //    Transform ImgHead = GetWNode(WN_PNL_UISelfPlayer).Find("IMG_HeroHead");
        //    Transform TxtName = GetWNode(WN_PNL_UISelfPlayer).Find("TXT_HeroName");
        //    ImgHead.GetComponent<Image>().sprite = data.sprite;
        //    TxtName.GetComponent<Text>().text = data.weaponName;
        //}

        private void SetWeaponInfo(List<SkillInfo> SkillInfos)
        {
            Transform weaponInfoNode = GetWNode(WN_PNL_Weapon_Info);
            Transform txtInfoNode = weaponInfoNode.Find("Text");
            weaponInfoNode.gameObject.SetActive(false);
            //foreach(var temp in SkillInfos)
            //{
            //    string.Format()
            //}
        }

        private void OnClickUpdateReadyState()
        {
            Text textNode = GetWNode(WN_BTN_UpdateReadyState).Find("Text").GetComponent<Text>();
            CellUpdatePlayerState cell = new CellUpdatePlayerState();
            cell.SaveData(GameController.GetInstance().Local2Tag(0)
                , textNode.text == "Ready"? PlayerState.READY : PlayerState.READY_NULL);
            gamelogic.heroID = (uint)heroId;
            gamelogic.SendGamePacket(cell);
            if (GameController.GetInstance().GetLineNetState())
            {
                this.Close();
                return;
            }
            gamelogic.AddPortocolListen(OnReceivePacket,Proto.ProtoCommand.ProtoCommand_Game);
        }

        public bool OnReceivePacket(object _object, byte[] buffer)
        {
            if ((GPGameCMD)_object != GPGameCMD.CMD_GAME_UPDATEPLAYERSTATE) return false;
            CellUpdatePlayerState cell = new CellUpdatePlayerState();
            GPCMD_UpdatePlayerState data = cell.ParseData<GPCMD_UpdatePlayerState>(buffer);
            GameController.GetInstance().UpdatePlayerState(data.seat,data.state);
            bool isAllReady = true;
            for(uint tag = 0; tag < GameController.GetInstance().GetPlayerCount();++tag)
            {
                isAllReady = isAllReady
                    && (GameController.GetInstance().GetPlayerByTag(0).state == (uint)PlayerState.READY);
            }
            if (isAllReady) this.Close();
            else
            {
                Text textNode = GetWNode(WN_BTN_UpdateReadyState).Find("Text").GetComponent<Text>();
                textNode.text = textNode.text == "Ready" ? "Cancel" : "Ready";
            }
            return true;
            
        }

    }
}
