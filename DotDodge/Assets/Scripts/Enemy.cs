using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MoveableObject
{
    public ParticleSystem ParticleSystem;
    private Rigidbody rb;
    public Rigidbody2D rb2d;
    public UnityEvent HitEvent;
    public AudioSource HitSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Impact();
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Impact();
        }
    }

    private void Impact()
    {
        HitEvent.Invoke();
        HitSound?.PlayOneShot(HitSound.clip);
        ParticleSystem.Play();
        if (rb != null)
            rb.isKinematic = false;
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.angularVelocity = 90;
        }
    }

}
