using Scripts.TwoDimensiona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic.GP
{
    // 10 : 7 (x : y)
    public class GPPhysicalGlobal : PhysicalGlobalBase
    {
        public override void FixedUpdateGlobal()
        {
            if (this.velocity.IsEuqal(Vector3.zero)) return;
            this.position.x += this.velocity.x;
            this.position.y += this.velocity.y;
            // Z轴独立计算
            if (this.velocity.z > 0 || this.position.z > 0)
            {
                float v_s = this.velocity.z;
                float v_e = (this.velocity.z -= PhysicalEngineWord.GetInstance().gravity * Time.fixedDeltaTime);
                Debug.Log(v_e);
                float pos_z = this.position.z +
                    (v_e * v_e - v_s * v_s) / (-2 * PhysicalEngineWord.GetInstance().gravity);
                if (pos_z < 0)
                {
                    pos_z = 0;
                    this.velocity.z = 0;
                }
                Debug.Log(string.Format("[{0}] pos_z : {1}", this.gameObject.name, pos_z));
                this.position.z = pos_z;
            }
            SwitchToViewPos();
        }

        private void SwitchToViewPos()
        {
            this.transform.position = new Vector3(
                this.position.x, this.position.y + this.position.z, 0);
        }
    }
}
