//using UnityEngine;
//using UnityEngine.UI;
//using Scripts.Logic;
//using Scripts.Logic.GP;
//using Scripts.UI.GP;
//using System.Collections.Generic;

//namespace Scripts.UI
//{
//    public class GPScene : BaseScene
//    {
//        private GameObject MapGrid;
//        public UIGP GetMainUI() { return (UIGP)mainUI; }
//        public override void OnEnter()
//        {
//            MapGrid = GameObject.Find("Grid");
//            mainUI = UIManager.GetInstance().OpenNode<UIGP>(UIConfig.UIGP);
//            UIManager.GetInstance().OpenNode<UIGPInit>(UIConfig.UIGPInit);
//        }

//        public override void ResetScene()
//        {
            
//        }

//        /// <summary>
//        /// 设置玩家头像
//        /// </summary>
//        /// <param name="头像地址"></param>
//        /// <param name="玩家索引"></param>
//        public void UpdatePlayerHead(string path,int index = 0)
//        {
//            if (path == "") return;
//            Transform playerNode = GetMainUI().GetUIPlayerInfo(index);
//            Transform headNode = playerNode.Find("head");
//            Sprite headsprite = Resources.Load<Sprite>(path);
//            if (headNode == null || headsprite == null) return;
//            headNode.GetComponent<Image>().sprite = headsprite;
//        }
//        /// <summary>
//        /// 设置玩家血条 蓝条
//        /// </summary>
//        /// <param name="类型"></param>
//        /// <param name="百分比"></param>
//        /// <param name="玩家索引"></param>
//        public void UpdatePlayerState(CostType type,float value,int index = 0)
//        {
//            Transform playerNode = GetMainUI().GetUIPlayerInfo(index);
//            RectTransform _node_max = new RectTransform();
//            RectTransform _node_cur = new RectTransform();
//            switch (type)
//            {
//                case CostType.hp:
//                    _node_max = playerNode.Find("hp_bg").GetComponent<RectTransform>();
//                    _node_cur = playerNode.Find("hp_bg").Find("hp").GetComponent<RectTransform>();
//                    break;
//                case CostType.mp:
//                    _node_max = playerNode.Find("mp_bg").GetComponent<RectTransform>();
//                    _node_cur = playerNode.Find("mp_bg").Find("mp").GetComponent<RectTransform>();
//                    break;
//            }
//            if (_node_max == null || _node_cur == null) return;
//            _node_cur.sizeDelta = new Vector2(
//                        _node_max.sizeDelta.x, _node_max.sizeDelta.y * value);
//        }
//    }
//}

