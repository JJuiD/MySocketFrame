using Scripts.Logic._2D_Base;
using Scripts.UI;
using Scripts.UI.GP;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Logic.GP
{
    using DictionaryXml = Dictionary<string, Dictionary<string, string>>;
    
    public class GPLogic : LogicBase
    {
        ClickKey key = new ClickKey();
        GameState state = GameState.None;
        //private Dictionary<Int16, GPPlayerView> playerList = new Dictionary<Int16, GPPlayerView>();
        public override void Start()
        {

            if(!GameController.GetInstance().GetLineNetState())
            {
                GPPlayerLogic selfplayer = new GPPlayerLogic();
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.name = DataCenter.GetInstance().LocalName;

                selfplayer.SetServerPlayerData(playerInfo);
                GameController.GetInstance().AddPlayer(selfplayer);
                GPConsoleServer.GetInstance().Init();
            }
        }

        public override void LogicFixedUpdate()
        {
            if (state != GameState.Start) return;
            _2DPhysicalEngineWorld.GetInstance().FixedUpdate();
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
                if (player == null || player.GetLocalSeat() != 0) continue;
                player.DealKey(key);
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
            state = GameState.Start;
        }

        public void CreateUnit(Vector3 pos,bool isAi)
        {
            GameObject createPlayer = GameObject.Instantiate(playerPrefab, pos, Quaternion.identity);

            playerPhysical = createPlayer.GetComponent<GPPhysicalGlobal>();
            playerView = createPlayer.GetComponent<GPPlayerView>();
            playerCollider = createPlayer.GetComponent<GPColliderGlobal>();
            playerPhysical.Init(playerView.transform.position);
            playerView.Init();
            playerCollider.Init(new Vector2(0, 0.23f), new Vector2(0.5f, 0.85f));
            _2DAIEngineWorld.GetInstance().AddTarget(createPlayer);
            if(isAi)
            {

            }
            else
            {
                UIManager.GetInstance().GetMainUI<UIGP>().UpdatePlayerHead(playerHero.headImage);
            }
        }

        public override void ExitGame()
        {
            Debug.Log("GPLogic ExitGame");
        }

        

        public GPLogic()
        {
        }

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

        public override void RecvGPBuffer(object _object, byte[] buffer)
        {
            
        }

        public override void RecvGSBuffer(object _object, byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }

    public class GPPlayerLogic : BasePlayer
    {
        GPPlayerView playerView;
        GPPhysicalGlobal playerPhysical;
        GPColliderGlobal playerCollider;

        bool isDestory = false;
        bool isJump = false;

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
                int curPosY = (int)(playerPhysical.position.z * 100);
                if (curPosY == 0 && isJump)
                    isJump = false;
                SetCost(CostType.mp, -playerHero.mprcvrate);
            }
        }

        #region 按键处理
        int sameKeyCount = 0;
        public void DealKey(ClickKey key)
        {
            if (key.GetKey() == 0)
            {
                playerPhysical.SetVelocity(Vector2.zero);
                playerView.ExcuteIdelAnimation();
                return;
            }

            if (key.GetTopKey() != key.GetLastKey())
            {
                sameKeyCount = 0;
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
            else
            {
                sameKeyCount += 1;
                Debug.Log(key.GetTopKey() + " : " + sameKeyCount);
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
                    playerPhysical.AddVelocity(new _Vector3(0, 0, 2.5f));
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
                bool isRun = false;

                if (moveDirection.y > 0)
                {

                }
                else if (moveDirection.x > 0)
                {
                    isRun = moveDirection.x > 1;
                    playerView.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (moveDirection.x < 0)
                {
                    isRun = moveDirection.x < -1;
                    playerView.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else if (moveDirection.y < 0)
                {

                }

                Vector2 targetPos = new Vector2(moveDirection.x + playerView.transform.position.x
                    , moveDirection.y + playerView.transform.position.y) * playerHero.speed;
                playerPhysical.SetVelocity(moveDirection * playerHero.speed * Time.fixedDeltaTime);
                playerView.ExcuteMoveAnimaion();
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
                            SetCost(skill.costType, skill.cost);
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
                    playerHero.costMp = playerHero.costMp < 0 ? 0 : playerHero.costMp;
                    UIManager.GetInstance().GetMainUI<UIGP>().UpdatePlayerState(
                        CostType.mp, (playerHero.mp - playerHero.costMp) / playerHero.mp);
                    break;
                case CostType.hp:
                    playerHero.costHp += value;
                    playerHero.costHp = playerHero.costHp < 0 ? 0 : playerHero.costHp;
                    UIManager.GetInstance().GetMainUI<UIGP>().UpdatePlayerState(
                        CostType.hp, (playerHero.hp - playerHero.costHp) / playerHero.hp);
                    break;
            }
        }
        public bool IsDied()
        {
            return !(playerHero.hp > playerHero.costHp);
        }
        public void ResetCost()
        {
            playerHero.costHp = 0;
            playerHero.costMp = 0;
        }
        #endregion
    }


}
