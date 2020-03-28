using UnityEngine;

public class IncrementalSpawner : SpawnerBase
{
    public float StartSpawningAt = 0;
    public float DifficultyEveryScore = 5000;
    public float DifficultyIncrement = 1;
    public PlayerBase Player;
    private float nextDifficulty;
    public override void ResetSpawner()
    {
        nextDifficulty = 0;
        base.ResetSpawner();
    }

    public override void Start()
    {
        nextDifficulty = DifficultyEveryScore;
        base.Start();
    }

    protected void ShouldIncreaseDifficulty()
    {
        if (Player.Score > nextDifficulty)
        {
            this.MaxSpeed += DifficultyIncrement;
            this.MinSpeed += DifficultyIncrement;
            nextDifficulty += DifficultyEveryScore;
        }
    }

    protected override GameObject SpawnObject(GameObject prefab)
    {
        if (Player.Score > StartSpawningAt)
        {
            var result = base.SpawnObject(prefab);
            ShouldIncreaseDifficulty();
            return result;
        }
        return null;
    }
}