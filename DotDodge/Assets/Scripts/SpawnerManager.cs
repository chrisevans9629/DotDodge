using UnityEngine;

namespace Assets.Scripts
{
    //[System.Serializable]
    public class Spawn : MonoBehaviour
    {
        public float MinY = -5;
        public float MaxY = 5;
        public float MinSpeed = 1;
        public float MaxSpeed = 3;
        public float MinSeconds = 2;
        public float MaxSeconds = 5;
        public GameObject Item;
        public float SpeedIncreaseDelta = 1;
        public float IntroductionDelay;
    }
    public class SpawnerManager : MonoBehaviour
    {
        public Spawn[] Spawns;
        public float IncreaseSpeedEvery = 1000;
    }
}