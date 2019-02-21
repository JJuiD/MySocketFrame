using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public virtual void Init()
        {
            InitWMNodes();
        }

        #region BaseUI Operator Open,Close
        public abstract void Open(params object[] _params);

        public virtual void Close()
        {
            UIManager.GetInstance().RealseNode(name);
            Destroy(this.gameObject);
        }
        #endregion

        #region WMNode Operator
        private Dictionary<string, Transform> WMNodes;
        public Transform GetWMNode(string nodename)
        {
            if(WMNodes.ContainsKey(nodename))
            {
                return WMNodes[nodename];
            }
            Debug.LogError(nodename + " 节点不存在");
            return null;
        }
        private void InitWMNodes()
        {
            WMNodes = new Dictionary<string, Transform>();
            SearchWNNode(this.transform);
        }
        private void SearchWNNode(Transform transform)
        {
            foreach (Transform temp in transform)
            {
                if (temp.childCount > 0)
                {
                    SearchWNNode(temp);
                }

                if (temp.name.Contains("WN"))
                {
                    WMNodes.Add(temp.name, temp);
                }
            }
        }
        #endregion
    }
}
