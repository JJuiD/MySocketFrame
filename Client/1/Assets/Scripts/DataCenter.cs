using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public enum DataEventType
    {
        EVENT_KEY_DOWN,
        EVENT_KEY_UP,
    }

    public class DataCenter : SingletonMono<DataCenter>
    {
        public void Init()
        {
            //Dictionary<string, KeyCode> UpdateDic = new Dictionary<string, KeyCode>();
            //foreach (var temp in DIC_DEFAULT_KEY_VALUE)
            //{
            //    string KeyStr = FileUtils.GetSTRValueForKey(temp.Key);
            //    if(KeyStr != "")
            //    {
            //        DIC_DEFAULT_KEY_VALUE[temp.Key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyStr);
            //    }
            //}
        }

        //按键
        //输入KeyCode 
        //private Dictionary<string, KeyCode> DIC_KEY_EVENTNAME = new Dictionary<string, KeyCode>();
        //private KeyCode LoadKeyCodeFromLocal(string Key)
        //{
        //    string STRValue = FileUtils.GetSTRValueForKey(Key);
        //    if (System.Enum.IsDefined(typeof(KeyCode), STRValue))
        //    {
        //        return (KeyCode)System.Enum.Parse(typeof(KeyCode), STRValue); 
        //    }
        //    else
        //    {
        //        return Config.DIC_DEFAULT_KEY_VALUE[Key];
        //    }
        //}
        //private Dictionary<string, KeyCode> DIC_DEFAULT_KEY_VALUE = new Dictionary<string, KeyCode>()
        //{
        //    {Config.KEY_UP,KeyCode.W },
        //    {Config.KEY_LEFT,KeyCode.A },
        //    {Config.KEY_DOWN,KeyCode.S },
        //    {Config.KEY_RIGHT,KeyCode.D },

        //    {Config.KEY_ATTACK,KeyCode.Mouse0 },
        //};
        //public void SetKeyEvent(string KeyName,KeyCode _KeyCode)
        //{
        //    if (DIC_DEFAULT_KEY_VALUE.ContainsKey(KeyName))
        //    {
        //        DIC_DEFAULT_KEY_VALUE[KeyName] = _KeyCode;
        //    }
        //    else
        //        DIC_DEFAULT_KEY_VALUE.Add(KeyName, _KeyCode);
        //}
        //public string GetKeyEvent(KeyCode _Value)
        //{
        //    if (_Value == KeyCode.None || !DIC_DEFAULT_KEY_VALUE.ContainsValue(_Value))
        //    {
        //        return "";
        //    }

        //    foreach(var temp in DIC_DEFAULT_KEY_VALUE)
        //    {
        //        if(temp.Value == _Value)
        //        {
        //            return temp.Key;
        //        }
        //    }
        //    return "";
        //}
        //public string GetKeyValue(string _Key)
        //{
        //    if (_Key == "" || !DIC_DEFAULT_KEY_VALUE.ContainsKey(_Key))
        //    {
        //        return "";
        //    }

        //    return DIC_DEFAULT_KEY_VALUE[_Key].ToString();
        //}

        public void OnDestroy()
        {
            //FileUtils.SetValueForKey(Config.KEY_EVENT_TAG, 1);
            //foreach (var temp in DIC_DEFAULT_KEY_VALUE)
            //{
            //    FileUtils.SetValueForKey(temp.Key, temp.Value.ToString());
            //}
            FileUtils.SaveCache();
            Debug.Log("DataCenter OnDestroy");
        }

        private KeyCode GetKeyDown()
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        SendData(DataEventType.EVENT_KEY_DOWN, keyCode.ToString());
                    }

                    if (Input.GetKeyUp(keyCode))
                    {
                        SendData(DataEventType.EVENT_KEY_UP,keyCode.ToString());
                    }

                }
            }
            return KeyCode.None;
        }

        public void FixedUpdate()
        {
            //Debug.Log(GetKeyDown().ToString());
        }

        private Dictionary<DataEventType, Action<object[]>> listenerList ;
        public void AddDataListener(DataEventType eventType, Action<object[]> listener)
        {
            if (listenerList.Count == 0 || listenerList.ContainsKey(eventType)) return;
            listenerList.Add(eventType, listener);
        }
        public void RemoveDataListener(DataEventType eventType)
        {
            if (listenerList.Count == 0) return;
            if (listenerList.ContainsKey(eventType))
            {
                listenerList.Remove(eventType);
            }
        }
        public void SendData(DataEventType eventType, params object[] args)
        {
            if (listenerList.Count == 0) return;
            if (listenerList.ContainsKey(eventType))
            {
                listenerList[eventType](args);
            }
        }
    }
}
