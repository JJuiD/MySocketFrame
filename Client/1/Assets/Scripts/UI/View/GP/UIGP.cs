using Scripts.Logic.GP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.GP
{
    public class UIGP : BaseUI
    {
        private string WN_PNL_PlayerList = "WN_PNL_PlayerList";
        private string WN_PNL_PlayerInfo_ = "WN_PNL_PlayerInfo_";

        public override void Open(params object[] _params)
        {
           
        }

        public Transform GetUIPlayerInfo(int localseat)
        {
            return GetWNode(WN_PNL_PlayerInfo_ + localseat.ToString());
        }
        /// <summary>
        /// 设置玩家头像
        /// </summary>
        /// <param name="头像地址"></param>
        /// <param name="玩家索引"></param>
        public void UpdatePlayerHead(string path, int index = 0)
        {
            if (path == "") return;
            Transform playerNode = GetUIPlayerInfo(index);
            Transform headNode = playerNode.Find("head");
            Sprite headsprite = Resources.Load<Sprite>(path);
            if (headNode == null || headsprite == null) return;
            headNode.GetComponent<Image>().sprite = headsprite;
        }
        public void UpdatePlayerHead(Sprite head, int index = 0)
        {
            if (head == null) return;
            Transform playerNode = GetUIPlayerInfo(index);
            Transform headNode = playerNode.Find("head");
            if (headNode == null || head == null) return;
            headNode.GetComponent<Image>().sprite = head;
        }
        /// <summary>
        /// 设置玩家血条 蓝条
        /// </summary>
        /// <param name="类型"></param>
        /// <param name="百分比"></param>
        /// <param name="玩家索引"></param>
        public void UpdatePlayerState(CostType type, float value, int index = 0)
        {
            Transform playerNode = GetUIPlayerInfo(index);
            RectTransform _node_max = new RectTransform();
            RectTransform _node_cur = new RectTransform();
            switch (type)
            {
                case CostType.hp:
                    _node_max = playerNode.Find("hp_bg").GetComponent<RectTransform>();
                    _node_cur = playerNode.Find("hp_bg").Find("hp").GetComponent<RectTransform>();
                    break;
                case CostType.mp:
                    _node_max = playerNode.Find("mp_bg").GetComponent<RectTransform>();
                    _node_cur = playerNode.Find("mp_bg").Find("mp").GetComponent<RectTransform>();
                    break;
            }
            if (_node_max == null || _node_cur == null) return;
            _node_cur.sizeDelta = new Vector2(
                        _node_max.sizeDelta.x, _node_max.sizeDelta.y * value);
        }
    }

}
