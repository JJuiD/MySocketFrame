using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.UI.GP;

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
        public GameObject rootUINode;
        private _XmlRoot rootNodeXml;
        

        void Awake()
        {
            UINodeList = new Dictionary<string, BaseUI>();
            rootNode = GameObject.Instantiate(Resources.Load<GameObject>("UI/RootCanvas"), Vector3.zero, Quaternion.identity);
            rootUINode = rootNode.transform.Find("UIRoot").gameObject;
            DontDestroyOnLoad(rootNode);
            InitUIPrefab();
        }

        #region 场景Load Realse
        private string CurSceneName = Config.Lobby;
        private BaseUI MainUI;
        public void LoadScene(string name,bool firstLoad = false)
        {
            if(firstLoad)
            {
                MainUI = OpenNode<UILobby>(UIConfig.UILobby);
                return;
            }
            RealseScene();
            CurSceneName = name;
            //先写这里 2.25
            SceneManager.LoadScene(name + "Scene");
            switch (name)
            {
                case Config.Lobby:
                    MainUI = OpenNode<UILobby>(UIConfig.UILobby);
                    break;
                case Config.GP:
                    MainUI = OpenNode<UIGP>(UIConfig.UIGP);
                    break;
            }
        }
        public void RealseScene()
        {
            if (CurSceneName == "") return;
            Debug.Log("Realse " + CurSceneName);
            //清楚上个场景的UI节点
            foreach(Transform temp in rootUINode.transform)
            {
                Destroy(temp.gameObject);
            }
            UINodeList.Clear();
        }
        public string GetSceneName() {
            return CurSceneName;
        }
        public T GetMainUI<T>() where T:BaseUI
        {
            return (T)MainUI;
        }
        #endregion

        #region UI 相关操作 LoadNode RealseNode
        private Dictionary<string, BaseUI> UINodeList;
        public T OpenNode<T>(string name,params object[] _params) where T : BaseUI
        {
            if (!UIPrefabList.ContainsKey(name)) return null;
            int zOrder = rootUINode.transform.childCount;
            Transform nodeRoot = rootUINode.transform;
            GameObject node = GameObject.Instantiate(UIPrefabList[name],nodeRoot);
            //node.transform.SetParent(rootNode.transform);
            node.transform.SetSiblingIndex(zOrder);
            //设置加载名字
            node.GetComponent<BaseUI>()._UIName = name;
            //设置显示名字
            if (UINodeList.ContainsKey(name)) name = name + "_" + zOrder.ToString();
            node.transform.name = name;
            //加入节点表
            UINodeList.Add(name, node.GetComponent<BaseUI>());
            //初始化按键绑定 打开节点
            node.GetComponent<BaseUI>().Init();
            node.GetComponent<BaseUI>().Open(_params);
            return node.GetComponent<T>();
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
            Transform Btn = baseUI.GetWNode(nodeName);
            if (Btn == null) return;
            Button tempBtn = Btn.GetComponent<Button>();
            if (tempBtn != null)
            {
                tempBtn.onClick.AddListener(func);
            }
            else Debug.LogError(tempBtn.name + "Button控件不存在");
        }
        public void RegisterClickEvent(GameObject node, UnityAction func)
        {
            Button tempBtn = node.GetComponent<Button>();
            if (tempBtn != null)
            {
                tempBtn.onClick.AddListener(func);
            }
            else Debug.LogError(tempBtn.name + "Button控件不存在");
        }
        public void RemoveAllClickListener(GameObject node)
        {
            Button tempBtn = node.GetComponent<Button>();
            if (tempBtn != null)
            {
                tempBtn.onClick.RemoveAllListeners();
            }
            else Debug.LogError(tempBtn.name + "Button控件不存在");
        }

        #endregion

        /// <summary>
        /// 初始化UI相关资源路径
        /// </summary>
        private Dictionary<string, GameObject> UIPrefabList;
        private void InitUIPrefab()
        {
            rootNodeXml = FileUtils.LoadFromXml<_XmlRoot>(Config.XML_UIDEFAULT);
            UIPrefabList = new Dictionary<string, GameObject>();
            if (rootNodeXml.ViewList == null) return;
            foreach (ViewToPath temp in rootNodeXml.ViewList)
            {
                UIPrefabList.Add(temp.name, Resources.Load<GameObject>(temp.path));
            }
        }

        #region 操作UI 
        public void SetUIXPosition(Transform node,float posX)
        {
            if (node == null) return;
            Vector3 _nodePos = node.position;
            node.position = new Vector2(posX,_nodePos.y);
        }
        public void SetUIYPosition(Transform node,float posY)
        {
            if (node == null) return;
            Vector3 _nodePos = node.position;
            node.position = new Vector2(_nodePos.x, posY);
        }
        #endregion
    }

}
