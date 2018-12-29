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
        where T : new()
    {
        private static readonly T instance = new T();

        public static T GetInstance()
        {
            return instance;
        }
    }

}
