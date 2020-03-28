using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;




public class Enemy : MoveableObject
{
    public ParticleSystem ParticleSystem;
    private Rigidbody rb;
    public Rigidbody2D rb2d;
    public UnityEvent HitEvent;
    public AudioSource HitSound;

    private Hover hover;
    // Start is called before the first frame update
    void Start()
    {
        hover = GetComponentInChildren<Hover>();
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
        if (HitSound != null)
            HitSound?.PlayOneShot(HitSound?.clip);
        ParticleSystem.Play();
        if (rb != null)
            rb.isKinematic = false;
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.angularVelocity = 90;
        }
        hover.StopHovering();
    }

}
