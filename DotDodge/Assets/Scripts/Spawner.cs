using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : SpawnerBase
{
    public GameObject EnemyPrefab;

    public AddPoints AddPoints;
    // Start is called before the first frame update
    public override void Start()
    {
        nextDifficulty = DifficultyEveryScore;
        StartCoroutine(Spawn(EnemyPrefab));
        base.Start();
    }

    protected override GameObject SpawnObject(GameObject prefab)
    {
        var result = base.SpawnObject(prefab);
        result.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);

        var enemy = result.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.HitEvent.AddListener(() => AddPoints?.AddPointsToPlayer());
        }

        return result;
    }
}
