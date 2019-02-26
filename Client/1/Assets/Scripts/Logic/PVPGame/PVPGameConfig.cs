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

    }

    public enum AniType
    {
        IDEL,
        WALK,
        RUN,
        ATTACK,
        JUMP,
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
        public int id = 0;
        public int cost = 0;
        public List<string> keys = new List<string>();
    }

    public class WeanponUnit
    {
        public string weaponName = "";
        public float damage = 3;
        public int id = 0;
        public List<SkillUnit> skills = new List<SkillUnit>();
    }
}
