using Proto.Cell;
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
        Dictionary<int, HeroUnit> HeroLocalData;
        //<武器索引,武器数据>
        //Dictionary<int, WeanponUnit> WeaponLocalData;
        //<技能索引,技能数据>
        Dictionary<int, SkillUnit> SkillLocalData;
        //客户端游戏状态
        GameState gameState;
        public override void InitData()
        {
            DataCenter.GetInstance().InsertUserDefault(Config.GP, GPConfig.XML_GP_USERDEFAULT);
            InitLocalData();
            ReqGameStart();
        }

        #region LocalData (英雄,技能)
        public HeroUnit GetHeroInfo(int index)
        {
            if (!HeroLocalData.ContainsKey(index)) return null;
            return HeroLocalData[index];
        }
        public GameObject GetHeroPrefab(int index)
        {
            HeroUnit temp = GetHeroInfo(index);
            if (temp != null)
            {
                return temp.prefab;
            }
        }
        public SkillUnit GetSkillInfo(int index)
        {
            if (!SkillLocalData.ContainsKey(index)) return null;
            return SkillLocalData[index];
        }
        public void InitLocalData()
        {
            KeyPairs = new Dictionary<string, KeyCode>();
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

            //英雄解析
            HeroLocalData = new Dictionary<int, HeroUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(GPConfig.XML_GP_HERODEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach (var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                HeroUnit unit = new HeroUnit();
                unit.ReadStream(temp.Value, id);
                HeroLocalData.Add(id, unit);
            }

            //技能解析
            SkillLocalData = new Dictionary<int, SkillUnit>();
            _DictionaryRoot root = FileUtils.LoadFromXml<_DictionaryRoot>(GPConfig.XML_GP_SKILLDEFAULT);
            DictionaryXml data = SwitchToDictionaryXml(root);
            foreach (var temp in data)
            {
                int id = 0;
                int.TryParse(temp.Key, out id);
                SkillUnit unit = new SkillUnit();
                unit.ReadStream(temp.Value, id);
                SkillLocalData.Add(id, unit);
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
            CellReqGameStart cellReqGame = new CellReqGameStart();
            gameState = GameState.NONE;
            cellReqGame.SaveData(0);//roomid : 0
            SendGamePacket(cellReqGame);
        }
        public override void SendGamePacket(Cell_Base data)
        {
            if(GameController.GetInstance().GetLineNetState())
            {
                Debug.Log("发送单机服务器");
                GPConsoleServer.GetInstance().OnPacketDealCenter(data);
            }
            else
            {

            }
        }
        public override void OnRecivePacket(object _object, byte[] buffer)
        {
            switch ((GameCMD)_object)
            {
                case GameCMD.CMD_GAME_RESPSTARTGAME:
                    
                    break;
                case GameCMD.CMD_GAME_PLAYERINFO:
                    
                    break;
            }
        }
        public override void OnReciveConsole(object _object, byte[] buffer)
        {
            switch ((GameCMD)_object)
            {
                case GameCMD.CMD_GAME_RESPSTARTGAME:
                    OnMsgRespStartGame();
                    break;
            }
        }
        private void OnMsgRespStartGame()
        {
            //第一次准备
            if(gameState == GameState.NONE)
            {
                gameState = GameState.READY;
                UIManager.GetInstance().OpenNode<UIGPInit>(UIConfig.UIGPInit);
                return;
            }
            
        }
        #endregion

        public override void LogicFixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}