using Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.PVPGame
{
    using DictionaryXml = Dictionary<string, Dictionary<string, string>>;
    public class PVPGameLogic : BaseLogic
    {
        //private Dictionary<Int16, PVPGamePlayer> playerList = new Dictionary<Int16, PVPGamePlayer>();
        private int maxPlayer = 4;

        public override void Init()
        {
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_UP, KeyCode.None);
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_LEFT, KeyCode.None);
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_DOWN, KeyCode.None);
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_RIGHT, KeyCode.None);

            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_ATTACK, KeyCode.None);
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_JUMP, KeyCode.None);
            EventToActionDic.Add(PVPGameConfig.KEY_EVENT_DEFENCE, KeyCode.None);


            AnalysisSkillDefault();
            AnalysisWeaponDefault();
            AnalysisHeroDefault();
        }

        public override void LoadKeyEventDic(UserDefault userDefault)
        {
            bool bLoad = false;
            foreach (var temp in userDefault._STRValues)
            {
                if (temp.name == PVPGameConfig.KEY_EVENT_START)
                {
                    bLoad = true;
                    continue;
                }
                else if (temp.name == PVPGameConfig.KEY_EVENT_END) return;

                if (bLoad && EventToActionDic.ContainsKey(temp.name))
                {
                    KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), temp.value);
                    KeyToEventDic.Add(key, temp.name);
                }
            }
        }

        public int GetMaxPlayer()
        {
            return maxPlayer;
        }

        

        public override void JoinGame()
        {
            UIManager.GetInstance().GetCurrentScene<PVPGameScene>().OnEnterGame();
        }

        public override void AddDataListener()
        {
            DataCenter.GetInstance().AddDataListener(DataEventType.EVENT_KEY_DOWN, GetKeyDown);
            DataCenter.GetInstance().AddDataListener(DataEventType.EVENT_KEY_UP, GetKeyUp);
        }

        public override void RemoveDataListener()
        {
            DataCenter.GetInstance().AddDataListener(DataEventType.EVENT_KEY_DOWN, GetKeyDown);
            DataCenter.GetInstance().AddDataListener(DataEventType.EVENT_KEY_UP, GetKeyUp);
        }

        public void GetKeyDown(params object[] args)
        {
            //KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), args[0].ToString());
        }

        public void GetKeyUp(params object[] args)
        {
            //KeyCode keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), args[0].ToString());
        }

        

        public override void LogicFixedUpdate()
        {
            List<KeyUnit> units = new List<KeyUnit>();
            foreach (var temp in KeyToEventDic)
            {
                KeyUnit item = new KeyUnit();
                if (Input.GetKeyDown(temp.Key))
                {
                    item.eventName = temp.Value;
                    item.isDown = true;
                }
                else if (Input.GetKeyUp(temp.Key))
                {
                    item.eventName = temp.Value;
                    item.isDown = false;
                }
                if (item.eventName == "") continue;
                units.Add(item);
            }

            if(units.Count > 0)
            {
                //PVPGamePlayer player = GetPlayerBySeat(0);
                //player.DealKeyUnit(units);
            }
        }

        #region 英雄本地数据解析,获取
        Dictionary<int, HeroUnit> HeroLocalData;
        private void AnalysisHeroDefault()
        {
            HeroLocalData = new Dictionary<int, HeroUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_HERODEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach(var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                HeroUnit unit = new HeroUnit();
                unit.ReadStream(temp.Value,id);
                HeroLocalData.Add(id, unit);
            }
        }
        public HeroUnit GetHeroInfo(int index)
        {
            if (!HeroLocalData.ContainsKey(index)) return null;
            return HeroLocalData[index];
        }
        #endregion

        #region 武器解析,获取
        Dictionary<int, WeanponUnit> WeaponLocalData;
        private void AnalysisWeaponDefault()
        {
            WeaponLocalData = new Dictionary<int, WeanponUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_WEAPONDEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach (var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                WeanponUnit unit = new WeanponUnit();
                unit.ReadStream(temp.Value, id);
                for (int i = 0; ; ++i)
                {
                    string dic_key = "skill_" + i.ToString();
                    if (!temp.Value.ContainsKey(dic_key)) break;
                    SkillUnit skillUnit = GetSkillInfo(i);
                    unit.skills.Add(i,skillUnit);
                }
                WeaponLocalData.Add(id, unit);
            }
        }
        public WeanponUnit GetWeaponInfo(int index)
        {
            if (!WeaponLocalData.ContainsKey(index)) return null;
            return WeaponLocalData[index];
        }
        #endregion

        #region 技能解析,获取
        Dictionary<int, SkillUnit> SkillLocalData;
        private void AnalysisSkillDefault()
        {
            SkillLocalData = new Dictionary<int, SkillUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_SKILLDEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach(var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                SkillUnit unit = new SkillUnit();
                unit.ReadStream(temp.Value, id);
                SkillLocalData.Add(id, unit);
            }
        }
        public SkillUnit GetSkillInfo(int index)
        {
            if (!SkillLocalData.ContainsKey(index)) return null;
            return SkillLocalData[index];
        }
        #endregion

        private DictionaryXml SwitchToDictionaryXml(_DictionaryRoot root)
        {
            DictionaryXml _DictionaryXml = new DictionaryXml();
            foreach (var temp in root.ViewList)
            {
                _DictionaryXml.Add(temp.name, new Dictionary<string, string>());
                foreach (var info in temp._InfoList)
                {
                    _DictionaryXml[temp.name].Add(info.name, info.value);
                }
            }

            return _DictionaryXml;
        }

        public override void InitMapData(int mapindex)
        {
            UIManager.GetInstance().GetCurrentScene<PVPGameScene>().InitMapData(mapindex);
        }

    }
}
