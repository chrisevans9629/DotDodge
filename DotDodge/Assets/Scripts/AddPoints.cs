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
        public int PointValue = 100;
        PlayerBase Player;
        private ShowPoints showPoints;
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
            showPoints = GameObject.FindObjectOfType<ShowPoints>();
        }
        public void AddPointsToPlayer()
        {
            Player.Score += PointValue;
            showPoints.OnPointScored(PointValue);
            //GotPoints.Invoke(PointValue);
        }
    }
}