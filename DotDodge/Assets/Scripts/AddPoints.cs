using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [Serializable]
    public class PointEvent : UnityEvent<int>
    {

    }
    public class AddPoints : MonoBehaviour
    {
        public int PointValue;
        public PlayerBase Player;
        public PointEvent GotPoints;

        public void AddPointsToPlayer()
         {
            Player.Score += PointValue;
            GotPoints.Invoke(PointValue);
        }
        //void OnTriggerEnter2D(Collider2D col)
        //{
        //    if (col.CompareTag("Enemy"))
        //    {
        //        Player.Score += PointValue;
        //        GotPoints.Invoke(PointValue);
        //    }
        //}
    }
}