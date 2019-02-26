using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Scripts.UI;
using UnityEngine.UI;

namespace Scripts
{
    public abstract class Person
    {
        public string name;
        public Person(string n)
        {
            name = n;
        }
    }

    public class Student : Person
    {
       public Student(string n):base(n)
        {

        }
    }

    public class Teacher : Person
    {
        public Teacher(string n) : base(n)
        {

        }
    }

    public class main: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        private void Start()
        {
            DataCenter.GetInstance().Init();
            UIManager.GetInstance().LoadScene(Config.LobbyScene);
        }

        public void FixedUpdate()
        {
            //Debug.Log("-----------Time Start-----------");
            //Debug.Log("Time.time " + Time.time);
            //Debug.Log("Time.deltaTime " + Time.deltaTime);
            //Debug.Log("Time.fixedTime " + Time.fixedTime);
            //Debug.Log("Time.SmoothDeltaTime " + Time.smoothDeltaTime);
        }
    }
}
