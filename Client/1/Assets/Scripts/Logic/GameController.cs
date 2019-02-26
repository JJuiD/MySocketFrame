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
            Logic.Init();
            Logic.LoadKeyEventDic(gameUserDefault);
        }

        //游戏地图
        private void InitMapData(int mapIndex = 0)
        {
            
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

