using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public abstract void onEnter();

        public virtual void onExit()
        {
            Destroy(base.gameObject);
        }

        //public abstract void onPause();

        //public abstract void onResume();
    }
}
