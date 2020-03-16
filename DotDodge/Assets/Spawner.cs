using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float MaxY;
    public float MinY;
    public float MaxSeconds;
    public float MinSeconds;
    public float MinSpeed;
    public float MaxSpeed;
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
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
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
