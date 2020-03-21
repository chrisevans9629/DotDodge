using UnityEngine;

public class MoveableObject : MonoBehaviour, ISpeed
{
    public float Speed;
    public float SpeedValue { get => Speed; set => Speed = value; }

    public void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }
}