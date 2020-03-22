using UnityEngine;

namespace Assets.Scripts
{
    public class AddPoints : MonoBehaviour
    {
        public float PointValue;
        public PlayerBase Player;
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                Player.Score += PointValue;
            }
        }
    }
}