using UnityEngine;
using UnityEngine.UI;
using Scripts.Logic;
using Scripts.Logic.PVPGame;
using Scripts.UI.PVPGame;

namespace Scripts.UI
{
    public class PVPGameScene : BaseScene
    {
        public override void onEnter()
        {
            UIManager.GetInstance().OpenNode<UIPVPGame>(UIConfig.UIPVPGame);
            UIManager.GetInstance().OpenNode<UIPVPGameInit>(UIConfig.UIPVPGameInit);
            GameController.GetInstance().GetLogic().Init();
        }

        public override void onExit()
        {

        }
    }
}
