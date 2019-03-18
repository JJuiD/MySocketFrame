using Scripts.TwoDimensiona;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.GP
{
    using DictionaryXml = Dictionary<string, Dictionary<string, string>>;
    
    public class GPLogic : BaseLogic
    {
        ClickKey key = new ClickKey();
        //private Dictionary<Int16, GPPlayer> playerList = new Dictionary<Int16, GPPlayer>();
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
                GPPlayerLogic selfplayer = new GPPlayerLogic();
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.name = DataCenter.GetInstance().LocalName;

                selfplayer.SetServerPlayerData(playerInfo);
                GameController.GetInstance().AddPlayer(selfplayer);
            }
        }

        public override void ResetData()
        {
            key.ResetData();
        }

        public override void LogicFixedUpdate()
        {
            PhysicalEngineWord.GetInstance().FixedUpdate();
            key.StartSetKey();
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_UP]), KeyByte.KEY_UP, true);
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_RIGHT]), KeyByte.KEY_RIGHT, true);
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_LEFT]), KeyByte.KEY_LEFT, true);
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_DOWN]), KeyByte.KEY_DOWN, true);

            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_ATTACK]), KeyByte.KEY_ATTACK);
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_JUMP]), KeyByte.KEY_JUMP);
            key.SetKey(Input.GetKey(KeyPairs[GPConfig.KEY_DEFENCE]), KeyByte.KEY_DEFENCE);
            key.EndSetKey();

            for (int i = 0; i < GameController.GetInstance().GetPlayerCount(); ++i)
            {
                GPPlayerLogic player = GameController.GetInstance().GetPlayerBySeat<GPPlayerLogic>(0);
                if (player == null || player.GetView() == null || player.GetLocalSeat() != 0) continue;
                player.ReqDealKey(key);
                player.TickUpdate();
                
            }
        }

        #region 玩家预制件
        private GameObject playerPrefab;
        public GameObject GetPlayerPrefab() { return playerPrefab; }
        private void InitPlayerPrefab()
        {
            playerPrefab = Resources.Load<GameObject>(Config.GP_PlayerObject);
            
        }
        #endregion

        #region 英雄本地数据解析,获取
        Dictionary<int, HeroUnit> HeroLocalData;
        private void AnalysisHeroDefault()
        {
            HeroLocalData = new Dictionary<int, HeroUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_GP_HERODEFAULT);
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
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_GP_WEAPONDEFAULT);
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
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(Config.XML_GP_SKILLDEFAULT);
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
            Debug.Log("GPLogic StartGame");
            for(short i = 0;i<GameController.GetInstance().GetPlayerCount(); ++i)
            {
                GameController.GetInstance().GetPlayerByLocalSeat<GPPlayerLogic>(i).Create(Vector3.zero);
            }
        }

        public override void ExitGame()
        {
            Debug.Log("GPLogic ExitGame");
            ResetData();
        }

        Dictionary<string, KeyCode> KeyPairs = new Dictionary<string, KeyCode>();
        public override void InitKeyEventDic()
        {
            UserDefault userDefault = DataCenter.GetInstance().GetUserDefault(Config.GP).GetData();
            if (userDefault == null) return;
            foreach (var temp in userDefault._STRValues)
            {
                if (temp.name.Contains("KEY_"))
                {
                    Debug.Log(temp.name + " bind " + temp.value);
                    KeyPairs.Add(temp.name, (KeyCode)Enum.Parse(typeof(KeyCode), temp.value));
                }
            }
        }
    }

    public class GPPlayerLogic : BasePlayer
    {
        GPPlayer playerView;
        
        bool isDestory = true;
        bool isJump = false;
        public GPPlayer GetView()
        {
            return playerView;
        }

        public void Create(Vector3 pos)
        {
            GameObject defaultObject = GameController.GetInstance().GetLogic<GPLogic>().GetPlayerPrefab();
            GameObject createPlayer = GameObject.Instantiate(defaultObject, pos, Quaternion.identity);

            playerView = createPlayer.GetComponent<GPPlayer>();
            playerView.Create();
            isDestory = false;
        }

        public void TickUpdate()
        {
            if (isDestory)
            {

            }
            else
            {
                if (beginReadySkillTime != 0
                    && Time.time - beginReadySkillTime > GPConfig.SKILL_OUTTIME)
                {
                    ClearSkillKeyDic();
                }
                int curPosY = (int)(playerView.GetPhySical().position.z * 100);
                if (curPosY == 0 && isJump)
                    isJump = false;
            }
        }

        #region 按键处理
        public void ReqDealKey(ClickKey key)
        {
            if (key.GetKey() == 0)
            {
                playerView.ExcuteIdelAnimation();
                return;
            }
                
            DealKey(key);
        }

        public void DealKey(ClickKey key)
        {
            if (key.GetTopKey() != key.GetLastKey())
            {
                bool isFowardToRight = playerView.transform.eulerAngles.x != 180;
                byte animKey = key.GetTopKey();
                if (!isFowardToRight && (animKey == (byte)KeyByte.KEY_LEFT
                    || animKey == (byte)KeyByte.KEY_RIGHT))
                {
                    animKey = (byte)(((byte)KeyByte.KEY_RIGHT + (byte)KeyByte.KEY_LEFT) & ~animKey);
                }
                Debug.Log(animKey);
                if (DealSkillCombo(animKey)) return;
            }

            #region 移动
            Vector2 moveDirection = Vector2.zero;
            if ((key.GetKey() & (byte)KeyByte.KEY_UP) == (byte)KeyByte.KEY_UP)
            {
                moveDirection.y += 0.7f;
            }
            if ((key.GetKey() & (byte)KeyByte.KEY_LEFT) == (byte)KeyByte.KEY_LEFT)
            {
                moveDirection.x += -1;
                moveDirection.x += key.IsClickDouble() ? -1 : 0;
            }
            if ((key.GetKey() & (byte)KeyByte.KEY_DOWN) == (byte)KeyByte.KEY_DOWN)
            {
                moveDirection.y += -0.7f;
            }
            if ((key.GetKey() & (byte)KeyByte.KEY_RIGHT) == (byte)KeyByte.KEY_RIGHT)
            {
                moveDirection.x += 1;
                moveDirection.x += key.IsClickDouble() ? 1 : 0;
            }
            #endregion

            if ((key.GetKey() & (byte)KeyByte.KEY_ATTACK) == (byte)KeyByte.KEY_ATTACK)
            {
                playerView.ExcuteAttackAnimation();
                return;
            }
            else if ((key.GetKey() & (byte)KeyByte.KEY_JUMP) == (byte)KeyByte.KEY_JUMP)
            {
                if (isJump == false)
                {
                    playerView.ExcuteJumpAnimation();
                    isJump = true;
                }
                
                return;
            }
            else if ((key.GetKey() & (byte)KeyByte.KEY_DEFENCE) == (byte)KeyByte.KEY_DEFENCE)
            {
                playerView.ExcuteDefenceAnimation();
                return;
            }

            if (moveDirection != Vector2.zero)
            {
                playerView.ExcuteWalkAnimaion(moveDirection, playerHero.speed);
                return;
            }
        }
        #endregion

        #region 技能逻辑判断
        //防御键 + 方向 + 攻击 + 攻击(...)
        //连招id,符合个数
        Dictionary<int, int> skillClickKeyCount = new Dictionary<int, int>();
        private float beginReadySkillTime = 0;
        public bool DealSkillCombo(byte keyByte)
        {
            if (skillClickKeyCount.Count == 0)
            {
                if (keyByte != (byte)KeyByte.KEY_DEFENCE) return false;
                foreach (var temp in playerWeapon.skills)
                {
                    skillClickKeyCount.Add(temp.Key, 1);
                }
                beginReadySkillTime = Time.time;
            }
            else
            {
                if (Time.time - beginReadySkillTime > GPConfig.SKILL_OUTTIME)
                {
                    ClearSkillKeyDic();
                    return false;
                }

                List<int> addList = new List<int>();
                List<int> delList = new List<int>();
                foreach (var temp in skillClickKeyCount)
                {
                    SkillUnit skill = playerWeapon.skills[temp.Key];
                    //if (temp.Value >= 1 && keyEventType == skill.keys[temp.Value - 1]) continue;
                    byte _keyByte = (byte)Enum.Parse(typeof(KeyByte), skill.keys[temp.Value]);
                    if (keyByte == _keyByte)
                    {
                        Debug.Log(temp.Value + " / " + skill.keys.Count);
                        if ((temp.Value + 1) == skill.keys.Count
                            && IsAllowCost(skill.costType, skill.cost))
                        {
                            playerView.ExcuteSkillAnimation(skill.id);
                            ClearSkillKeyDic();
                            return true;
                        }
                        else addList.Add(temp.Key);
                        Debug.Log(temp.Key + " add " + keyByte);
                    }
                    else
                    {
                        delList.Add(temp.Key);
                    }
                }
                for (int i = 0; ; ++i)
                {
                    if (i >= delList.Count && i >= addList.Count) break;
                    if (addList.Count > i) skillClickKeyCount[addList[i]] += 1;
                    if (delList.Count > i) skillClickKeyCount.Remove(delList[i]);
                }
            }
            return false;
        }
        public void ClearSkillKeyDic()
        {
            skillClickKeyCount.Clear();
            beginReadySkillTime = 0;
        }
        #endregion

        #region 玩家游戏数据
        private WeanponUnit playerWeapon = new WeanponUnit();
        private HeroUnit playerHero = new HeroUnit();
        public void SetLocalPlayerData(int heroId, int weaponId)
        {
            playerWeapon = GameController.GetInstance().GetLogic<GPLogic>().GetWeaponInfo(weaponId);
            playerHero = GameController.GetInstance().GetLogic<GPLogic>().GetHeroInfo(heroId);
        }
        public bool IsAllowCost(CostType type, float value)
        {
            switch (type)
            {
                case CostType.mp:
                    if (playerHero.mp >= value + playerHero.costMp) return true;
                    break;
                case CostType.hp:
                    if (playerHero.hp > value + playerHero.costHp) return true;
                    break;
            }

            return false;
        }
        public void SetCost(CostType type, float value)
        {
            if (!IsAllowCost(type, value)) return;
            switch (type)
            {
                case CostType.mp:
                    playerHero.costMp += value;
                    break;
                case CostType.hp:
                    playerHero.costHp += value;
                    break;
            }
        }
        public bool IsDied()
        {
            return (playerHero.hp > playerHero.costHp);
        }
        public void ResetCost()
        {
            playerHero.costHp = 0;
            playerHero.costMp = 0;
        }
        #endregion
    }


}
