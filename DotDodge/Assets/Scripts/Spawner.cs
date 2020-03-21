using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : SpawnerBase
{
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        nextDifficulty = DifficultyEveryScore;
        StartCoroutine(Spawn(EnemyPrefab));
    }

    protected override GameObject SpawnObject(GameObject prefab)
    {
        var result = base.SpawnObject(prefab);
        result.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);
        return result;
    }
}
