using UnityEngine;
using Scripts.UI;

namespace Scripts
{
    //public abstract class Person
    //{
    //    public string name;
    //    public Person(string n)
    //    {
    //        name = n;
    //    }
    //}

    //public class Student : Person
    //{
    //   public Student(string n):base(n)
    //    {

    //    }
    //}

    //public class Teacher : Person
    //{
    //    public Teacher(string n) : base(n)
    //    {

    //    }
    //}
    public enum KeyNum : byte
    {
        Skill1 = 2,//a 0b01
        Skill2 = 4,//d 0b10
        Skill3 = 8, //w  0b
        Down = 16,//s
        MoveKey = 32,
        Attack = 64,
        k1 = 128,

    }
    public class main: MonoBehaviour
    {
        private void Awake()
        {
           // 
        }

        private void Start()
        {
            //KeyNum num1 = KeyNum.Skill1;
            //KeyNum num2 = KeyNum.Skill2;
            //Debug.Log(num1 & num2);
            //Debug.Log(num1 & ~num2);
            //Debug.Log(num1 & ~num1);
            //Debug.Log( ~num1);
            //Debug.Log(num1 | num2);
            Debug.Log(Mathf.Cos(Mathf.Deg2Rad * 45 ));
            Debug.Log(Mathf.Sin(Mathf.Deg2Rad * 45 ));
            DataCenter.GetInstance().InitData();
            UIManager.GetInstance().LoadScene(Config.Lobby,true);
            
        }

        //public void FixedUpdate()
        //{
        //    Debug.Log("-----------Time Start-----------");
        //    Debug.Log("Time.time " + Time.time);
        //    Debug.Log("Time.deltaTime " + Time.deltaTime);
        //    Debug.Log("Time.fixedTime " + Time.fixedTime);
        //    Debug.Log("Time.SmoothDeltaTime " + Time.smoothDeltaTime);
        //}
    }
}
