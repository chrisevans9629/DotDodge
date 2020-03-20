using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public ParticleSystem ParticleSystem;
    private Rigidbody rb;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ParticleSystem.Play();
            if (rb != null)
                rb.isKinematic = false;
            if (rb2d != null)
            {
                rb2d.isKinematic = false;
            }
            //Destroy(this.gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }
}
