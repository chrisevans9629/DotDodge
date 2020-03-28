using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Color Color = Color.white;
    private Hover hover;

    public int Health;

    private List<SpriteRenderer> renders;
    // Start is called before the first frame update
    void Start()
    {
        hover = GetComponentInChildren<Hover>();
        rb = GetComponent<Rigidbody>();
        renders = GetComponentsInChildren<SpriteRenderer>().ToList();
        var current = GetComponent<SpriteRenderer>();
        if (current != null)
        {
            renders.Add(current);
        }

        //Health = Random.Range(0, 2);
        //if (Health == 1)
        //{
            renders.ForEach(p => p.color = Color);
        //}
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

        if (Health > 0)
        {
            Health--;
            return;
        }

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
