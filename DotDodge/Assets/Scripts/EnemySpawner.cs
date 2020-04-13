using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;
//[RequireComponent(typeof(AddPoints))]
public class EnemySpawner : IncrementalSpawner
{

    //private AddPoints AddPoints;

    public override void Start()
    {
       // AddPoints = GetComponent<AddPoints>();
        initHoverAmount = HoverAmount;
        base.Start();
    }
    public Color Color = Color.white;

    public int Health = 0;

    public float HoverAmount = 0.5f;
    //public float HoverIncreaseDelta = 1;
    private float initHoverAmount;
    public override void ResetSpawner()
    {
        HoverAmount = initHoverAmount;
        base.ResetSpawner();
    }

    //protected override void IncreaseDifficulty()
    //{
    //    HoverAmount += HoverIncreaseDelta;
    //    base.IncreaseDifficulty();
    //}
    public int PointValue = 100;
    protected override GameObject SpawnObject(GameObject prefab)
    {
        var result = base.SpawnObject(prefab);
        if (result == null)
            return null;
        result.transform.rotation = Quaternion.AngleAxis(15, Vector3.forward);

        var enemy = (IEnemy)result.GetComponent(typeof(IEnemy)) ?? (IEnemy)result.GetComponentInChildren(typeof(IEnemy));
        //enemy.HitEvent.AddListener(() => AddPoints?.AddPointsToPlayer());
        enemy.Health = Health;
        enemy.Color = Color;
        var addpoints = result.GetComponent<AddPoints>();
        if (addpoints != null)
        {
            addpoints.PointValue = PointValue;
        }
        var hover = result.GetComponent<Hover>();
        if (hover != null)
        {
            hover.Amount = HoverAmount;
        }
        return result;
    }
}
