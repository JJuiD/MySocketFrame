using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Logic._2D_Base
{
    // 自定义Vector3
    public class _Vector3
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
        public _Vector3() { }
        public _Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public _Vector3(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public _Vector3(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }
        public static _Vector3 operator + (_Vector3 _self, _Vector3 vector)
        {
            return new _Vector3(_self.x + vector.x
                , _self.y + vector.y, _self.z + vector.z);
        }
        public static _Vector3 operator + (_Vector3 _self, Vector3 vector)
        {
            return new _Vector3(_self.x + vector.x
                , _self.y + vector.y, _self.z + vector.z);
        }
        public static _Vector3 operator * (_Vector3 _self,float num)
        {
            return new _Vector3(_self.x * num
                , _self.y * num, _self.z * num);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(this.x 
                , this.y,this.z );
        }
        public bool IsEuqal(Vector3 vector)
        {
            return (((int)(this.x * 100) == (int)(vector.x * 100)) && ((int)(this.y * 100) == (int)(vector.y * 100))
                && ((int)(this.z * 100) == (int)(vector.z * 100)));
        }
        public void Log()
        {
            Debug.Log(string.Format("_Vector3({0},{1},{2})", this.x, this.y, this.z));
        }
    }

    

    //物理Base(个人)
    public abstract class PhysicalGlobalBase : MonoBehaviour
    {
        public _Vector3 velocity; //初速度
        public _Vector3 position; //实际坐标
        public PhysicalGlobalBase()
        {
            velocity = new _Vector3(0, 0, 0);
            position = new _Vector3(0, 0, 0);
        }
        public void Init(Vector3 pos)
        {
            position = new _Vector3(pos);
            _2DPhysicalEngineWorld.GetInstance().AddPhysicalGlobal(this);
        }
        //移动
        public void SetVelocity(Vector2 vector)
        {
            velocity.x = vector.x;
            velocity.y = vector.y;
        }
        //跳跃
        public void AddVelocity(_Vector3 vector)
        {
            this.velocity += vector;
        }
        public abstract void FixedUpdateGlobal();
    }

    //物理(世界)
    public class _2DPhysicalEngineWorld : Singleton<_2DPhysicalEngineWorld>
    {
        public float gravity = 10f; //重力 10m/s
        private List<PhysicalGlobalBase> list_objects = new List<PhysicalGlobalBase>();
        public void AddPhysicalGlobal(PhysicalGlobalBase globalBase)
        {
            if (globalBase == null) return;
            if (list_objects.Contains(globalBase)) return;
            list_objects.Add(globalBase);
        }
        public void RemovePhysicalGlobal(PhysicalGlobalBase globalBase)
        {
            if (globalBase == null) return;
            if (!list_objects.Contains(globalBase)) return;
            list_objects.Remove(globalBase);
        }

        public void FixedUpdate()
        {
            if (list_objects.Count == 0) return;
            foreach(var temp in list_objects)
            {
                temp.FixedUpdateGlobal();
            }
            _2DColliderEngineWorld.GetInstance().FixedUpdate();
        }
    }
}
