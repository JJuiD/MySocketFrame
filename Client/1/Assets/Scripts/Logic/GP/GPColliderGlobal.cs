using Scripts.Logic._2D_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Scripts.Logic.GP
{
    public class GPColliderGlobal : BoxColliderBase
    {
        public override void OnColliderEnter(BoxColliderBase collider)
        {
            Debug.Log("OnColliderEnter : " + collider.transform.name);
        }

        public override void OnColliderExit(BoxColliderBase collider)
        {
            
        }

        public override void OnColliderStay(BoxColliderBase collider)
        {
            
        }
    }

    
}
