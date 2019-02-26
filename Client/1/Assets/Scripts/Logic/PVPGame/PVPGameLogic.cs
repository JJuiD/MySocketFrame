using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.PVPGame
{
    using DictionaryXml = Dictionary<string, Dictionary<string, string>>;
    public class PVPGameLogic : BaseLogic
    {
        private Dictionary<Int16, PVPGamePlayer> playerList = new Dictionary<Int16, PVPGamePlayer>();
        
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

            AnalysisHeroDefault();
            AnalysisWeaponDefault();
            AnalysisSkillDefault();
        }

        public override void AddPlayer(BasePlayerLogic player)
        {
            PVPGamePlayer itemPlayer = (PVPGamePlayer)player;
            if (playerList.Count == 0 )
            {
                playerList.Add(itemPlayer.GetServerSeat(), itemPlayer);
                return;
            }

            foreach(var temp in playerList)
            {
                if(temp.Value.GetServerSeat() == itemPlayer.GetServerSeat())
                {
                    Debug.LogError("[ERROR] 存在同样的用户");
                    return;
                }
            }
        }

        public PVPGamePlayer GetPlayerBySeat(Int16 seat)
        {
            if (playerList.Count == 0) { return null; }
            if (playerList.ContainsKey(seat)) return playerList[seat];
            Debug.LogError("[ERROR] GetPlayerBySeat : " + seat.ToString() + " 不存在");
            return null;
        }

        public PVPGamePlayer GetPlayerByLocalSeat(Int16 localseat)
        {
            if (playerList.Count == 0) { return null; }
            foreach (var temp in playerList)
            {
                if (temp.Value.GetLocalSeat() == localseat)
                {
                    return temp.Value;
                }
            }

            return null;
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

        public override void LoadKeyEventDic(UserDefault userDefault)
        {
            bool bLoad = false;
            foreach(var temp in userDefault._STRValues)
            {
                if (temp.name == PVPGameConfig.KEY_EVENT_START)
                {
                    bLoad = true;
                    continue;
                }
                else if (temp.name == PVPGameConfig.KEY_EVENT_END) return;

                if(bLoad && EventToActionDic.ContainsKey(temp.name))
                {
                    KeyCode key = (KeyCode)Enum.Parse(typeof(KeyCode), temp.value);
                    KeyToEventDic.Add(key, temp.name);
                }
            }
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
                PVPGamePlayer player = GetPlayerBySeat(0);
                player.DealKeyUnit(units);
            }
        }

        #region 英雄本地数据解析,获取
        DictionaryXml HeroLocalData;
        private void AnalysisHeroDefault()
        {
            HeroLocalData = new DictionaryXml();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_HERODEFAULT);
            HeroLocalData = SwitchToDictionaryXml(root);
        }
        public Dictionary<string,string> GetHeroInfo(int index)
        {
            if (!HeroLocalData.ContainsKey(index.ToString())) return null;
            return HeroLocalData[index.ToString()];
        }
        #endregion

        #region 武器解析,获取
        DictionaryXml WeaponLocalData;
        private void AnalysisWeaponDefault()
        {
            WeaponLocalData = new DictionaryXml();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_WEAPONDEFAULT);
            WeaponLocalData = SwitchToDictionaryXml(root);
        }
        public Dictionary<string, string> GetWeaponXmlInfo(int index)
        {
            if (!WeaponLocalData.ContainsKey(index.ToString())) return null;
            return WeaponLocalData[index.ToString()];
        }
        public WeanponUnit GetWeaponInfo(int id)
        {
            Dictionary<string, string> weaponXmlInfo = GetWeaponXmlInfo(id);
            if(weaponXmlInfo == null)
            {
                Debug.LogError("武器id : " + id.ToString() + " 不存在");
                return null;
            }
            WeanponUnit weanpon = new WeanponUnit();
            weanpon.id = id;
            float.TryParse(weaponXmlInfo["damage"], out weanpon.damage);
            for (int i = 0; ; ++i)
            {
                string dic_key = "skill_" + i.ToString();
                if (!weaponXmlInfo.ContainsKey(dic_key)) break;
                SkillUnit skillUnit = new SkillUnit();
                Dictionary<string, string> skillXmlInfo = GameController.GetInstance().GetLogic<PVPGameLogic>().GetSkillInfo(skillUnit.id);

                int.TryParse(weaponXmlInfo[dic_key], out skillUnit.id);
                int.TryParse(skillXmlInfo["cost"], out skillUnit.cost);
                float.TryParse(skillXmlInfo["damage"], out skillUnit.damage);
                skillUnit.skillName = skillXmlInfo["skillName"];
                skillUnit.keys = new List<string>(skillXmlInfo["key"].Split('|'));
                weanpon.skills.Add(skillUnit);
            }

            return weanpon;
        }
        #endregion

        #region 技能解析,获取
        DictionaryXml SkillLocalData;
        private void AnalysisSkillDefault()
        {
            SkillLocalData = new DictionaryXml();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_PVPGAME_SKILLDEFAULT);
            SkillLocalData = SwitchToDictionaryXml(root);
        }
        public Dictionary<string, string> GetSkillInfo(int index)
        {
            if (!SkillLocalData.ContainsKey(index.ToString())) return null;
            return SkillLocalData[index.ToString()];
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

        //private Dictionary<string, string> GetXmlInfoById(DictionaryXml dic,int id)
        //{
        //    foreach(var temp in dic)
        //    {

        //    }
        //}
    }
}
