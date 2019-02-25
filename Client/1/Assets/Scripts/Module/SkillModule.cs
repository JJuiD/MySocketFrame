using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Module.Skill
{
    //准备->获取技能预制件->释放

    #region Trigger
    public abstract class Trigger
    {
        public float livetime = 0;
        public float starttime = 0;
        public GameObject gameobject;
        public abstract void Excute();
        public virtual bool isDelete(float curtime)
        {
            if (curtime > starttime + livetime) { return true; }
            return false;
        }
        public void Create() { }
        public Trigger() { }
    }

    #region Ejection
    public enum LOCKTYPE
    {
        AUTO,
        HALFAUTO,
        NULL,
    }
    public class Ejection : Trigger
    {
        LOCKTYPE locktype;
        float speed;
        float elivetime;
        Vector3 targetPos;

        public void Create(float speed, LOCKTYPE locktype = LOCKTYPE.NULL,
            Vector3 pos = new Vector3(), float elivetime = 0)
        {
            this.locktype = locktype;
            this.speed = speed;
            this.elivetime = elivetime;
            this.targetPos = pos;
        }

        public override void Excute()
        {
            float step = speed * Time.deltaTime;
            switch (locktype)
            {
                case LOCKTYPE.NULL:
                    gameobject.transform.Translate(Vector3.forward * step);
                    break;
                case LOCKTYPE.AUTO:
                    gameobject.transform.localPosition = Vector3.MoveTowards(gameobject.transform.localPosition, targetPos, step);
                    break;
            }

        }
    }
    #endregion

    public class Break : Trigger
    {
        public override void Excute()
        {
            
        }
    }

    #endregion

    #region Skill && SkillConfig
    public enum SkillConfig
    {
        FireBall,
    }
    public class Skill : MonoBehaviour
    {
        //释放条件
        public Func<bool> bUseFunc = delegate () { return true; };

        private float livetime = 0;
        private float singtime = 0;
        private Dictionary<float, List<Trigger>> TimeAxis;
        private float curtime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="livetime"></param>
        /// <param name="singtime"></param>
        public Skill(float livetime, float singtime)
        {
            TimeAxis = new Dictionary<float, List<Trigger>>();
            curtime = 0;
            this.livetime = livetime;
            this.singtime = singtime;
        }

        public T Add<T>(float starttime,float livetime) where T:Trigger,new()
        {
            T trigger = new T();
            trigger.gameobject = this.gameObject;
            trigger.starttime = starttime;
            trigger.livetime = livetime;
            if(TimeAxis.ContainsKey(starttime))
            {
                TimeAxis[starttime].Add(trigger);
            }
            else
            {
                List<Trigger> triggers = new List<Trigger>();
                triggers.Add(trigger);
                TimeAxis.Add(starttime, triggers);
            }
            return trigger;
        }

        public void FixedUpdate()
        {
            curtime += Time.fixedTime;
            if (curtime > livetime) { Destroy(this.gameObject); return; }
            if(TimeAxis.Count != 0)
            {
                List<int> deleteTrigger = new List<int>();
                foreach(var temp in TimeAxis)
                {
                    if (curtime > temp.Key)
                    {
                        deleteTrigger.Clear();
                        for (int i = 0;i < temp.Value.Count;++i)
                        {
                            if(!temp.Value[i].isDelete(curtime))
                            {
                                temp.Value[i].Excute();
                            }
                            else
                            {
                                deleteTrigger.Add(i);
                            }
                        }

                        if (deleteTrigger.Count > 0)
                        {
                            foreach(int index in deleteTrigger)
                            {
                                temp.Value.RemoveAt(index);
                            }
                        }
                    }
                }
            }
        }
    }

    #endregion

    public class SkillModule : Singleton<SkillModule>
    {
        private Dictionary<SkillConfig, Skill> dic_acts = new Dictionary<SkillConfig, Skill>();
        
        /// <summary>
        /// 需要设置释放条件,目标对象
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject Use(SkillConfig index,Func<bool> func, GameObject targetObject = null)
        {
            return new GameObject();
        }

        public SkillModule()
        {
            Skill skill;
            #region Skill ID : 0
            skill = new Skill(20,0.5f);
            skill.Add<Ejection>(0, 20).Create(5.0f);
            dic_acts.Add(SkillConfig.FireBall, skill);
            #endregion
        }
    }
}

