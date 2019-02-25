
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Scripts.Logic.PVPGame
{
    public enum PlayerState
    {
        FREE,
        READY,
        INGAME,
    }

    public class PVPGameLogic : BaseLogic
    {
        private Dictionary<Int16, PVPGamePlayer> playerList = new Dictionary<Int16, PVPGamePlayer>();
        private int maxPlayer = 4;

        public override void Init()
        {
            
        }

        public override void AddPlayer(BasePlayerLogic player)
        {
            PVPGamePlayer itemPlayer = (PVPGamePlayer)player;
            if (playerList.Count == 0 )
            {
                playerList.Add(itemPlayer.GetServerSeat(), itemPlayer);
                return;
            }

            foreach(var temp in playerList)
            {
                if(temp.Value.GetServerSeat() == itemPlayer.GetServerSeat())
                {
                    Debug.WriteLine("[ERROR] 存在同样的用户");
                    return;
                }
            }
        }
        public PVPGamePlayer GetPlayerBySeat(Int16 seat)
        {
            if (playerList.Count == 0) { return null; }
            if (playerList.ContainsKey(seat)) return playerList[seat];
            Debug.WriteLine("[ERROR] GetPlayerBySeat : " + seat.ToString() + " 不存在");
            return null;
        }
        public PVPGamePlayer GetPlayerByLocalSeat(Int16 localseat)
        {
            if (playerList.Count == 0) { return null; }
            foreach (var temp in playerList)
            {
                if (temp.Value.GetLocalSeat() == localseat)
                {
                    return temp.Value;
                }
            }

            return null;
        }
    }
}
