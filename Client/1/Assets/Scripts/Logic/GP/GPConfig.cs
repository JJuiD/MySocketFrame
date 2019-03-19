using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic.GP
{
    // 名字: 
    // 描述: 类小朋友齐打交游戏 
    // 标签: 格斗,联机
    public static class GPConfig
    {
        //事件名
        public const string KEY_UP = "KEY_UP";
        public const string KEY_RIGHT = "KEY_RIGHT";
        public const string KEY_LEFT = "KEY_LEFT";
        public const string KEY_DOWN = "KEY_DOWN";

        public const string KEY_ATTACK = "KEY_ATTACK";
        public const string KEY_JUMP = "KEY_JUMP";
        public const string KEY_DEFENCE = "KEY_DEFENCE";

        public const int HERO_EMOTICONS_MAX_COUNT = 6;
        public const float SKILL_OUTTIME = 5f;
        public const float GAME_GRAVITY = 10f;
        public const float JUMP_FORCE = 12f;
    }

    public enum KeyByte : byte
    {
        KEY_NULL = 0,
        KEY_UP = 1,
        KEY_RIGHT = 2,
        KEY_LEFT = 4,
        KEY_DOWN = 8,

        KEY_ATTACK = 16,
        KEY_JUMP = 32,
        KEY_DEFENCE = 64,
    }


    //表情
    public enum EmoticonType
    {
        pain,
        happy,
        bored,
        idel,
        greet,
        angry,
    }

    public enum CostType
    {
        hp,
        mp,
    }

    public class ClickKey
    {
        private byte lastKey;
        private byte curKey;
        private byte topKey;

        public ClickKey() { ResetData(); }

        public void ResetData()
        {
            lastKey = (byte)KeyByte.KEY_NULL;
            curKey = (byte)KeyByte.KEY_NULL;
            topKey = (byte)KeyByte.KEY_NULL;
            doubleLastKey = (byte)KeyByte.KEY_NULL;
            lerpTime = 0;
            downtime = 0;
        }

        public void StartSetKey() { lastKey = topKey; topKey = 0; curKey = 0; }
        public void SetKey(bool isDown, KeyByte keyByte, bool isAdd = false)
        {
            if (isDown)
            {
                topKey = (byte)keyByte;
                if (isAdd) curKey += (byte)keyByte;
                else curKey = (byte)keyByte;
            }
        }
        public void EndSetKey()
        {
            if(lastKey == 0 && topKey != 0)
            {
                doubleLastKey = topKey;
                lerpTime = Time.time - downtime;
                downtime = Time.time;
            }
        }

        public byte GetKey() { return curKey; }
        public byte GetLastKey() { return lastKey; }
        public byte GetTopKey() { return topKey; }

        private byte doubleLastKey = 0;
        private float lerpTime = 0;
        private float downtime = 0;
        public bool IsClickDouble()
        {
            return lerpTime > 0 && lerpTime < 0.5f && ((curKey & doubleLastKey) == doubleLastKey) ;
        }
    }

    public class SkillUnit
    {
        public string skillName = "";
        public float damage = 0;
        public float cost = 0;
        public CostType costType;
        public int id = 0;
        public List<string> keys = new List<string>();

        public void ReadStream(Dictionary<string, string> data, int id)
        {
            this.id = id;
            skillName = data["skillName"];
            float.TryParse(data["damage"], out this.damage);
            keys = new List<string>(data["key"].Split('|'));
            float.TryParse(data["cost"], out this.cost);
            this.costType = (CostType)Enum.Parse(typeof(CostType), data["costType"]);
        }
    }

    public class WeanponUnit
    {
        public string weaponName = "";
        public float damage = 3;
        public int id = 0;
        public Sprite sprite;
        public Dictionary<int,SkillUnit> skills = new Dictionary<int, SkillUnit>();

        public void ReadStream(Dictionary<string, string> data, int id)
        {
            this.id = id;
            weaponName = data["weaponName"];
            float.TryParse(data["damage"], out this.damage);
            sprite = Resources.Load<Sprite>(data["path"]);
        }
    }

    public class HeroUnit
    {
        public string heroName = "";
        public float hp = 0;
        public float costHp = 0;
        public float costMp = 0;
        public float mp = 0;
        public float mprcvrate = 0;
        public float speed = 0;
        public int id = 0;
        public float attack = 0;
        public Dictionary<EmoticonType, Sprite> emoticons;
        public Sprite headImage;

        public void ReadStream(Dictionary<string,string> data,int id)
        {
            this.id = id;
            heroName = data["heroName"];
            float.TryParse(data["hp"], out this.hp);
            float.TryParse(data["mp"], out this.mp);
            float.TryParse(data["speed"], out this.speed);
            float.TryParse(data["attack"], out this.attack);
            float.TryParse(data["mprcvrate"], out this.mprcvrate);
            Sprite[] sprites = Resources.LoadAll<Sprite>(data["heroSpritePath"]);
            emoticons = new Dictionary<EmoticonType, Sprite>();
            for (int i = 0;i < GPConfig.HERO_EMOTICONS_MAX_COUNT;++i)
            {
                emoticons.Add((EmoticonType)i, sprites[i]);
            }
            headImage = sprites[6];
        }
    }
}
