using UnityEngine;
using UnityEngine.UI;
using Scripts.Logic;
using Scripts.Logic.PVPGame;
using Scripts.UI.PVPGame;
using System.Collections.Generic;

namespace Scripts.UI
{
    public class PVPGameScene : BaseScene
    {
        public override void OnEnter()
        {
            mainUI = UIManager.GetInstance().OpenNode<UIPVPGame>(UIConfig.UIPVPGame);
            UIManager.GetInstance().OpenNode<UIPVPGameInit>(UIConfig.UIPVPGameInit);
        }

        public override void ResetScene()
        {
            GameController.GetInstance().Clear();
        }
        
        
    }
}

