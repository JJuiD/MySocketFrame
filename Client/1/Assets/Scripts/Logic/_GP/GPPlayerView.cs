using Scripts.Logic._2D_Base;
using Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.GP
{
    public class GPPlayerView : MonoBehaviour
    {
        public void Init()
        {
            playerAnimation = this.GetComponent<Animation>();
        }

        private Animation playerAnimation;
        public void ExcuteIdelAnimation()
        {
        }
        public void ExcuteMoveAnimaion()
        {
        }
        public void ExcuteDefenceAnimation()
        {
        }
        public void ExcuteAttackAnimation()
        {
        }
        public void ExcuteJumpAnimation()
        {
        }
        public void ExcuteSkillAnimation(int id)
        {
            Debug.Log("ExcuteSkillAnimation " + id);
        }

    }
}
