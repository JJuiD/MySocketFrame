using Proto;
using Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{

    public class GameController : Singleton<GameController>
    {
        /// <summary>
        /// 游戏逻辑
        /// </summary>
        public LogicBase Logic { get; set; }
        /// <summary>
        /// 是否为单机
        /// </summary>
        private bool isConsole;
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isConsole"></param>
        public void StartGame(string name, bool isConsole = true)
        {
            Debug.Log("GameController Init " + name);
            SetLineNetState(isConsole);
            switch (name)
            {
                case Config.GP:

                    Logic = new GP.GPLogic();
                    break;
            }
            Logic.InitData();
            UIManager.GetInstance().LoadScene(name);
        }


        #region 网络状态
        /// <summary>
        /// 获取网络状态 (True: 离线)
        /// </summary>
        /// <returns></returns>
        public bool GetLineNetState() { return isConsole; }
        private void SetLineNetState(bool isConsole) { this.isConsole = isConsole; }
        #endregion


        #region 玩家相关

        #endregion

    }
}

