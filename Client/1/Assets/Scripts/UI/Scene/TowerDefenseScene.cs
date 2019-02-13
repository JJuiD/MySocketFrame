using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TowerDefenseScene : BaseScene
    {
        public override void onEnter()
        {
            UIManager.GetInstance().LoadNode("UITowerDefense");
        }

        public override void onExit()
        {

        }
    }
}
