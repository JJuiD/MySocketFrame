﻿using System.Collections.Generic;
using UnityEngine;

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

    public class PVPGamePlayer : MonoBehaviour
    {
        private bool isDestory = true;
        public void Create()
        {
            ResetData();
            //GameObject.Instantiate(this.gameObject, pos, Quaternion.identity);
            playerCollider = this.GetComponent<PVPGamePlayerCollider>();
            playerBodyTrans = this.transform.Find("Bone_Body");
            playerAnimation = this.GetComponent<Animation>();
            isDestory = false;
        }

        #region 碰撞相关
        private PVPGamePlayerCollider playerCollider;

        #endregion

        #region 玩家游戏数据
        private WeanponUnit playerWeapon = new WeanponUnit();
        private HeroUnit playerHero = new HeroUnit();
        public void SetLocalPlayerData(int heroId,int weaponId)
        {
            playerWeapon = GameController.GetInstance().GetLogic<PVPGameLogic>().GetWeaponInfo(weaponId);
            playerHero = GameController.GetInstance().GetLogic<PVPGameLogic>().GetHeroInfo(heroId);
        }
        public bool IsAllowCost(CostType type, float value)
        {
            switch (type)
            {
                case CostType.mp:
                    if (playerHero.mp >= value + playerHero.costMp) return true;
                    break;
                case CostType.hp:
                    if (playerHero.hp > value + playerHero.costHp) return true;
                    break;
            }

            return false;
        }
        public void SetCost(CostType type, float value)
        {
            if (!IsAllowCost(type, value)) return;
            switch (type)
            {
                case CostType.mp:
                    playerHero.costMp += value;
                    break;
                case CostType.hp:
                    playerHero.costHp += value;
                    break;
            }
        }
        public bool IsDied()
        {
            return (playerHero.hp > playerHero.costHp);
        }
        public void ResetCost()
        {
            playerHero.costHp = 0;
            playerHero.costMp = 0;
        }
        #endregion

        public void TickUpdate()
        {
            if (isDestory)
            {

            }
            else
            {
                if (beginReadySkillTime != 0 
                    && Time.time - beginReadySkillTime > PVPGameConfig.SKILL_OUTTIME)
                {
                    ClearSkillKeyDic();
                }
            }
        }

        public void DealKeyUnit(List<KeyUnit> units)
        {
            bool isDefence = false;
            bool isJump = false;
            bool isAttack = false;
            Vector2 moveDirection = Vector2.zero;

            foreach (var unit in units)
            {
                switch (unit.eventName)
                {
                    case PVPGameConfig.KEY_EVENT_DEFENCE:
                        isDefence = unit.isDown;
                        break;
                    case PVPGameConfig.KEY_EVENT_UP:
                        moveDirection.y += unit.isDown ? 1 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_LEFT:
                        moveDirection.x += unit.isDown ? -1 : 0;
                        moveDirection.x += unit.IsClickDouble() ? -1 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_DOWN:
                        moveDirection.y += unit.isDown ? -1 : 0;
                        break;
                    case PVPGameConfig.KEY_EVENT_RIGHT:
                        moveDirection.x += unit.isDown ? 1 : 0;
                        moveDirection.x += unit.IsClickDouble() ? 1 : 0;
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
            //  if (skillId >= 0) { gameScene.ExcuteSkillAnimation(skillId); return; }
            if (isDefence) { ExcuteDefenceAnimation(); return; }
            else if (isAttack) { ExcuteAttackAnimation(); return; }
            else if (isJump) { ExcuteJumpAnimation(); return; }
            else if (moveDirection != Vector2.zero)
            {
                ExcuteWalkAnimaion(moveDirection);
                return;
            }

            ExcuteIdelAnimation();
        }

        public void ExcuteIdelAnimation()
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SetPlayerGameState(PlayerGameState.idel);
        }

        private Transform playerBodyTrans;
        private Animation playerAnimation;
        public void ExcuteWalkAnimaion(Vector2 moveDirection)
        {
            //  2   3   4
            // -1   0   1
            // -4  -3  -2
            string dirEventType = "";
            bool isRun = false;
            bool isFowardToRight = playerBodyTrans.eulerAngles.x != 180;
            if (moveDirection.y > 0)
            {
                dirEventType = PVPGameConfig.KEY_EVENT_UP;
            }else if(moveDirection.x > 0)
            {
                dirEventType = isFowardToRight ? PVPGameConfig.KEY_EVENT_RIGHT: PVPGameConfig.KEY_EVENT_LEFT;
                isRun = moveDirection.x > 1;
                playerBodyTrans.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if(moveDirection.x < 0)
            {
                dirEventType = isFowardToRight ? PVPGameConfig.KEY_EVENT_LEFT : PVPGameConfig.KEY_EVENT_RIGHT;
                isRun = moveDirection.x < -1;
                playerBodyTrans.rotation = Quaternion.Euler(0,180,90);
            }
            else if(moveDirection.y < 0)
            {
                dirEventType = PVPGameConfig.KEY_EVENT_DOWN;
            }
            if (DealSkillCombo(dirEventType)) return;
            Vector2 targetPos = new Vector2(moveDirection.x + transform.position.x
                , moveDirection.y + transform.position.y) * playerHero.speed;
            this.GetComponent<Rigidbody2D>().velocity = moveDirection * playerHero.speed;
            //this.GetComponent<Rigidbody2D>().MovePosition(targetPos);
            //this.transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

            if (isRun)
            {
                SetPlayerGameState(PlayerGameState.run);
            }
            else
            {
                SetPlayerGameState(PlayerGameState.walk);
            }
        }
        public void ExcuteDefenceAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_DEFENCE)) return;
            SetPlayerGameState(PlayerGameState.defence);
        }
        public void ExcuteAttackAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_ATTACK)) return;

            SetPlayerGameState(PlayerGameState.attack);
        }
        public void ExcuteJumpAnimation()
        {
            if (DealSkillCombo(PVPGameConfig.KEY_EVENT_JUMP)) return;

            SetPlayerGameState(PlayerGameState.jump);
        }
        public void ExcuteSkillAnimation(int id)
        {
            Debug.Log("ExcuteSkillAnimation " + id);
            ClearSkillKeyDic();
        }
        #region 技能逻辑判断
        //防御键 + 方向 + 攻击 + 攻击(...)
        //连招id,符合个数
        Dictionary<int, int> skillClickKeyCount = new Dictionary<int, int>();
        private float beginReadySkillTime = 0;
        public bool DealSkillCombo(string keyEventType)
        {
            if(skillClickKeyCount.Count == 0)
            {
                if (keyEventType != PVPGameConfig.KEY_EVENT_DEFENCE) return false;
                foreach (var temp in playerWeapon.skills)
                {
                    skillClickKeyCount.Add(temp.Key,0);
                }
                beginReadySkillTime = Time.time;
            }
            else
            {
                if(Time.time - beginReadySkillTime > PVPGameConfig.SKILL_OUTTIME)
                {
                    ClearSkillKeyDic();
                    return false;
                }

                List<int> addList = new List<int>();
                List<int> delList = new List<int>();
                foreach(var temp in skillClickKeyCount)
                {
                    SkillUnit skill = playerWeapon.skills[temp.Key];
                    if (temp.Value >= 1 && keyEventType == skill.keys[temp.Value - 1]) continue;
                    if (keyEventType == skill.keys[temp.Value])
                    {
                        if(temp.Value == skill.keys.Count
                            && IsAllowCost(skill.costType,skill.cost))
                        {
                            ExcuteSkillAnimation(skill.id);
                            return true;
                        }
                        else addList.Add(temp.Key);
                        Debug.Log(temp.Key + " add " + keyEventType);
                    }
                    else
                    {
                        delList.Add(temp.Key);
                    }
                }
                for(int i = 0;;++i)
                {
                    if (i >= delList.Count && i >= addList.Count) break;
                    if (addList.Count > i) ++skillClickKeyCount[addList[i]];
                    if (delList.Count > i) skillClickKeyCount.Remove(delList[i]);
                }
            }
            return false;
        }
        public void ClearSkillKeyDic()
        {
            skillClickKeyCount.Clear();
            beginReadySkillTime = 0;
        }
        #endregion

        #region PlayerGameState
        private PlayerGameState playerGameState;
        public PlayerGameState GetPlayerGameState()
        {
            return playerGameState;
        }
        public void SetPlayerGameState(PlayerGameState state)
        {
            playerGameState = state;
        }
        #endregion


        public void ResetData()
        {
            isDestory = true;
            playerWeapon = new WeanponUnit();
            playerHero = new HeroUnit();
            skillClickKeyCount = new Dictionary<int, int>();
            beginReadySkillTime = 0;
            playerGameState = PlayerGameState.idel;
        }
    }
}
