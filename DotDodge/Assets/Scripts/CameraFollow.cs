using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera Camera;
    public float Speed;
    public Transform Follow;
    public bool IsFollowingActive = true;
    public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        SetupOffset();
    }

    public void SetupOffset()
    {
        if (Follow != null)
        {
            Offset = Camera.transform.position - Follow.position;
        }
    }
    private Vector3 velocity;
    // Update is called once per frame
    void Update()
    {
        if (Follow != null && IsFollowingActive)
            Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, Follow.position + Offset, ref velocity, Speed);
    }
}
