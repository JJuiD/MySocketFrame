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

        public BaseLogic GetLogic() { return Logic; }

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

        }

        //游戏地图
        private void InitMapData(int mapIndex = 0)
        {
            
        }

        private KeyCode GetKeyDown()
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        return keyCode;
                    }

                    if(Input.GetKeyUp(keyCode))
                    {

                    }

                }
            }
            return KeyCode.None;
        }

        public void FixedUpdate()
        {
            Debug.Log(GetKeyDown().ToString());
        }

    }
}
