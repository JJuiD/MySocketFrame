using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

/*
UI命名
Text      : TXT
Image     : IMG
Raw Image : RMG
Button    : BTN
Toggle    : TOG
Pannel    : PNL
*/

namespace Scripts.UI
{
    public class UIManager : SingletonMono<UIManager>
    {
        public GameObject rootNode;
        private _XmlRoot rootNodeXml;
        

        public UIManager()
        {
            rootNode = GameObject.Instantiate(Resources.Load<GameObject>("UI/Root"), Vector3.zero, Quaternion.identity);
            rootNode.name = "UIRoot";
            DontDestroyOnLoad(rootNode);
            initUIPath();
        }

        #region 场景Load Realse
        private BaseScene CurScene;
        public void LoadScene(string name)
        {
            if (CurScene != null && name == CurScene.name) return;
            RealseScene();
            switch (name)
            {
                case UIConfig.LobbyScene:
                    CurScene = new LobbyScene();
                    CurScene.name = UIConfig.LobbyScene;
                    break;
            }
            CurScene.onEnter();
        }
        public void RealseScene()
        {
            if (CurScene == null) return;
            Debug.Log("Realse " + CurScene.name);
            CurScene.onExit();
        }
        #endregion

        #region UI 相关操作 LoadNode RealseNode
        private Dictionary<string, BaseUI> UINodeList = new Dictionary<string, BaseUI>();
        private Dictionary<string, List<Transform>> DICWNNode = new Dictionary<string, List<Transform>>();
        /// <summary>
        /// 加载节点
        /// </summary>
        /// <param 节点名></param>
        /// <param 层级></param>
        /// <param 是否唯一></param>
        public void LoadNode(string name,int zOrder = -1)
        {
            GameObject node ;
            if (!UIPathList.ContainsKey(name)) return;
            zOrder = zOrder < 0 ? rootNode.transform.childCount : Math.Min(zOrder, rootNode.transform.childCount);
            bool isClone = false;
            if (UINodeList.ContainsKey(name) && !isClone) { node = UINodeList[name].gameObject; }
            string path = UIPathList[name] ;
            Transform nodeRoot = rootNode.transform;

            node = GameObject.Instantiate(Resources.Load<GameObject>(path), rootNode.transform, false);
            node.transform.SetParent(rootNode.transform);
            node.transform.SetSiblingIndex(zOrder);
            node.transform.name = name;
            if (isClone) node.transform.name = name + "_" + zOrder.ToString();

            UINodeList.Add(name, node.GetComponent<BaseUI>());
            DICWNNode.Add(name, new List<Transform>());
            SearchWNNode(node.transform, node.transform.name);

            node.GetComponent<BaseUI>().onEnter();
            //node.GetComponent<BaseUI>().
        }
        public void RealseNode(string name)
        {
            int index = 0;
            if (UINodeList.ContainsKey(name))
            {
                index = UINodeList[name].transform.GetSiblingIndex();
                UINodeList[name].onExit();
                UINodeList.Remove(name);
            }
            //Transform[] transforms = rootNode.transform.GetComponentsInChildren<Transform>();
            //for(;index < transforms.Length; ++index)
            //{
            //    transforms[index].SetSiblingIndex(index);
            //}
        }
        /// <summary>
        /// 遍历节点筛选 "WN"开头节点
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="rootName"></param>
        private void SearchWNNode(Transform transform,string rootName)
        {
            foreach(Transform temp in transform)
            {
                if(temp.childCount > 0)
                {
                    SearchWNNode(temp, rootName);
                }

                if(temp.name.Contains("WN"))
                {
                    DICWNNode[rootName].Add(temp);
                }
            }
        }
        public Transform GetWNTransform(string name, BaseUI baseUI)
        {
            string rootName = baseUI.transform.name;
            if (DICWNNode.ContainsKey(rootName) && DICWNNode[rootName].Count > 0)
            {
                foreach (Transform temp in DICWNNode[rootName])
                {
                    if (temp.name == name)
                    {
                        return temp;
                    }
                }
            }
            Debug.LogError(name + " 节点不存在");
            return null;
        }
        #endregion

        #region 事件相关 注册
        public void RegisterClickEvent(string nodeName,BaseUI baseUI,UnityAction func)
        {
            Transform Btn = GetWNTransform(nodeName,baseUI);
            if (Btn == null) return;
            Button tempBtn = Btn.GetComponent<Button>();
            if (tempBtn != null)
            {
                tempBtn.onClick.AddListener(func);
            }
            else Debug.LogError(tempBtn.name + "Button控件不存在");
        }
        #endregion

        /// <summary>
        /// 初始化UI相关资源路径
        /// </summary>
        private Dictionary<string, string> UIPathList = new Dictionary<string, string>();
        private void initUIPath()
        {
            rootNodeXml = FileUtils.LoadFromXml<_XmlRoot>(Config.XML_UIDEFAULT);
            UIPathList = new Dictionary<string, string>();
            if (rootNodeXml.ViewList == null) return;
            foreach (ViewToPath temp in rootNodeXml.ViewList)
            {
                UIPathList.Add(temp.name, temp.path);
            }
        }
    }

}
