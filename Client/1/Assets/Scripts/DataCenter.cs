using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class DataCenter : SingletonMono<DataCenter>
    {
        public void Init()
        {
            UserDefault userDefault = FileUtils.LoadFromXml<UserDefault>(Config.XML_USERDEFAULT);
            bool isLoadLocal = false;
            //if(userDefault != null && userDefault._INTValues != null 
            //    && FileUtils.GetINTValueForKey(Config.KEY_EVENT_TAG) != 0) { isLoadLocal = true; }
            Dictionary<string, KeyCode> UpdateDic = new Dictionary<string, KeyCode>();
            foreach (var temp in DIC_DEFAULT_KEY_VALUE)
            {
                string KeyStr = FileUtils.GetSTRValueForKey(temp.Key);
                if(KeyStr != "")
                {
                    DIC_DEFAULT_KEY_VALUE[temp.Key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyStr);
                }
            }
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
        private Dictionary<string, KeyCode> DIC_DEFAULT_KEY_VALUE = new Dictionary<string, KeyCode>()
        {
            {Config.KEY_UP,KeyCode.W },
            {Config.KEY_LEFT,KeyCode.A },
            {Config.KEY_DOWN,KeyCode.S },
            {Config.KEY_RIGHT,KeyCode.D },

            {Config.KEY_ATTACK,KeyCode.Mouse0 },
        };
        public void SetKeyEvent(string KeyName,KeyCode _KeyCode)
        {
            if (DIC_DEFAULT_KEY_VALUE.ContainsKey(KeyName))
            {
                DIC_DEFAULT_KEY_VALUE[KeyName] = _KeyCode;
            }
            else
                DIC_DEFAULT_KEY_VALUE.Add(KeyName, _KeyCode);
        }
        public string GetKeyEvent(KeyCode _Value)
        {
            if (_Value == KeyCode.None || !DIC_DEFAULT_KEY_VALUE.ContainsValue(_Value))
            {
                return "";
            }

            foreach(var temp in DIC_DEFAULT_KEY_VALUE)
            {
                if(temp.Value == _Value)
                {
                    return temp.Key;
                }
            }
            return "";
        }
        public string GetKeyValue(string _Key)
        {
            if (_Key == "" || !DIC_DEFAULT_KEY_VALUE.ContainsKey(_Key))
            {
                return "";
            }

            return DIC_DEFAULT_KEY_VALUE[_Key].ToString();
        }

        public void OnDestroy()
        {
            //FileUtils.SetValueForKey(Config.KEY_EVENT_TAG, 1);
            foreach (var temp in DIC_DEFAULT_KEY_VALUE)
            {
                FileUtils.SetValueForKey(temp.Key, temp.Value.ToString());
            }
            FileUtils.SaveCache();
            Debug.Log("DataCenter OnDestroy");
        }

        public void FixedUpdate()
        {
            
        }
    }
}
