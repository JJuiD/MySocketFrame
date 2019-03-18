using Scripts.TwoDimensiona;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.GP
{

    public class GPPlayer : MonoBehaviour
    {
        GPPhysicalGlobal playerPhysical;
        public void Create()
        {
            ResetData();
            //GameObject.Instantiate(this.gameObject, pos, Quaternion.identity);
            playerCollider = this.GetComponent<GPPlayerCollider>();
            playerPhysical = this.GetComponent<GPPhysicalGlobal>();
            playerAnimation = this.GetComponent<Animation>();
            PhysicalEngineWord.GetInstance().AddPhysicalGlobal(playerPhysical);
            playerPhysical.InitRealPos(this.transform.position);
        }

        #region 碰撞相关
        private GPPlayerCollider playerCollider;
        #endregion

        public GPPhysicalGlobal GetPhySical()
        {
            return playerPhysical;
        }

        public void ExcuteIdelAnimation()
        {
            playerPhysical.SetVelocity(Vector2.zero);
        }

        //private Transform playerBodyTrans;
        private Animation playerAnimation;
        public void ExcuteWalkAnimaion(Vector2 moveDirection,float speed)
        {
            //  2   3   4
            // -1   0   1
            // -4  -3  -2
            bool isRun = false;
            
            if (moveDirection.y > 0)
            {
                
            }else if(moveDirection.x > 0)
            {
                isRun = moveDirection.x > 1;
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(moveDirection.x < 0)
            {
                isRun = moveDirection.x < -1;
                this.transform.rotation = Quaternion.Euler(0,180,0);
            }
            else if(moveDirection.y < 0)
            {
                
            }

            Vector2 targetPos = new Vector2(moveDirection.x + transform.position.x
                , moveDirection.y + transform.position.y) * speed;
            playerPhysical.SetVelocity(moveDirection * speed * Time.fixedDeltaTime);
            // this.GetComponent<Rigidbody2D>().velocity = moveDirection * playerHero.speed;
            //this.GetComponent<Rigidbody2D>().MovePosition(targetPos);
            //this.transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

        }
        public void ExcuteDefenceAnimation()
        {

        }
        public void ExcuteAttackAnimation()
        {

        }
        
        public void ExcuteJumpAnimation()
        {
            playerPhysical.AddVelocity(new _Vector3(0, 0, 2.5f));
            
        }

        public void ExcuteSkillAnimation(int id)
        {
            Debug.Log("ExcuteSkillAnimation " + id);
        }

        public void ResetData()
        {
            //isDestory = true;
            //playerWeapon = new WeanponUnit();
            //playerHero = new HeroUnit();
            //skillClickKeyCount = new Dictionary<int, int>();
            //beginReadySkillTime = 0;
        }
    }
}
