using UnityEngine;
using UnityEngine.UI;
using Scripts.Logic;
using Scripts.Logic.GP;
using Scripts.UI.GP;
using System.Collections.Generic;

namespace Scripts.UI
{
    public class GPScene : BaseScene
    {
        private GameObject MapGrid;
        public override void OnEnter()
        {
            MapGrid = GameObject.Find("Grid");
            mainUI = UIManager.GetInstance().OpenNode<UIGP>(UIConfig.UIGP);
            UIManager.GetInstance().OpenNode<UIGPInit>(UIConfig.UIGPInit);
        }

        public void OnEnterGame()
        {
            
         }

        public void InitMapData(int mapindex)
        {

        }

        public override void ResetScene()
        {
            
        }
        
        
    }
}

