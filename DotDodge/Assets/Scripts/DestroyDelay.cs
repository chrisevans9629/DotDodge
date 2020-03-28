using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyDelay : MonoBehaviour
    {
        public float Delay;

        void Start()
        {
            Destroy(gameObject, Delay);
        }
    }
}