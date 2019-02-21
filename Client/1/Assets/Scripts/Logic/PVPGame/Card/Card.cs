using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
    //卡牌类型
    public enum MagicType
    {
        MAGIC,        //魔法
        EQUIPMENT,    //装备
        FUZHU,        //辅助
        //Buff,
        //DeBuff,
    }
    
    //使用类型
    public enum UseEndType
    {
        ONCE,         //单次消耗
        UNLIMITED,    //不限次数

    }

    public enum UseConditionType
    {
        NULL,
        MP,
        HP,
    }

    public abstract class Card : MonoBehaviour
    {
        protected MagicType cardType;
        protected UseEndType useEndType;
        protected UseConditionType useConditionType;
        protected string cardInfo;
    }
}
