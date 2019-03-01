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

    public class PVPGamePlayer : BasePlayer
    {
        public void ResetData()
        {
            isDestory = true;
            playerWeapon = new WeanponUnit();
            playerHero = new HeroUnit();
            SkillClickKeyCount = new Dictionary<int, int>();
        }

        private bool isDestory = true;
        public void Create(Vector3 pos)
        {
            if (isDestory) return;
            GameObject.Instantiate(this.gameObject, pos, Quaternion.identity);
            isDestory = false;
        }

        #region 玩家游戏数据
        private WeanponUnit playerWeapon = new WeanponUnit();
        private HeroUnit playerHero = new HeroUnit();
        public void SetLocalPlayerData(int heroId,int weaponId)
        {
            playerWeapon = GameController.GetInstance().GetLogic<PVPGameLogic>().GetWeaponInfo(weaponId);
            playerHero = GameController.GetInstance().GetLogic<PVPGameLogic>().GetHeroInfo(heroId);
        }
        #endregion


        public bool IsAllowSkillCost(CostType type,float value)
        {
            switch(type)
            {
                case CostType.mp:
                    if (playerHero.mp >= value) return true;
                    break;
                case CostType.hp:
                    if (playerHero.hp >= value) return true;
                    break;
            }

            return false;
        }

        public void SetSkillCost(CostType type,float value)
        {
            if (!IsAllowSkillCost(type, value)) return;
            switch (type)
            {
                case CostType.mp:
                    playerHero.mp -= value;
                    break;
                case CostType.hp:
                    playerHero.hp -= value;
                    break;
            }
        }

        public void DealKeyUnit(List<KeyUnit> units)
        {
            bool isFowardToRight = false ;
            bool isDefence = false;
            bool isJump = false;
            bool isAttack = false;
            int dir = 0;

            foreach (var unit in units)
            {
                
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
            if (isDefence) { RunDefenceAnimation(); return; }
            else if (isAttack) { RunAttackAnimation(); return; }
            else if (isJump) { RunJumpAnimation(); return; }
            else if (dir != 0) { RunWalkAnimaion(isFowardToRight,dir); return; }

            RunIdelAnimation();
        }

        public void RunIdelAnimation()
        {
            SkillClickKeyCount.Clear();
        }

        public void RunWalkAnimaion(bool isFowardToRight, int dir)
        {
            //  2   3   4
            // -1   0   1
            // -4  -3  -2
            string dirEventType = "";
            if(dir == 3 || dir == 2 || dir == 4 )
            {
                dirEventType = PVPGameConfig.KEY_EVENT_UP;
            }else if(dir == 1 || dir == -2 )
            {
                dirEventType = isFowardToRight ? PVPGameConfig.KEY_EVENT_RIGHT: PVPGameConfig.KEY_EVENT_LEFT;
            }else if(dir == -1 || dir == -4)
            {
                dirEventType = isFowardToRight ? PVPGameConfig.KEY_EVENT_LEFT : PVPGameConfig.KEY_EVENT_RIGHT;
            }
            else if(dir == -3)
            {
                dirEventType = PVPGameConfig.KEY_EVENT_DOWN;
            }
            if(DealSkillCombo(dirEventType)) return;
        }

        public void RunDefenceAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_DEFENCE)) return;
        }

        public void RunAttackAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_DEFENCE)) return;
        }

        public void RunJumpAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_DEFENCE)) return;
        }

        public void RunSkillAnimation(int id)
        {
            SkillClickKeyCount.Clear();
        }

        //防御键 + 方向 + 攻击 + 攻击(...)
        //连招id,符合个数
        Dictionary<int, int> SkillClickKeyCount = new Dictionary<int, int>();
        public bool DealSkillCombo(string keyEventType)
        {
            if(SkillClickKeyCount.Count == 0)
            {
                foreach (var temp in playerWeapon.skills)
                {
                    SkillClickKeyCount.Add(temp.Key,0);
                    //if (SkillClickKeyCount.ContainsKey(temp.Key))
                    //{
                    //    int index = SkillClickKeyCount[temp.Key];
                    //    if (keyEventType == temp.Value.keys[index]
                    //        && index == temp.Value.keys.Count
                    //        && IsAllowSkillCost(temp.Value.costType, temp.Value.cost))
                    //    {
                    //        //释放技能成功
                    //    }
                    //}
                }
            }
            else
            {
                List<int> addList = new List<int>();
                List<int> delList = new List<int>();
                foreach(var temp in SkillClickKeyCount)
                {
                    SkillUnit skill = playerWeapon.skills[temp.Key];
                    if (keyEventType == skill.keys[temp.Value])
                    {
                        if(temp.Value == skill.keys.Count
                            && IsAllowSkillCost(skill.costType,skill.cost))
                        {
                            RunSkillAnimation(skill.id);
                            return true;
                        }
                        else addList.Add(temp.Key);
                    }
                    else
                    {
                        delList.Add(temp.Key);
                    }
                }
                for(int i = 0;;++i)
                {
                    if (delList.Count + addList.Count == 0) break;
                    ++SkillClickKeyCount[addList[i]];
                    SkillClickKeyCount.Remove(delList[i]);
                }
            }
            return false;
        }
    }
}
