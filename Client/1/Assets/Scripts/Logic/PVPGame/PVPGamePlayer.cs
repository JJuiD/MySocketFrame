using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Logic.PVPGame
{
    public class PlayerInfo
    {
        public string name;
        public Int16 seat;
        public Int16 localSeat;
        public PlayerState state;

        public PlayerInfo()
        {
            name = "";
            seat = 0;
            localSeat = 0;
            state = PlayerState.FREE;
        }
    }

    //enum ColliderType
    //{
    //    Tolerant,
    //    Disjoint,
    //    Overlap,
    //}

    public class BoxColliderPlayer
    {
        private Vector2 pos;
        private Vector2 topLefPos;
        private Vector2 topRigPos;
        private Vector2 btmLefPos;
        private Vector2 btmRigPos;

        private GameObject collider;
        private Texture texture;

        public Vector2 GetTopLefPos() { return topLefPos; }
        public Vector2 GetBtmRigPos() { return btmRigPos; }

        public BoxColliderPlayer(GameObject gameObject)
        {
            this.collider = gameObject;
            this.texture = collider.GetComponent<Image>().sprite.texture;
            pos = gameObject.transform.position;
            topLefPos = new Vector2(pos.x - texture.width / 2, pos.y + texture.height / 2);
            topRigPos = new Vector2(pos.x + texture.width / 2, pos.y + texture.height / 2);
            btmLefPos = new Vector2(pos.x - texture.width / 2, pos.y - texture.height / 2);
            btmRigPos = new Vector2(pos.x + texture.width / 2, pos.y - texture.height / 2);
            //gameObject.transform.GetComponent<Image>().sprite.texture.width;
            //topLefPos = new Vector2(pos.x - )
        }

        public bool IsCollider(GameObject gameObject)
        {
            Vector2 targetPos = gameObject.transform.position;
            Texture targetTexture = gameObject.GetComponent<Image>().sprite.texture;

            float dis_width = Math.Abs(targetPos.x - pos.x);
            float dis_height = Math.Abs(targetPos.y - pos.y);
            if (dis_width < targetTexture.width/2 + texture.width/2
                && dis_width < targetTexture.width / 2 + texture.width / 2)
            {
                return true;
            }

            return false;
            //float dis = (dis_width + dis_height) * (dis_width + dis_height) - 2 * dis_width * dis_height
        }
    }

    public class PVPGamePlayer : BasePlayerLogic
    {
        private PlayerInfo playerInfo;

        public void SetData(PlayerInfo playerinfo)
        {
            this.playerInfo.name = playerinfo.name;
            this.playerInfo.seat = playerinfo.seat;
            this.playerInfo.localSeat = playerinfo.localSeat;
            this.playerInfo.state = playerinfo.state;
        }

        public Int16 GetServerSeat() { return playerInfo.seat; }
        public Int16 GetLocalSeat() { return playerInfo.localSeat; }

    }
}
