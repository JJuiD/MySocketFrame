using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Scripts.Logic._2D_Base
{
    // 自定义Vector3
    [Serializable]
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

    [CustomPropertyDrawer(typeof(_Vector3))]
    public class _Vector3Drawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
#if UNITY_EDITOR
            var x = property.FindPropertyRelative("x");
            var y = property.FindPropertyRelative("y");
            var z = property.FindPropertyRelative("z");
            float LabelWidth = EditorGUIUtility.labelWidth;
            var labelRect = new Rect(position.x, position.y, LabelWidth, position.height);
            var width = (position.width - 175) / 3 ;
            //Debug.Log(position.min.x);
            //Debug.Log(position.min.y);
            var xRect = new Rect(position.x + 160, position.y, width, position.height);
            var yRect = new Rect(position.x + 160 + width, position.y, width, position.height);
            var zRect = new Rect(position.x + 160 + 2*width, position.y, width, position.height);

            EditorGUIUtility.labelWidth = 12.0f;
            EditorGUI.LabelField(labelRect, label);
            EditorGUI.PropertyField(xRect, x);
            EditorGUI.PropertyField(yRect, y);
            EditorGUI.PropertyField(zRect, z);
            EditorGUIUtility.labelWidth = LabelWidth;
#endif
        }
    }

    //物理Base(个人)
    public abstract class PhysicalGlobalBase : MonoBehaviour
    {
        public _Vector3 velocity; //初速度
        public _Vector3 position; //实际坐标
        private void Start()
        {
            velocity = new _Vector3(0, 0, 0);
            position += this.transform.position;
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
