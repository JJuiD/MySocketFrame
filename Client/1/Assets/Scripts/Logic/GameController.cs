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
        public void init()
        {

        }


        //游戏地图
        private void initMapData(int mapIndex = 0)
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
