using UnityEngine;

namespace Scripts.Logic
{
    //Ejection     : EJE|(speed)|(acceleration)|(locktype)
    //Magic Circle : MCR|()



    //public enum MagicType
    //{
    //    MAGIC,        
    //    EQUIPMENT,    
    //    FUZHU,        
    //    //Buff,
    //    //DeBuff,
    //}
    
    //public enum UseEndType
    //{
    //    ONCE,         //单次消耗
    //    UNLIMITED,    //不限次数

    //}

    public enum UseConditionType
    {
        NULL,
        MP,
        HP,
    }

    public abstract class Card : MonoBehaviour
    {
        //protected MagicType cardType;
        //protected UseEndType useEndType;
        protected UseConditionType useConditionType;
        //protected string cardInfo;
        protected float liveTime;
    }
}
