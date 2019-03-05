using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Scripts
{
    #region UIDefault.xml
    [XmlRootAttribute("_XmlRoot", IsNullable = false)]
    public class _XmlRoot
    {
        [XmlArrayAttribute("View")]
        public ViewToPath[] ViewList { get; set; }
    }

    //public class _Scene
    //{
    //    [XmlAttribute("name")]
    //    public string name { get; set; }

    //    [XmlArrayAttribute("View")]
    //    public ViewToPath[] _ViewToPaths { get; set; }
    //}

    public class ViewToPath
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlText]
        public string path { get; set; }

    }

    //ViewToPath area2 = new ViewToPath();
    //area2.name = "UILoading";
    //area2.path = "UI/UILoading";

    //_Scene nodelist = new _Scene();
    //nodelist.name = "SimpleGame";
    //nodelist._ViewToPaths = new ViewToPath[] {  area2 };

    //_XmlRoot city1 = new _XmlRoot();
    //city1.ViewList = new _Scene[] { nodelist };

    //FileUtils.SaveToXml("UIPrefabXml.xml", city1, typeof(_XmlRoot),"root");
    //Console.WriteLine("SDASDA");
    #endregion

    #region UserDefault.xml
    [XmlRootAttribute("UserDefault", IsNullable = false)]
    public class UserDefault
    {
        [XmlArrayAttribute("STRValueNode")]
        public List<STRValue> _STRValues = new List<STRValue>();

        [XmlArrayAttribute("INTValueNode")]
        public List<INTValue> _INTValues = new List<INTValue>();

        [XmlArrayAttribute("FLTValueNode")]
        public List<FLTValue> _FLTValues = new List<FLTValue>();
    }

    public class STRValue 
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlText]
        public string value { get; set; }
    }

    public class INTValue 
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlText]
        public int value { get; set; }
    }

    public class FLTValue 
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlText]
        public float value { get; set; }
    }

    public class UserDefaultData
    {
        UserDefault data;
        string path;
        string name;
        Dictionary<string, float> FltUserDefault ;
        Dictionary<string, int> IntUesrDefault;
        Dictionary<string, string> StrUserDefault;
        public void Init(string name, string path)
        {
            this.name = name;
            this.path = path;
            data = FileUtils.LoadFromXml<UserDefault>(path);
            ClassifyData();
        }
        private void ClassifyData()
        {
            FltUserDefault = new Dictionary<string, float>();
            foreach (var temp in data._FLTValues)
            {
                FltUserDefault.Add(temp.name, temp.value);
            }

            IntUesrDefault = new Dictionary<string, int>();
            foreach (var temp in data._INTValues)
            {
                IntUesrDefault.Add(temp.name, temp.value);
            }

            StrUserDefault = new Dictionary<string, string>();
            foreach (var temp in data._STRValues)
            {
                StrUserDefault.Add(temp.name, temp.value);
            }
        }
        public T GetUserDefaultValue<T>(string index)
        {
            string name = UI.UIManager.GetInstance().GetSceneName();
            if (typeof(T) == typeof(int) && IntUesrDefault.ContainsKey(index))
            {
                return (T)((object)IntUesrDefault[index]);
            }
            else if (typeof(T) == typeof(float) && FltUserDefault.ContainsKey(index))
            {
                return (T)((object)FltUserDefault[index]);
            }
            else if (typeof(T) == typeof(string) && StrUserDefault.ContainsKey(index))
            {
                return (T)((object)StrUserDefault[index]);
            }

            throw new NotImplementedException();
        }
        public string GetName() { return name; }
        public string GetPath() { return path; }
        public UserDefault GetData() { return data; }
    }
    #endregion

    #region CardDefault.xml
    //[XmlRootAttribute("_XmlCardRoot", IsNullable = false)]
    //public class _XmlCardRoot
    //{
    //    [XmlArrayAttribute("Card")]
    //    public _XmlCard[] CardList { get; set; }
    //}

    //public class _XmlCard
    //{

    //    [XmlAttribute("name")]
    //    public string name { get; set; }

    //    [XmlArrayAttribute("CardInfo")]

    //    public _CardInfo[] _CardInfoList { get; set; }
    //}

    //public class _CardInfo
    //{
    //    [XmlAttribute("name")]
    //    public string name { get; set; }

    //    [XmlText]
    //    public string value { get; set; }
    //}


    #endregion

    #region PVPGame

    #region PVPGameHeroDefault.xml
    [XmlRootAttribute("_DictionaryRoot", IsNullable = false)]
    public class _DictionaryRoot
    {
        [XmlArrayAttribute("ViewList")]
        public _ViewList[] ViewList { get; set; }
    }

    public class _ViewList
    {

        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlArrayAttribute("InfoList")]

        public _Info[] _InfoList { get; set; }
    }

    public class _Info
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlText]
        public string value { get; set; }
    }
    #endregion

    #endregion

}
