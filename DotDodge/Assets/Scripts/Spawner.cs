using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

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
        if (result == null)
            return null;
        result.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);

        var enemy = result.GetComponent<Enemy>() ?? result.GetComponentInChildren<Enemy>();
        if (enemy != null)
        {
            enemy.HitEvent.AddListener(() => AddPoints?.AddPointsToPlayer());
        }

        return result;
    }
}
