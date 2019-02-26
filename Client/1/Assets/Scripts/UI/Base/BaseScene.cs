using Scripts.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.UI
{
    public abstract class BaseScene
    {
        public string name { get; set; }
        public BaseUI mainUI;

        public abstract void OnEnter();
        public abstract void ResetScene();
        public virtual void OnExit()
        {
            ResetScene();
            
            UIManager.GetInstance().LoadScene(Config.LobbyScene);
        }
    }
}
