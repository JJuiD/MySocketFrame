using UnityEngine;
using UnityEngine.UI;
using Scripts.Logic;
using Scripts.Logic.PVPGame;

namespace Scripts.UI
{
    public class PVPGameSceen : BaseScene
    {
        public override void onEnter()
        {
            UIManager.GetInstance().OpenNode<UIPVPGame>(UIConfig.UIPVPGame);
            PVPGameLogic.GetInstance().Init();
        }

        public override void onExit()
        {

        }
    }
}
