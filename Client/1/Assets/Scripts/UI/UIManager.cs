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
            initUIPrefab();
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
        public void OpenNode(string name,params object[] _params)
        {
            if (!UIPrefabList.ContainsKey(name)) return;
            int zOrder = rootNode.transform.childCount;
            Transform nodeRoot = rootNode.transform;
            GameObject node = GameObject.Instantiate(UIPrefabList[name],nodeRoot);
            //node.transform.SetParent(rootNode.transform);
            node.transform.SetSiblingIndex(zOrder);
            if(UINodeList.ContainsKey(name)) name = name + "_" + zOrder.ToString();
            node.transform.name = name;
            UINodeList.Add(name, node.GetComponent<BaseUI>());
            node.GetComponent<BaseUI>().Open(_params);
        }
        public void RealseNode(string name)
        {
            int index = 0;
            if (UINodeList.ContainsKey(name))
            {
                index = UINodeList[name].transform.GetSiblingIndex();
                UINodeList.Remove(name);
            }
            
            //Transform[] transforms = rootNode.transform.GetComponentsInChildren<Transform>();
            //for(;index < transforms.Length; ++index)
            //{
            //    transforms[index].SetSiblingIndex(index);
            //}
        }

        #endregion

        #region 事件相关 注册
        public void RegisterClickEvent(string nodeName,BaseUI baseUI,UnityAction func)
        {
            Transform Btn = baseUI.GetWMNode(nodeName);
            if (Btn == null) return;
            Button tempBtn = Btn.GetComponent<Button>();
            if (tempBtn != null)
            {
                tempBtn.onClick.AddListener(func);
            }
            else Debug.LogError(tempBtn.name + "Button控件不存在");
        }
        public void RegisterClickEvent(Transform nodeName, BaseUI baseUI, UnityAction func)
        {
            Button tempBtn = nodeName.GetComponent<Button>();
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
        private Dictionary<string, GameObject> UIPrefabList;
        private void initUIPrefab()
        {
            rootNodeXml = FileUtils.LoadFromXml<_XmlRoot>(Config.XML_UIDEFAULT);
            UIPrefabList = new Dictionary<string, GameObject>();
            if (rootNodeXml.ViewList == null) return;
            foreach (ViewToPath temp in rootNodeXml.ViewList)
            {
                UIPrefabList.Add(temp.name, Resources.Load<GameObject>(temp.path));
            }
        }
    }

}
