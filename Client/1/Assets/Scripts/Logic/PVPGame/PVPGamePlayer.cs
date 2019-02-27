using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic.PVPGame
{

    //enum ColliderType
    //{
    //    Tolerant,
    //    Disjoint,
    //    Overlap,
    //}

    //public class BoxColliderPlayer
    //{
    //    private Vector2 pos;
    //    private Vector2 topLefPos;
    //    private Vector2 topRigPos;
    //    private Vector2 btmLefPos;
    //    private Vector2 btmRigPos;

    //    private GameObject collider;
    //    private Texture texture;

    //    public Vector2 GetTopLefPos() { return topLefPos; }
    //    public Vector2 GetBtmRigPos() { return btmRigPos; }

    //    public BoxColliderPlayer(GameObject gameObject)
    //    {
    //        this.collider = gameObject;
    //        this.texture = collider.GetComponent<Image>().sprite.texture;
    //        pos = gameObject.transform.position;
    //        topLefPos = new Vector2(pos.x - texture.width / 2, pos.y + texture.height / 2);
    //        topRigPos = new Vector2(pos.x + texture.width / 2, pos.y + texture.height / 2);
    //        btmLefPos = new Vector2(pos.x - texture.width / 2, pos.y - texture.height / 2);
    //        btmRigPos = new Vector2(pos.x + texture.width / 2, pos.y - texture.height / 2);
    //        //gameObject.transform.GetComponent<Image>().sprite.texture.width;
    //        //topLefPos = new Vector2(pos.x - )
    //    }

    //    public bool IsCollider(GameObject gameObject)
    //    {
    //        Vector2 targetPos = gameObject.transform.position;
    //        Texture targetTexture = gameObject.GetComponent<Image>().sprite.texture;

    //        float dis_width = Math.Abs(targetPos.x - pos.x);
    //        float dis_height = Math.Abs(targetPos.y - pos.y);
    //        if (dis_width < targetTexture.width/2 + texture.width/2
    //            && dis_width < targetTexture.width / 2 + texture.width / 2)
    //        {
    //            return true;
    //        }

    //        return false;
    //        //float dis = (dis_width + dis_height) * (dis_width + dis_height) - 2 * dis_width * dis_height
    //    }
    //}

    public class PVPGamePlayer : BasePlayerLogic
    {
        private PlayerInfo playerInfo = new PlayerInfo();
        private WeanponUnit playerWeapon = new WeanponUnit();
        private HeroUnit playerHero = new HeroUnit();

        public void SetServerPlayerData(PlayerInfo playerinfo)
        {
            this.playerInfo.name = playerinfo.name;
            this.playerInfo.seat = playerinfo.seat;
            this.playerInfo.localSeat = playerinfo.localSeat;
            this.playerInfo.SetPlayerState(PlayerGameState.FREE);
        }
        public void SetLocalPlayerData(int heroId,int weaponId)
        {
            playerWeapon = GameController.GetInstance().GetLogic<PVPGameLogic>().GetWeaponInfo(weaponId);
            playerHero = GameController.GetInstance().GetLogic<PVPGameLogic>().GetHeroInfo(heroId);
        }

        public Int16 GetServerSeat() { return playerInfo.seat; }
        public Int16 GetLocalSeat() { return playerInfo.localSeat; }

        

        public void DealKeyUnit(List<KeyUnit> units)
        {
            bool isFowardToRight = false ;
            bool isDefence = false;
            bool isJump = false;
            bool isAttack = false;
            int dir = 0;

            foreach (var unit in units)
            {
                //  2   3   4
                // -1   0   1
                // -4  -3  -2
                switch (unit.eventName)
                {
                    case PVPGameConfig.KEY_EVENT_DEFENCE:
                        if (unit.isDown)
                        {
                            isDefence = true;
                        }
                        else
                        {
                            isDefence = false;
                        }
                        break;
                    case PVPGameConfig.KEY_EVENT_UP:
                        dir += unit.isDown == true ? 3 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_LEFT:
                        dir += unit.isDown == true ? -1 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_DOWN:
                        dir += unit.isDown == true ? -3 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_RIGHT:
                        dir += unit.isDown == true ? 1 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_ATTACK:
                        isAttack = unit.isDown;
                        break;
                    case PVPGameConfig.KEY_EVENT_JUMP:
                        isJump = unit.isDown;
                        break;
                }
            }
            
           // int skillId = DealSkillCombo();
          //  if (skillId >= 0) { gameScene.RunSkillAnimation(skillId); return; }
            if(isJump) { RunJumpAnimation(); return; }
            else if (isAttack) { RunAttackAnimation(); return; }
            else if (isDefence) { RunDefenceAnimation(); return; }
            else if (dir != 0) { RunWalkAnimaion(isFowardToRight,dir); return; }

            RunIdelAnimation();
        }

        public void RunIdelAnimation()
        {

        }

        public void RunWalkAnimaion(bool isTolef, int dir)
        {

        }

        public void RunDefenceAnimation()
        {

        }

        public void RunAttackAnimation()
        {

        }

        public void RunJumpAnimation()
        {

        }

        public void RunSkillAnimation(int id)
        {

        }

        //防御键 + 方向 + 攻击 + 攻击(...)
        Dictionary<int, int> SkillClickKeyCount = new Dictionary<int, int>();
        public int DealSkillCombo()
        {

            return -1;
        }
        
    }
}
