using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public string _UIName { get;set; }
        public virtual void Init()
        {
            InitWNodes();
        }

        #region BaseUI Operator Open,Close
        public abstract void Open(params object[] _params);

        public virtual void Close()
        {
            UIManager.GetInstance().RealseNode(name);
            Destroy(this.gameObject);
        }
        #endregion

        #region WNode Operator
        private Dictionary<string, Transform> WNodes;
        public Transform GetWNode(string nodename)
        {
            if(WNodes.ContainsKey(nodename))
            {
                return WNodes[nodename];
            }
            Debug.LogError(nodename + " 节点不存在");
            return null;
        }
        private void InitWNodes()
        {
            WNodes = new Dictionary<string, Transform>();
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
                    WNodes.Add(temp.name, temp);
                }
            }
        }
        #endregion
    }
}
