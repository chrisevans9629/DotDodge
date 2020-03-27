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
    public float DifficultyEveryScore = 5000;
    public float DifficultyIncrement = 1;
    public PlayerBase Player;

    protected float nextDifficulty;

    public virtual void Start()
    {
        initialMaxSpeed = MaxSpeed;
        initialMinSpeed = MinSpeed;
    }

    private float initialMaxSpeed;
    private float initialMinSpeed;
    public void ResetSpawner()
    {
        nextDifficulty = 0;
        MaxSpeed = initialMaxSpeed;
        MinSpeed = initialMinSpeed;
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
    protected IEnumerator Spawn(GameObject prefab)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinSeconds, MaxSeconds));

            SpawnObject(prefab);
        }

    }

    protected virtual GameObject SpawnObject(GameObject prefab)
    {
        var position = transform.position;
        position.y = Random.Range(MinY, MaxY);

        var result = Instantiate(prefab, position, Quaternion.identity, transform);
        var speed = result.GetComponent<ISpeed>();
        speed.SpeedValue = Random.Range(MinSpeed, MaxSpeed);
        ShouldIncreaseDifficulty();
        return result;
    }
}