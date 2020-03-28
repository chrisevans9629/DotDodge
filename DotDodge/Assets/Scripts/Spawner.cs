using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class IncrementalSpawner : SpawnerBase
{
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
        var result = base.SpawnObject(prefab);
        ShouldIncreaseDifficulty();
        return result;
    }
}
public class Spawner : IncrementalSpawner
{

    public AddPoints AddPoints;
    // Start is called before the first frame update
    //public override void Start()
    //{
    //    nextDifficulty = DifficultyEveryScore;
    //    //StartCoroutine(Spawn(EnemyPrefab));
    //    base.Start();
    //}

    protected override GameObject SpawnObject(GameObject prefab)
    {
        var result = base.SpawnObject(prefab);
        result.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);

        var enemy = result.GetComponent<Enemy>() ?? result.GetComponentInChildren<Enemy>();
        if (enemy != null)
        {
            enemy.HitEvent.AddListener(() => AddPoints?.AddPointsToPlayer());
        }

        return result;
    }
}
