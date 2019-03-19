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
            Debug.Log("ExcuteIdelAnimation");
        }
        public void ExcuteMoveAnimaion()
        {
            Debug.Log("ExcuteMoveAnimaion");
        }
        public void ExcuteDefenceAnimation()
        {
            Debug.Log("ExcuteDefenceAnimation");
        }
        public void ExcuteAttackAnimation()
        {
            Debug.Log("ExcuteAttackAnimation");
        }
        public void ExcuteJumpAnimation()
        {
            Debug.Log("ExcuteJumpAnimation");
        }
        public void ExcuteSkillAnimation(int id)
        {
            Debug.Log("ExcuteSkillAnimation " + id);
        }

    }
}
