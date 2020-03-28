using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class DistanceNotifyer : MonoBehaviour
    {
        public float Distance;
        public UnityEvent WithinDistance;
        private GameObject player;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public bool HasNotified = false;
        void Update()
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) < Distance && !HasNotified)
            {
                WithinDistance.Invoke();
                HasNotified = true;
            }
        }
    }
}