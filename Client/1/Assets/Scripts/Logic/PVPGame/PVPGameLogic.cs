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
        

        public override void InitData()
        {
            ResetData();
            InitKeyEventDic();

            AnalysisSkillDefault();
            AnalysisWeaponDefault();
            AnalysisHeroDefault();

            InitPlayerPrefab();

            if(!GameController.GetInstance().GetLineNetState())
            {
                PVPGamePlayerLogic selfplayer = new PVPGamePlayerLogic();
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.name = DataCenter.GetInstance().LocalName;

                selfplayer.SetServerPlayerData(playerInfo);
                GameController.GetInstance().AddPlayer(selfplayer);
            }
        }

        public override void ResetData()
        {
            step = GameStep.GAME_STEP_NULL;
        }

        private GameStep step = GameStep.GAME_STEP_NULL;
        public override void LogicFixedUpdate()
        {
            for(int i = 0; i < GameController.GetInstance().GetPlayerCount(); ++i)
            {
                PVPGamePlayerLogic player = GameController.GetInstance().GetPlayerBySeat<PVPGamePlayerLogic>(0);
                if (player == null || player.GetView() == null || player.GetLocalSeat() != 0) continue;
                player.GetView().TickUpdate();
            }
        }

        #region 玩家预制件
        private GameObject playerPrefab;
        public GameObject GetPlayerPrefab() { return playerPrefab; }
        private void InitPlayerPrefab()
        {
            playerPrefab = Resources.Load<GameObject>(Config.PVPGame_PlayerObject);
            
        }
        #endregion

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

        public override void StartGame()
        {
            Debug.Log("PVPGameLogic StartGame");
            for(short i = 0;i<GameController.GetInstance().GetPlayerCount(); ++i)
            {
                GameController.GetInstance().GetPlayerByLocalSeat<PVPGamePlayerLogic>(i).Create(Vector3.zero);
            }
        }

        public override void ExitGame()
        {
            Debug.Log("PVPGameLogic ExitGame");
            ResetData();
        }

        public override void InitKeyEventDic()
        {
            UserDefault userDefault = DataCenter.GetInstance().GetUserDefault(Config.PVPGame).GetData();
            if (userDefault == null) return;
            DataCenter.GetInstance().ClearKeyList();
            foreach (var temp in userDefault._STRValues)
            {
                if (temp.name.Contains("KEY_EVENT_"))
                {
                    Debug.Log(temp.name + " bind " + temp.value);
                    KeyUnit keyUnit = new KeyUnit();
                    keyUnit.InitData(temp.name, temp.value);
                    DataCenter.GetInstance().AddKeyListener(keyUnit);
                }
            }
        }
    }

    public class PVPGamePlayerLogic : BasePlayer
    {
        PVPGamePlayer playerView;

        public PVPGamePlayer GetView()
        {
            return playerView;
        }

        public void Create(Vector3 pos)
        {
            GameObject defaultObject = GameController.GetInstance().GetLogic<PVPGameLogic>().GetPlayerPrefab();
            GameObject createPlayer = GameObject.Instantiate(defaultObject, pos, Quaternion.identity);

            playerView = createPlayer.GetComponent<PVPGamePlayer>();
            if(GetLocalSeat() == 0)
            {
                DataCenter.GetInstance().SetKeyEventAction(ReqDealKeyUnit);
            }
            playerView.Create();
        }

        public void ReqDealKeyUnit(List<KeyUnit> units)
        {
            playerView.DealKeyUnit(units);
        }
    }

    
}
