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

    

    
}
