using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Scripts.Logic._2D_Base
{
    public class RectangleArea
    {
        Vector2 offset; //中心点
        Vector2 size;
        public RectangleArea(Vector2 size, Vector2 offset)
        {
            this.size = size;
            this.offset = offset;
        }
        /// <summary>
        /// 获取顶点
        /// </summary>
        /// <param name="isLeft"></param>
        /// <param name="isTop"></param>
        /// <returns></returns>
        public Vector2 GetVertex(bool isLeft,bool isTop)
        {
            float pos_x = offset.x + (isLeft ? -size.x : size.x);
            float pos_y = offset.y + (isTop ? size.y : -size.y);
            return new Vector2(pos_x, pos_y);
        }
        public Vector2 GetOffset() { return offset; }
        public Vector2 GetSize() { return size; }
        public float GetArea() { return size.x * size.y; }

    }

    [ExecuteInEditMode]
    public abstract class BoxColliderBase : MonoBehaviour
    {
        public _Vector3 offset;
        public _Vector3 size; 
        public bool isTrigger;
        [HideInInspector]
        public RectangleArea area;
        [HideInInspector]
        public List<BoxColliderBase> colliders;
        [HideInInspector]
        public PhysicalGlobalBase physical; //判断实际坐标用
        [HideInInspector]
        public bool isCloseCollider;
        private void Start()
        {
            //area = new RectangleArea(size, offset);
            physical = this.GetComponent<PhysicalGlobalBase>();
            colliders = new List<BoxColliderBase>();
            isCloseCollider = false;
            _2DColliderEngineWorld.GetInstance().AddColliderGlobal(this);
        }
        public void InsertCollider(BoxColliderBase collider)
        {
            if (collider.isCloseCollider || this.isCloseCollider) return;
            if(colliders.Contains(collider))
            {
                OnColliderStay(collider);
            }
            else
            {
                OnColliderEnter(collider);
                colliders.Add(collider);
            }
        }
        public void RemoveCollider(BoxColliderBase collider)
        {
            if (collider.isCloseCollider || this.isCloseCollider) return;
            if (colliders.Contains(collider))
            {
                colliders.Remove(collider);
                OnColliderExit(collider);
            }
        }
        /// <summary>
        /// 是否关闭碰撞判定
        /// </summary>
        /// <param name="bClose"></param>
        public void UpdateType(bool bClose)
        {
            isCloseCollider = bClose;
            if (isCloseCollider) { colliders.Clear(); }
        }
        public abstract void OnColliderEnter(BoxColliderBase collider);
        public abstract void OnColliderStay(BoxColliderBase collider);
        public abstract void OnColliderExit(BoxColliderBase collider);

        #region 预设,绘制
        void OnDrawGizmos()
        {
            DrawBGLine();
        }

        public float Get45Y(float value)
        {
            return value * Mathf.Cos(Deg2Rad(45));
        }

        public void DrawBGLine()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;
            Vector3 bot_lef = new Vector3(this.transform.position.x + offset.x - size.x / 2
                , this.transform.position.y + offset.y - size.z / 2);
            Vector3 top_rig = new Vector3(this.transform.position.x + offset.x + size.x / 2
                , this.transform.position.y + offset.y + size.z / 2);
            Gizmos.DrawLine(bot_lef, bot_lef);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(bot_lef, bot_lef + new Vector3(size.x,0));
            Gizmos.DrawLine(bot_lef, bot_lef + new Vector3(0, size.z));
            Gizmos.DrawLine(top_rig, top_rig + new Vector3(0, -size.z));
            Gizmos.DrawLine(top_rig, top_rig + new Vector3(-size.x, 0));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(bot_lef, bot_lef + new Vector3(Get45Y(size.y),0, -Get45Y(size.y)));
            Gizmos.DrawLine(bot_lef, bot_lef + new Vector3(-Get45Y(size.y),0, Get45Y(size.y)));
#endif
        }
        public float Deg2Rad(float rad)
        {
            return Mathf.Deg2Rad * rad ;
        }
        #endregion

    }

    
    public class _2DColliderEngineWorld : Singleton<_2DColliderEngineWorld>
    {
        private RectangleArea area;
        private List<BoxColliderBase> triggerList = new List<BoxColliderBase>();
        private List<BoxColliderBase> colliderList = new List<BoxColliderBase>();
        public void AddColliderGlobal(BoxColliderBase globalBase)
        {
            if(globalBase.isTrigger)
            {
                triggerList.Add(globalBase);
            }
            else
            {
                colliderList.Add(globalBase);
            }
        }
        public void RemoveColliderGlobal(BoxColliderBase globalBase)
        {
            if (globalBase == null) return;
            if (globalBase.isTrigger)
            {
                if (!triggerList.Contains(globalBase)) return;
                triggerList.Remove(globalBase);
            }
            else
            {
                if (!colliderList.Contains(globalBase)) return;
                colliderList.Remove(globalBase);
            }
        }
        public void SetValidColliderArea(Vector2 size,Vector2 offset,int splitCount = 1)
        {
            area = new RectangleArea(size, offset);
        }

        public bool IsCollider(BoxColliderBase collider1, BoxColliderBase collider2)
        {
            _Vector3 pos1 = collider1.physical.position;
            _Vector3 pos2 = collider2.physical.position;
            if (Mathf.Abs(pos1.z - pos2.z) > (collider1.size.z + collider2.size.z)
                || Mathf.Abs(pos1.x - pos2.x) > (collider1.size.x + collider2.size.x)
                || Mathf.Abs(pos1.y - pos2.y) > (collider1.size.y + collider2.size.y))
                return false;

            return true;
        }

        public void FixedUpdate()
        {
            if (triggerList.Count <= 1) return;
            for(int i = 0; i < triggerList.Count-1;++i)
            {
                for(int j = i+1; j < triggerList.Count;++j)
                {
                    if (IsCollider(triggerList[i], triggerList[j]))
                    {
                        triggerList[i].InsertCollider(triggerList[j]);
                        triggerList[j].InsertCollider(triggerList[i]);
                    }
                    else
                    {
                        triggerList[i].RemoveCollider(triggerList[j]);
                        triggerList[j].RemoveCollider(triggerList[i]);
                    }
                }
            }
            _2DAIEngineWorld.GetInstance().FixedUpdate();
        }
    }
}
