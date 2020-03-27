using UnityEngine;

namespace Assets.Scripts
{
    public class PowerupSpawner : SpawnerBase
    {
        public GameObject Powerup;
        public override void Start()
        {
            nextDifficulty = DifficultyEveryScore;
            StartCoroutine(Spawn(Powerup));
            base.Start();
        }
    }
}