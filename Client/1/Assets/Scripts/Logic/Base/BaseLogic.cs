using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic
{
     
    public abstract class BasePlayer : MonoBehaviour
    {
        protected PlayerInfo playerInfo = new PlayerInfo();
        public void SetServerPlayerData(PlayerInfo playerinfo)
        {
            this.playerInfo.name = playerinfo.name;
            this.playerInfo.seat = playerinfo.seat;
            this.playerInfo.localSeat = playerinfo.localSeat;
            this.playerInfo.SetPlayerState(PlayerGameState.FREE);
        }
        public Int16 GetServerSeat() { return playerInfo.seat; }
        public Int16 GetLocalSeat() { return playerInfo.localSeat; }
    }

    public abstract class BaseLogic
    {
        public BaseLogic() { AddDataListener(); }
        ~BaseLogic() { RemoveDataListener(); }
        public virtual void Init() { }

        public abstract void AddDataListener();
        public abstract void RemoveDataListener();

        public abstract void LogicFixedUpdate();

        public abstract void JoinGame();

        public abstract void InitMapData(int mapindex);

        public Dictionary<string, KeyCode> EventToActionDic = new Dictionary<string, KeyCode>();
        public Dictionary<KeyCode, string> KeyToEventDic = new Dictionary<KeyCode, string>();
        public abstract void LoadKeyEventDic(UserDefault userDefault);
    }
}
