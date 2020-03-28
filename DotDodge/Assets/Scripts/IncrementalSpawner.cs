using UnityEngine;

public class IncrementalSpawner : SpawnerBase
{
    [Tooltip("this will be added to the difficulty every score")]
    public float StartSpawningAt = 0;
    [Tooltip("This will increase values over time when the player reaches this score")]
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
        nextDifficulty = DifficultyEveryScore + StartSpawningAt;
        base.Start();
    }

    private void ShouldIncreaseDifficulty()
    {
        if (Player.Score > nextDifficulty)
        {
            IncreaseDifficulty();
        }
    }

    protected virtual void IncreaseDifficulty()
    {
        this.MaxSpeed += DifficultyIncrement;
        this.MinSpeed += DifficultyIncrement;
        nextDifficulty += DifficultyEveryScore;
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