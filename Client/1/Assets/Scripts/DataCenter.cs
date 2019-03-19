using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public enum KeyState
    {
        realse,
        realseToPress,
        press,
        pressToRealse,
    }

    public class KeyUnit
    {
        #region 键值 与 事件名
        KeyCode key;
        public string eventName = "";
        public void ResetData()
        {
            upTime = 0;
            lerpTime = 0;
            downtime = 0;
            isDown = false;
            state = KeyState.realse;
        }
        #endregion

        #region 是否连点
        float lerpTime = 0;
        float downtime = 0;
        float upTime = 0;
        public bool IsClickDouble()
        {
            return downtime - upTime > 0 && downtime - upTime + lerpTime < 0.5f;
        }
        #endregion

        public bool isDown = false;
        public KeyState state = KeyState.realse;
        public void Update()
        {
            bool TempBool = isDown;
            isDown = Input.GetKey(key);
            if (isDown)
            {
                state = KeyState.press;
                if (!TempBool) state = KeyState.realseToPress;
                downtime = !TempBool ? Time.time : downtime;
            }
            else
            {
                upTime = Time.time;
                lerpTime = upTime - downtime;
                state = KeyState.realse;
                if (TempBool)
                {
                    state = KeyState.pressToRealse;
                    Debug.Log(eventName + " Realse!");
                }
            }
        }
    }

    public class DataCenter : SingletonMono<DataCenter>
    {
        public string LocalName { get; set; }

        public void InitData()
        {
            tUserDefault = new List<UserDefaultData>();
            //KeyEventList = new List<KeyUnit>();
            //EventAction = delegate (List<KeyUnit> units) { };
            InsertUserDefault(Config.Lobby, Config.XML_USERDEFAULT);
            LocalName = GetUserDefault().GetUserDefaultValue<string>("localName");
        }

        protected override void OnDestroy()
        {
            //FileUtils.SetValueForKey(Config.KEY_TAG, 1);
            //foreach (var temp in DIC_DEFAULT_KEY_VALUE)
            //{
            //    FileUtils.SetValueForKey(temp.Key, temp.Value.ToString());
            //}
            foreach(var temp in tUserDefault)
            {
                FileUtils.SaveCache<UserDefault>(temp.GetData(),temp.GetPath());
            }
            Debug.Log("DataCenter OnDestroy");
        }

        #region UserDefault
        private List<UserDefaultData> tUserDefault;
        private Dictionary<string, Dictionary<string, int>> tIntUesrDefault;
        private Dictionary<string, Dictionary<string, float>> tFltUserDefault;
        private Dictionary<string, Dictionary<string, string>> tStrUserDefault;
        public void InsertUserDefault(string name,string path)
        {
            UserDefaultData data = new UserDefaultData();
            data.Init(name, path);
            tUserDefault.Add(data);
        }
        public void RemoveUserDefault(string name)
        {
            UserDefaultData temp = GetUserDefault(name);
            if (temp == null) return;
            tUserDefault.Remove(temp);
        }
        public UserDefaultData GetUserDefault(string name = "")
        {
            name = name == "" ? UI.UIManager.GetInstance().GetSceneName() : name;
            foreach(var temp in tUserDefault)
            {
                if (temp.GetName() == name)
                    return temp;
            }
            
            return null;
        }
        #endregion

        #region KeyEvent
        //List<KeyUnit> KeyEventList;
        //Action<List<KeyUnit>> EventAction;
        //public void AddKeyListener(KeyUnit unit,int index = 0)
        //{
        //    KeyEventList.Add(unit);
        //}
        //public void SetKeyEventAction(Action<List<KeyUnit>> action)
        //{
        //    if (KeyEventList == null || KeyEventList.Count == 0) return;
        //    EventAction = action;
        //}
        //public void ClearKeyList()
        //{
        //    if (KeyEventList != null) KeyEventList.Clear();
        //}
        //public void UpdateKeyState()
        //{
        //    if (KeyEventList == null || KeyEventList.Count == 0) return;
        //    foreach (var temp in KeyEventList)
        //    {
        //        temp.Update();
        //    }
        //    EventAction(KeyEventList);
        //}
        #endregion

       
    }
}
