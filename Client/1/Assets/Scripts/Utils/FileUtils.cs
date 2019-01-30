using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Scripts
{
    public static class FileUtils
    {
        private static string XmlDefaultPath = @"Assets/Resources/Local/";
        public static bool isFileExists(ref string filename)
        {
            bool isExist = File.Exists(filename);
            if (!isExist)
            {
                filename = XmlDefaultPath + filename;
                isExist = File.Exists(filename);
            }

            return isExist;
        }

        #region UserDefault.xml 表的相关操作(类型有float int string)
        private static UserDefault userDefault = new UserDefault();
        public static string GetSTRValueForKey(string Key, string value = "")
        {
            if (userDefault._STRValues.Count > 0)
            {
                foreach (var temp in userDefault._STRValues)
                {
                    if (temp.name == Key)
                    {
                        return temp.value;
                    }
                }
            }

            STRValue _STRValue = new STRValue();
            _STRValue.name = Key;
            _STRValue.value = value;
            userDefault._STRValues.Add(_STRValue);
            return value;
        }
        public static int GetINTValueForKey(string Key, int value = 0)
        {
            if (userDefault._INTValues.Count > 0)
            {
                foreach (var temp in userDefault._INTValues)
                {
                    if (temp.name == Key)
                    {
                        return temp.value;
                    }
                }
            }

            INTValue _INTValue = new INTValue();
            _INTValue.name = Key;
            _INTValue.value = value;
            userDefault._INTValues.Add(_INTValue);
            return value;
        }
        public static float GetFLTValueForKey(string Key, float value = 0)
        {
            if (userDefault._FLTValues.Count > 0)
            {
                foreach (var temp in userDefault._FLTValues)
                {
                    if (temp.name == Key)
                    {
                        return temp.value;
                    }
                }
            }

            FLTValue _FLTValue = new FLTValue();
            _FLTValue.name = Key;
            _FLTValue.value = value;
            userDefault._FLTValues.Add(_FLTValue);
            return value;
        }
        public static void SetValueForKey(string Key, string value = "")
        {
            GetSTRValueForKey(Key);
            foreach (var temp in userDefault._STRValues)
            {
                if (temp.name == Key) temp.value = value;
            }
        }
        public static void SetValueForKey(string Key, int value = 0)
        {
            GetINTValueForKey(Key);
            foreach (var temp in userDefault._INTValues)
            {
                if (temp.name == Key) temp.value = value;
            }
        }
        public static void SetValueForKey(string Key, float value = 0)
        {
            GetFLTValueForKey(Key);
            foreach (var temp in userDefault._FLTValues)
            {
                if (temp.name == Key) temp.value = value;
            }
        }
        #endregion


        public static void SaveCache()
        {
            SaveToXml<UserDefault>("UserDefault.xml", userDefault, "UserDefault");
        }

        public static void SaveToXml<T>(string filePath, object sourceObj, string xmlRootName)
        {
            if (!string.IsNullOrEmpty(filePath) && sourceObj != null)
            {
                filePath = XmlDefaultPath + filePath;
                Type type = typeof(T);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializer xmlSerializer = string.IsNullOrEmpty(xmlRootName) ?
                        new XmlSerializer(type) :
                        new XmlSerializer(type, new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public static T LoadFromXml<T>(string filePath) where T : new()
        {
            T result = new T();
            if (isFileExists(ref filePath))
            {
                Type type = typeof(T);
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    result = (T)xmlSerializer.Deserialize(reader);
                }
            }
            return result;
        }

    }
}
