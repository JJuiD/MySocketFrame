using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class LobbyScene : BaseScene
    {
        public override void onEnter()
        {
            UIManager.GetInstance().OpenNode<UILobby>(UIConfig.UILobby);
        }

        public override void onExit()
        {

        }
    }
}
