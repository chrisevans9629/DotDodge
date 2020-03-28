using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TreeParalax : SpawnerBase
{
    public GameObject Tree;
    public float MaxSize;
    public float MinSize;
    public float SpeedScaleRatio = 0.8f;
    // Start is called before the first frame update
    //public override void Start()
    //{
    //    StartCoroutine(Spawn(Tree));
    //    base.Start();
    //}

    protected override GameObject SpawnObject(GameObject prefab)
    {
        var result = base.SpawnObject(prefab);
        if (result == null)
            return null;
        var speed = result.GetComponent<ISpeed>();
        result.transform.localScale = Vector3.one * Random.Range(MinSize,MaxSize);

        speed.SpeedValue = result.transform.localScale.x * SpeedScaleRatio;

        return result;
    }
}
