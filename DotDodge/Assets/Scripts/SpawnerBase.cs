using System.Collections;
using UnityEngine;

public class SpawnerBase : MonoBehaviour
{
    public float MaxY;
    public float MinY;
    public float MaxSeconds;
    public float MinSeconds;
    public float MinSpeed;
    public float MaxSpeed;
    public GameObject ObjectToSpawn;


    public virtual void Start()
    {
        initialMaxSpeed = MaxSpeed;
        initialMinSpeed = MinSpeed;
        StartCoroutine(Spawn());
    }

    private float initialMaxSpeed;
    private float initialMinSpeed;
    public virtual void ResetSpawner()
    {
        MaxSpeed = initialMaxSpeed;
        MinSpeed = initialMinSpeed;
    }


    protected IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinSeconds, MaxSeconds));

            SpawnObject(ObjectToSpawn);
        }

    }

    protected virtual GameObject SpawnObject(GameObject prefab)
    {
        var position = transform.position;
        position.y = Random.Range(MinY, MaxY);

        var result = Instantiate(prefab, position, Quaternion.identity, transform);
        var speed = result.GetComponent<ISpeed>();
        if (speed != null)
            speed.SpeedValue = Random.Range(MinSpeed, MaxSpeed);
        return result;
    }
}