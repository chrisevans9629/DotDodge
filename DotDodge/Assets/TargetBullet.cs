using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour, ISpeed
{
    private GameObject Player;

    public float _speedValue;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        float angle = 0;

        Vector3 relative = transform.InverseTransformPoint(Player.transform.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * SpeedValue * Time.deltaTime;
    }

    public float SpeedValue
    {
        get => _speedValue;
        set => _speedValue = value;
    }
}
