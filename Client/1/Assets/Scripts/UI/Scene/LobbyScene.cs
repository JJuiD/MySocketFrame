using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class LobbyScene : BaseScene
    {
        public override void OnEnter()
        {
            mainUI = UIManager.GetInstance().OpenNode<UILobby>(UIConfig.UILobby);
        }

        public override void ResetScene()
        {
            
        }
    }
}
