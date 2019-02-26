using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
     
    public abstract class BasePlayerLogic
    {
        
    }

    public abstract class BaseLogic
    {
        public BaseLogic() { AddDataListener(); }
        ~BaseLogic() { RemoveDataListener(); }
        public virtual void Init() { }
        public virtual void AddPlayer(BasePlayerLogic player) { }
        public abstract void AddDataListener();
        public abstract void RemoveDataListener();
        public abstract void LogicFixedUpdate();

        public Dictionary<string, KeyCode> EventToActionDic = new Dictionary<string, KeyCode>();
        public Dictionary<KeyCode, string> KeyToEventDic = new Dictionary<KeyCode, string>();
        public abstract void LoadKeyEventDic(UserDefault userDefault);
    }
}
