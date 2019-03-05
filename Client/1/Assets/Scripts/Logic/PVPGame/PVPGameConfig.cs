using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic.PVPGame
{
    public static class PVPGameConfig
    {
        //事件名
        public const string KEY_EVENT_START = "KEY_EVENT_START";
        public const string KEY_EVENT_UP = "KEY_EVENT_UPKEY_UP";
        public const string KEY_EVENT_LEFT = "KEY_EVENT_LEFT";
        public const string KEY_EVENT_DOWN = "KEY_EVENT_DOWN";
        public const string KEY_EVENT_RIGHT = "KEY_EVENT_RIGHT";

        public const string KEY_EVENT_ATTACK = "KEY_EVENT_ATTACK";
        public const string KEY_EVENT_JUMP = "KEY_EVENT_JUMP";
        public const string KEY_EVENT_DEFENCE = "KEY_EVENT_DEFENCE";
        public const string KEY_EVENT_END = "KEY_EVENT_END";

        public const int HERO_EMOTICONS_MAX_COUNT = 6;
        public const float SKILL_OUTTIME = 2.5f;
    }

    public enum GameStep
    {
        GAME_STEP_NULL,
        GAME_STEP_START,
        GAME_STEP_END,
    }

    public enum AniType
    {
        IDEL,
        WALK,
        RUN,
        ATTACK,
        JUMP,
    }

    public enum PlayerGameState
    {
        idel,
        attack,
        jump,
        run,
        walk,
        defence,
        bHint,
    }

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

    public class KeyUnit
    {
        public string eventName = "";
        public bool isDown = false;
        public float time = 0;
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
        public float speed = 0;
        public int id = 0;
        public Dictionary<EmoticonType, Sprite> emoticons;
        public Sprite headImage;

        public void ReadStream(Dictionary<string,string> data,int id)
        {
            this.id = id;
            heroName = data["heroName"];
            float.TryParse(data["hp"], out this.hp);
            float.TryParse(data["mp"], out this.mp);
            float.TryParse(data["speed"], out this.mp);
            Sprite[] sprites = Resources.LoadAll<Sprite>(data["heroSpritePath"]);
            emoticons = new Dictionary<EmoticonType, Sprite>();
            for (int i = 0;i < PVPGameConfig.HERO_EMOTICONS_MAX_COUNT;++i)
            {
                emoticons.Add((EmoticonType)i, sprites[i]);
            }
            headImage = sprites[6];
        }
    }
}
