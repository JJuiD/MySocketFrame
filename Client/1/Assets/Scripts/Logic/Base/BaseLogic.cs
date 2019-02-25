using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scripts.Logic
{
    public abstract class BasePlayerLogic
    {
        
    }

    public abstract class BaseLogic
    {
        public virtual void Init() { }
        public virtual void AddPlayer(BasePlayerLogic player) { } 
    }
}
