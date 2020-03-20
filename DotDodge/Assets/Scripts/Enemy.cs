using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public ParticleSystem ParticleSystem;
    [HideInInspector]
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ParticleSystem.Play();
            rb.isKinematic = false;
            //Destroy(this.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }
}
