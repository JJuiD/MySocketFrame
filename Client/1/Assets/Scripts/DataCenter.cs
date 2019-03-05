using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class DataCenter : SingletonMono<DataCenter>
    {
        public string LocalName { get; set; }

        public void InitData()
        {
            tUserDefault = new List<UserDefaultData>();
            InsertUserDefault(Config.Lobby, Config.XML_USERDEFAULT);
            LocalName = GetUserDefault().GetUserDefaultValue<string>("localName");
        }

        protected override void OnDestroy()
        {
            //FileUtils.SetValueForKey(Config.KEY_EVENT_TAG, 1);
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

    }
}
