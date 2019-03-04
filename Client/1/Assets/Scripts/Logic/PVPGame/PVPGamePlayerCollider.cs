using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Scripts.Logic.PVPGame
{
    public class PVPGamePlayerCollider : MonoBehaviour
    {
        private List<Collision2D> enterCollisions = new List<Collision2D>();
        private List<Collision2D> stayCollisions = new List<Collision2D>();
        private void OnCollisionEnter2D(Collision2D collision)
        {
            AddEnterCollision(collision);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            RemoveEnterCollision(collision);
            AddStayCollision(collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            RemoveEnterCollision(collision);
            RemoveStayCollision(collision);
        }

        
        public List<Collision2D> GetEnterCollisions()
        {
            return enterCollisions;
        }
        public void AddEnterCollision(Collision2D collision)
        {
            if (!enterCollisions.Contains(collision))
            {
                enterCollisions.Remove(collision);
            }
        }
        public void RemoveEnterCollision(Collision2D collision)
        {
            if (enterCollisions.Count == 0) return;
            if (enterCollisions.Contains(collision))
            {
                enterCollisions.Remove(collision);
            }
        }

        public List<Collision2D> GetStayCollisions()
        {
            return stayCollisions;
        }
        public void AddStayCollision(Collision2D collision)
        {
            if (!stayCollisions.Contains(collision))
            {
                stayCollisions.Remove(collision);
            }
        }
        public void RemoveStayCollision(Collision2D collision)
        {
            if (stayCollisions.Count == 0) return;
            if (stayCollisions.Contains(collision))
            {
                stayCollisions.Remove(collision);
            }
        }
    }
}
