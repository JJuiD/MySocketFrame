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
        public Vector2 offset;
        public Vector2 size;
        public bool isTrigger;
        [HideInInspector]
        public RectangleArea area;
        [HideInInspector]
        public List<BoxColliderBase> colliders;
        [HideInInspector]
        public PhysicalGlobalBase physical;
        [HideInInspector]
        public bool isCloseCollider;
        public void Init(Vector2 size, Vector2 offset)
        {
            this.offset = offset;
            this.size = size;
            area = new RectangleArea(size, offset);
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

        public float GetWidth()
        {
            return size.x * 0.4f;
        }

        public void DrawBGLine()
        {
#if UNITY_EDITOR
            area = new RectangleArea(size, offset);
            float angle = this.transform.rotation.z;
            //x0 = (x - offset.x) * cos(a) - (y - offset.y) * sin(a) + offset.x;
            //y0 = (x - offset.x) * sin(a) + (y - offset.y) * cos(a) + offset.y;
            Vector2 center = this.transform.position + new Vector3(offset.x, offset.y);
            Vector2 lef_top = new Vector3(center.x, center.y) + new Vector3(-size.x / 2, size.y / 2);
            Vector2 rig_bot = new Vector3(size.x / 2, -size.y / 2) + new Vector3(center.x, center.y);
            Gizmos.color = Color.green;
            //Debug.Log(GetRPos(lef_top, center, angle).x + "," + GetRPos(lef_top, center, angle).y);
            //Debug.Log((size.x/2) * Mathf.Cos(Deg2Rad(90)) - (size.y/2) * Mathf.Sin(Deg2Rad(90)));
            Gizmos.DrawLine(GetRPos(lef_top, center, angle), GetRPos(new Vector2(lef_top.x + size.x, lef_top.y), center, angle));
            Gizmos.DrawLine(GetRPos(lef_top, center, angle), GetRPos(new Vector2(lef_top.x, lef_top.y - size.y), center, angle));
            Gizmos.DrawLine(GetRPos(rig_bot, center, angle), GetRPos(new Vector2(rig_bot.x - size.x, rig_bot.y), center, angle));
            Gizmos.DrawLine(GetRPos(rig_bot, center, angle), GetRPos(new Vector2(rig_bot.x, rig_bot.y + size.y), center, angle));
#endif
        }

        public Vector2 GetRPos(Vector2 pos,Vector2 center, float a)
        {
            return new Vector3((pos.x - center.x) * Mathf.Cos(Deg2Rad(a)) - (pos.y - center.y) * Mathf.Sin(Deg2Rad(a)) + center.x,
                (pos.x - center.x) * Mathf.Sin(Deg2Rad(a)) + (pos.y - center.y) * Mathf.Cos(Deg2Rad(a)) + center.y);
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
