using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts
{

    public abstract class Singleton<T> 
        where T:new()
    {
        private static readonly T instance = new T();

        public static T GetInstance()
        {
            return instance;
        }
    }

    public abstract class SingletonMono<T> : MonoBehaviour
        where T : SingletonMono<T>
    {
        protected static T instance = null;

        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogError("More than 1!");
                    return instance;
                }

                if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    Debug.Log("Instance Name: " + instanceName);
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                        instanceGO = new GameObject(instanceName);
                    instance = instanceGO.AddComponent<T>();
                    DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
                    Debug.Log("Add New Singleton " + instance.name + " in Game!");
                }
                else
                {
                    Debug.Log("Already exist: " + instance.name);
                }
            }

            return instance;
        }


        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }

}
