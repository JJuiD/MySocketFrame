using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
    struct KEY_STATE
    {
        KeyCode down;
        KeyCode up;
    }

    public class GameController : SingletonMono<GameController>
    {
        public string GameName {get ; set;}

        private UserDefault gameUserDefault;
        private Dictionary<string, int> IntUesrDefault;
        private Dictionary<string, float> FltUserDefault;
        private Dictionary<string, string> StrUserDefault;
        private BaseLogic Logic;

        public T GetLogic<T>() where T : BaseLogic { return (T)Logic; }
        public UserDefault GetGameUserDefault() { return gameUserDefault; }

        public void Init()
        {
            Debug.Log("GameController Init " + GameName);
            switch(GameName)
            {
                case Config.PVPGameScene:
                    gameUserDefault = FileUtils.LoadFromXml<UserDefault>(Config.XML_PVPGAME_USERDEFAULT);
                    Logic = new PVPGame.PVPGameLogic();
                    break;
            }
            SaveUserDefault();
            Logic.Init();
            Logic.LoadKeyEventDic(gameUserDefault);
        }

        //游戏地图
        private void InitMapData(int mapIndex = 0)
        {
            Logic.InitMapData(mapIndex);
        }

        private void SaveUserDefault()
        {
            FltUserDefault = new Dictionary<string, float>();
            foreach (var temp in gameUserDefault._FLTValues)
            {
                FltUserDefault.Add(temp.name, temp.value);
            }

            IntUesrDefault = new Dictionary<string, int>();
            foreach (var temp in gameUserDefault._INTValues)
            {
                IntUesrDefault.Add(temp.name, temp.value);
            }

            StrUserDefault = new Dictionary<string, string>();
            foreach (var temp in gameUserDefault._STRValues)
            {
                StrUserDefault.Add(temp.name, temp.value);
            }
        }
        public T GetUserDefault<T>(string index) 
        {
            if(typeof(T) == typeof(int) && IntUesrDefault.ContainsKey(index))
            {
                return (T)((object)IntUesrDefault[index]);
            }
            else if (typeof(T) == typeof(float) && FltUserDefault.ContainsKey(index))
            {
                return (T)((object)FltUserDefault[index]);
            }
            else if (typeof(T) == typeof(string) && StrUserDefault.ContainsKey(index))
            {
                return (T)((object)StrUserDefault[index]);
            }

            throw new NotImplementedException();
        }

        public void JoinGame()
        {
            this.Logic.JoinGame();
        }

        public void Clear()
        {
            gameUserDefault = null;
            Logic = null;
        }

        private void FixedUpdate()
        {
            Logic.LogicFixedUpdate();
        }
    }
}

