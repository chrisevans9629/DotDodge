using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float MaxY;
    public float MinY;
    public float MaxSeconds;
    public float MinSeconds;
    public float MinSpeed;
    public float MaxSpeed;
    public GameObject EnemyPrefab;
    public float DifficultyEveryScore = 5000;
    public float DifficultyIncrement = 1;
    public PlayerBase Player;
    // Start is called before the first frame update
    void Start()
    {
        nextDifficulty = DifficultyEveryScore;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinSeconds, MaxSeconds));

            var position = transform.position;
            position.y = Random.Range(MinY, MaxY);

            var result = Instantiate(EnemyPrefab, position, Quaternion.identity, transform);
            result.GetComponent<Enemy>().Speed = Random.Range(MinSpeed, MaxSpeed);
            ShouldIncreaseDifficulty();
        }

    }

    private float nextDifficulty;
    void ShouldIncreaseDifficulty()
    {
        if (Player.Score > nextDifficulty)
        {
            this.MaxSpeed += DifficultyIncrement;
            this.MinSpeed += DifficultyIncrement;
            nextDifficulty += DifficultyEveryScore;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
