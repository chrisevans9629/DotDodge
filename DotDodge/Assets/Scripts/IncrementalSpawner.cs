using UnityEngine;

public class IncrementalSpawner : SpawnerBase
{
    public float StartSpawningAt = 0;
    public float StopSpawningAt = float.MaxValue;

    public int MaxCount = int.MaxValue;
    int _count = 0;
    PlayerBase Player;

    public override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
        base.Start();
    }

    public override void ResetSpawner()
    {
        _count = 0;
        base.ResetSpawner();
    }


    protected override GameObject SpawnObject(GameObject prefab)
    {
        if (Player.Score > StartSpawningAt && Player.Score <= StopSpawningAt && _count < MaxCount)
        {
            var result = base.SpawnObject(prefab);
            _count++;
            //ShouldIncreaseDifficulty();
            return result;
        }
        return null;
    }
}