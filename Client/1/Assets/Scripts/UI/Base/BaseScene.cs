using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.UI
{
    public abstract class BaseScene
    {
        public string name { get; set; }

        public abstract void onEnter();

        public abstract void onExit();
    }
}
