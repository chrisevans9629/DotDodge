using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupSpawner : SpawnerBase
    {
        public GameObject Powerup;
        void Start()
        {
            nextDifficulty = DifficultyEveryScore;
            StartCoroutine(Spawn(Powerup));
        }
    }
}