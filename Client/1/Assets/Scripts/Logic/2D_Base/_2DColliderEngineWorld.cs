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
        public RectangleArea area;
        [HideInInspector]
        public PhysicalGlobalBase physical;
        public bool isTrigger;
        public void Init(Vector2 size, Vector2 offset)
        {
            this.offset = offset;
            this.size = size;
            physical = this.GetComponent<PhysicalGlobalBase>();
            _2DColliderEngineWorld.GetInstance().AddColliderGlobal(this);
        }

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
            Vector2 lef_top = this.transform.position + new Vector3(offset.x, offset.y)
                + new Vector3(-size.x / 2, size.y / 2);
            Vector2 rig_bot = this.transform.position + new Vector3(offset.x, offset.y)
                + new Vector3(size.x / 2, -size.y / 2);
            Vector2 center = new Vector2(this.transform.position.x + offset.x, this.transform.position.y + offset.y);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(GetRPos(lef_top, center, angle), GetRPos(new Vector2(lef_top.x + size.x, lef_top.y), center, angle));
            Gizmos.DrawLine(GetRPos(lef_top, center, angle), GetRPos(new Vector2(lef_top.x, lef_top.y - size.y), center, angle));
            Gizmos.DrawLine(GetRPos(rig_bot, center, angle), GetRPos(new Vector2(rig_bot.x - size.x, rig_bot.y), center, angle));
            Gizmos.DrawLine(GetRPos(rig_bot, center, angle), GetRPos(new Vector2(rig_bot.x, rig_bot.y + size.y), center, angle));
#endif
        }

        public Vector2 GetRPos(Vector2 pos,Vector2 center, float a)
        {
            return new Vector3((pos.x - center.x) * Mathf.Cos(a) - (pos.y - center.y) * Mathf.Sin(a) + center.x,
                (pos.x - center.x) * Mathf.Sin(a) + (pos.y - center.y) * Mathf.Cos(a) + center.y);
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
            if (triggerList.Count == 0) return;
            for(int i = 0; i < triggerList.Count-1;++i)
            {

            }
        }
    }
}
