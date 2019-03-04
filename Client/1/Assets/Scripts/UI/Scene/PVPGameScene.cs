﻿using UnityEngine;
using UnityEngine.UI;
using Scripts.Logic;
using Scripts.Logic.PVPGame;
using Scripts.UI.PVPGame;
using System.Collections.Generic;

namespace Scripts.UI
{
    public class PVPGameScene : BaseScene
    {
        private GameObject MapGrid;
        public override void OnEnter()
        {
            MapGrid = GameObject.Find("Grid");
            mainUI = UIManager.GetInstance().OpenNode<UIPVPGame>(UIConfig.UIPVPGame);
            UIManager.GetInstance().OpenNode<UIPVPGameInit>(UIConfig.UIPVPGameInit);
        }

        public void OnEnterGame()
        {
            for(int i = 0;i<GameController.GetInstance().GetMaxPlayer();++i)
            {
                PVPGamePlayer player = GameController.GetInstance().GetPlayerBySeat<PVPGamePlayer>(i);
                if(player != null)
                    player.Create(Vector3.zero);
            }
        }

        public void InitMapData(int mapindex)
        {

        }

        public override void ResetScene()
        {
            GameController.GetInstance().Clear();
        }
        
        
    }
}

