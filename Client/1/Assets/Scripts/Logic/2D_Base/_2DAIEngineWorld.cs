using Scripts.Logic.GP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic._2D_Base
{
    public enum AIState
    {
        Idel , //保持不动
        Attack, //进攻
        Watch, //观察
    }

    public abstract class AIGlobalBase : MonoBehaviour
    {
        public AIState state = AIState.Idel;
        public float watchTime = 2;
    }

    public class _2DAIEngineWorld : Singleton<_2DAIEngineWorld>
    {
        List<GameObject> targets = new List<GameObject>();
        public void AddTarget(GameObject target)
        {
            if (!targets.Contains(target))
            {
                targets.Add(target);
            }
        }

        public void FixedUpdate()
        {

        }
    }
}
