using Proto.Cell;
using Proto;
using Proto.GPGameProto;
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
        //<按键事件,按键>
        Dictionary<string, KeyCode> KeyPairs;
        //<英雄索引,英雄数据>
        Dictionary<int, HeroInfo> HeroLocalData;
        public uint heroID { get; set; }
        //<武器索引,武器数据>
        //Dictionary<int, WeanponUnit> WeaponLocalData;
        //<技能索引,技能数据>
        Dictionary<int, SkillInfo> SkillLocalData;
        /// <summary>
        /// <关卡索引,关卡数据>
        /// </summary>
        Dictionary<int, LevelInfo> LevelLocalData;
        /// <summary>
        /// 单位列表
        /// </summary>
        List<UnitLogic> UnitList;
        /// <summary>
        /// 键盘按键处理
        /// </summary>
        ClickKey key = new ClickKey();
        //客户端游戏状态
        GameStep gameStep;
        
        public override void InitData()
        {
            DataCenter.GetInstance().InsertUserDefault(Config.GP, GPConfig.XML_GP_USERDEFAULT);
            InitLocalData();
            UIManager.GetInstance().LoadScene(Config.GP);
            ReqGameStart();
        }

        #region LocalData (英雄,技能)
        public HeroInfo GetHeroInfo(int index)
        {
            if (!HeroLocalData.ContainsKey(index)) return null;
            return HeroLocalData[index];
        }
        public SkillInfo GetSkillInfo(int index)
        {
            if (!SkillLocalData.ContainsKey(index)) return null;
            return SkillLocalData[index];
        }
        public void InitLocalData()
        {
            KeyPairs = new Dictionary<string, KeyCode>();
            UnitList = new List<UnitLogic>();
            UserDefault userDefault = DataCenter.GetInstance().GetUserDefault(Config.GP).GetData();
            if (userDefault == null) return;
            foreach (var temp in userDefault._STRValues)
            {
                if (temp.name.Contains("KEY_"))
                {
                    Debug.Log(temp.name + " bind " + temp.value);
                    KeyPairs.Add(temp.name, (KeyCode)Enum.Parse(typeof(KeyCode), temp.value));
                }
                else if(temp.name.Contains("LEVEL_"))
                {
                    //string[] strArray = temp.name.Split('_');
                    //int levelid = int.Parse(strArray[1]);
                    //int childlevelid = int.Parse(strArray[2]);
                    //LevelInfo info = new LevelInfo();
                    //if (LevelLocalData.ContainsKey(levelid))
                    //{
                    //    info = LevelLocalData[levelid];
                    //}
                    //if(info.childlevelInfo.ContainsKey)
                }
            }

            //技能解析
            SkillLocalData = new Dictionary<int, SkillInfo>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(GPConfig.XML_GP_SKILLDEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach (var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                SkillInfo unit = new SkillInfo();
                unit.ReadStream(temp.Value, id);
                SkillLocalData.Add(id, unit);
            }

            //英雄解析
            HeroLocalData = new Dictionary<int, HeroInfo>();
            root = FileUtils.LoadFromXml<_DictionaryRoot>(GPConfig.XML_GP_HERODEFAULT);
            data = SwitchToDictionaryXml(root);
            foreach (var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                HeroInfo unit = new HeroInfo();
                unit.ReadStream(temp.Value, id);
                for (int i = 0; ; ++i)
                {
                    string dic_key = "skill_" + i.ToString();
                    if (!temp.Value.ContainsKey(dic_key)) break;
                    SkillInfo SkillInfo = GetSkillInfo(i);
                    unit.skills.Add(i, SkillInfo);
                }
                HeroLocalData.Add(id, unit);
            }
        }
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
        #endregion

        #region Protocol 协议部分
        public void ReqGameStart()
        {
            Debug.Log("ReqGameStart");
            CellReqGameStart cellReqGame = new CellReqGameStart();
            gameStep = GameStep.NONE;
            cellReqGame.SaveData(0);//roomid : 0
            SendGamePacket(cellReqGame);
        }
        public override void SendGamePacket(Cell_Base data)
        {
            if(GameController.GetInstance().GetLineNetState())
            {
                Debug.Log("发送到单机服务器");
                GPConsoleServer.GetInstance().OnPacketDealCenter(data);
            }
            else
            {

            }
        }
        public override bool OnRecivePacket(object _object, byte[] buffer)
        {
            switch ((GPGameCMD)_object)
            {
                case GPGameCMD.CMD_GAME_RESPSTARTGAME:
                    
                    break;
                case GPGameCMD.CMD_GAME_PLAYERINFO:
                    CellPlayerInfo cellPlayerInfo = new CellPlayerInfo();
                    GPCMD_PlayerInfo dataPlayerinfo = cellPlayerInfo.ParseData<GPCMD_PlayerInfo>(buffer);
                    OnMsgPlayerInfo(dataPlayerinfo);
                    break;
                case GPGameCMD.CMD_GAME_UPDATESTEP:
                    CellUpdateGameStep cellUpdateGameStep = new CellUpdateGameStep();
                    GPCMD_UpdateStep dataStep = cellUpdateGameStep.ParseData<GPCMD_UpdateStep>(buffer);
                    OnMsgUpdateStep(dataStep.step);
                    break;
            }

            return false;
        }
        public override bool OnReciveConsole(object _object, byte[] buffer)
        {
            switch ((GPGameCMD)_object)
            {
                case GPGameCMD.CMD_GAME_RESPSTARTGAME:
                    OnMsgRespStartGame();
                    break;
            }

            return false;
        }
        public override void AddPortocolListen(CallBack callback, ProtoCommand cmd)
        {
            if(GameController.GetInstance().GetLineNetState())
            {
                GPConsoleServer.GetInstance().addPortocolListen(callback);
            }
            else
            {

            }
        }
        private void OnMsgRespStartGame()
        {
            //第一次准备
            if(gameStep == GameStep.NONE)
            {
                gameStep = GameStep.READY;
                UIManager.GetInstance().OpenNode<UIGPInit>(UIConfig.UIGPInit);
                return;
            }
        }
        private void OnMsgPlayerInfo(GPCMD_PlayerInfo data)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.name = data.name;
            playerInfo.seat = data.seat;
            GameController.GetInstance().AddPlayer(playerInfo);
        }
        private void OnMsgUpdateStep(uint stepid)
        {
            GameStep step = (GameStep)Enum.ToObject(typeof(GameStep), stepid);
            switch(step)
            {
                case GameStep.START:
                    ResertGameScene();
                    break;
            }

            gameStep = step;
        }
        #endregion

        #region 关卡信息
        
        #endregion

        private void ResertGameScene()
        {
            //重置关卡数据
            //重置玩家信息
            if(UnitList.Count == 0)
            {
                for(uint i = 0;i < GameController.GetInstance().GetPlayerCount();++i)
                {
                    //导入玩家单位
                    UnitLogic unit = new UnitLogic();
                    unit.localTag = (int)i;
                    uint _heroID = heroID;
                    if (i != 0) { /*_heroID = ;*/ }
                    unit.InitData((int)_heroID, false);
                    unit.Create(Vector3.zero);
                    UnitList.Add(unit);
                }

                //临时创建
                UnitLogic aiUnit = new UnitLogic();
                aiUnit.InitData(0);
                aiUnit.Create(new Vector3(7, 0));
                UnitList.Add(aiUnit);
            }
            else
            {
                foreach (var unit in UnitList)
                    unit.Resert();
            }
        }
        public override void LogicFixedUpdate()
        {
            if (gameStep != GameStep.START || UnitList.Count == 0) return;
            _2DPhysicalEngineWorld.GetInstance().FixedUpdate();
            key.StartSetKey();
            key.SetKey(KeyPairs[GPConfig.KEY_UP], KeyByte.KEY_UP);
            key.SetKey(KeyPairs[GPConfig.KEY_DOWN], KeyByte.KEY_DOWN);
            key.SetKey(KeyPairs[GPConfig.KEY_RIGHT], KeyByte.KEY_RIGHT, KeyByte.KEY_NULL);
            key.SetKey(KeyPairs[GPConfig.KEY_LEFT], KeyByte.KEY_LEFT, KeyByte.KEY_RIGHT);

            key.SetKey(KeyPairs[GPConfig.KEY_ATTACK], KeyByte.KEY_ATTACK);
            key.SetKey(KeyPairs[GPConfig.KEY_JUMP], KeyByte.KEY_JUMP);
            key.SetKey(KeyPairs[GPConfig.KEY_DEFENCE], KeyByte.KEY_DEFENCE);
            key.EndSetKey();

            foreach (var temp in UnitList)
            {
                if (temp.localTag == 0)
                    temp.DealKey(key);
                temp.TickUpdate();
            }
        }
    }

    public class UnitLogic
    {
        public int localTag = -1;
        bool isAi = true;
        bool isDestory = false;
        bool isJump = false;
        //int sameKeyCount = 0;

        GPPlayerView playerView;

        //private WeanponUnit playerWeapon = new WeanponUnit();
        HeroInfo unitInfo = new HeroInfo();

        Dictionary<int, int> skillClickKeyCount = new Dictionary<int, int>();
        float beginReadySkillTime = 0;

        public void InitData(int heroid,bool isAi = true)
        {
            this.isAi = isAi;
            unitInfo = (GameController.GetInstance().Logic as GPLogic).GetHeroInfo(heroid);
        }

        public void Create(Vector3 pos)
        {
            GameObject gameObject = GameObject.Instantiate(unitInfo.prefab, pos, Quaternion.identity);
            playerView = gameObject.GetComponent<GPPlayerView>();
            if (this.isAi)
            {
                gameObject.AddComponent<GPAIGlobal>();
            }
            else
            {

            }
        }

        public void Resert()
        {
            unitInfo.Resert();
            isDestory = false;
            isJump = false;
            ClearSkillKeyDic();
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
                int curPosZ = (int)(playerView.gameObject.GetComponent<GPPhysicalGlobal>().position.z * 100);
                if (curPosZ == 0 && isJump)
                    isJump = false;
                SetCost(CostType.mp, -unitInfo.mprcvrate);
            }
        }

        public void DealKey(ClickKey key)
        {
            if (key.GetKey() == key.GetLastKey()) return;
            //没有键按下
            if (key.GetKey() == 0)
            {
                if(!isJump)
                    playerView.gameObject.GetComponent<GPPhysicalGlobal>().SetVelocity(Vector2.zero);
                playerView.ExcuteIdelAnimation();
                return;
            }

            if (key.GetKey() != key.GetLastKey())
            {
                bool isFowardToRight = playerView.transform.eulerAngles.y != 180;
                byte animKey = key.GetKey();
                if (!isFowardToRight && (animKey == (byte)KeyByte.KEY_LEFT
                    || animKey == (byte)KeyByte.KEY_RIGHT))
                {
                    animKey = (byte)(((byte)KeyByte.KEY_RIGHT + (byte)KeyByte.KEY_LEFT) & ~animKey);
                }
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
                    playerView.gameObject.GetComponent<GPPhysicalGlobal>().AddVelocity(new _Vector3(0, 0, 2.5f));
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

            if ( !isJump)
            {
                bool isRun = false;

                if (moveDirection.x > 0)
                {
                    isRun = moveDirection.x > 1;
                    playerView.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (moveDirection.x < 0)
                {
                    isRun = moveDirection.x < -1;
                    playerView.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                Vector2 targetPos = new Vector2(moveDirection.x + playerView.transform.position.x
                    , moveDirection.y + playerView.transform.position.y) * unitInfo.speed;
                playerView.gameObject.GetComponent<GPPhysicalGlobal>().SetVelocity(moveDirection * unitInfo.speed * Time.fixedDeltaTime);
                playerView.ExcuteMoveAnimaion();
                return;
            }
        }

        #region 技能逻辑判断
        public bool DealSkillCombo(byte keyByte)
        {
            if (skillClickKeyCount.Count == 0)
            {
                if (keyByte != (byte)KeyByte.KEY_DEFENCE) return false;
                foreach (var temp in unitInfo.skills)
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
                    SkillInfo skill = unitInfo.skills[temp.Key];
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
                if (skillClickKeyCount.Count > 0) return true;
            }
            return false;
        }
        public void ClearSkillKeyDic()
        {
            skillClickKeyCount.Clear();
            beginReadySkillTime = 0;
        }
        #endregion

        #region 玩家数据
        public bool IsAllowCost(CostType type, float value)
        {
            switch (type)
            {
                case CostType.mp:
                    if (unitInfo.mp >= value + unitInfo.costMp) return true;
                    break;
                case CostType.hp:
                    if (unitInfo.hp > value + unitInfo.costHp) return true;
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
                    unitInfo.costMp += value;
                    unitInfo.costMp = unitInfo.costMp < 0 ? 0 : unitInfo.costMp;
                    UIManager.GetInstance().GetMainUI<UIGP>().UpdatePlayerState(
                        CostType.mp, (unitInfo.mp - unitInfo.costMp) / unitInfo.mp);
                    break;
                case CostType.hp:
                    unitInfo.costHp += value;
                    unitInfo.costHp = unitInfo.costHp < 0 ? 0 : unitInfo.costHp;
                    UIManager.GetInstance().GetMainUI<UIGP>().UpdatePlayerState(
                        CostType.hp, (unitInfo.hp - unitInfo.costHp) / unitInfo.hp);
                    break;
            }
        }
        public bool IsDied()
        {
            return !(unitInfo.hp > unitInfo.costHp);
        }
        public void ResetCost()
        {
            unitInfo.costHp = 0;
            unitInfo.costMp = 0;
        }
        #endregion
    }
}