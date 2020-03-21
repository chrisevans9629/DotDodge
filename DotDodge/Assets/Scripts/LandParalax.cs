using UnityEngine;

namespace Assets.Scripts
{
    public class LandParalax : MonoBehaviour
    {
        public GameObject Current;
        public GameObject Coming;

        public float Speed;

        private Vector3 currentPosition;
        private Vector3 comingPosition;

        void Start()
        {
            currentPosition = Current.transform.position;
            comingPosition = Coming.transform.position;
        }

        void Update()
        {
            Current.transform.position += Vector3.left * Speed * Time.deltaTime;
            Coming.transform.position += Vector3.left * Speed * Time.deltaTime;

            if (Coming.transform.position.x < currentPosition.x)
            {
                var current = Current;
                Current = Coming;
                Coming = current;
                Coming.transform.position = comingPosition;
            }
        }
    }
}