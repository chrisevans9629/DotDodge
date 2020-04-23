using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;


public interface IEnemy
{
    UnityEvent HitEvent { get; }
    int Health { get; set; }
    Color Color { get; set; }
}

[RequireComponent(typeof(AddPoints))]
public class Enemy : MoveableObject, IEnemy
{
    private AddPoints _addPoints;
    public ParticleSystem ParticleSystem;
    private Rigidbody rb;
    public Rigidbody2D rb2d;
    public UnityEvent HitEvent;
    public AudioSource HitSound;
    public Color Color = Color.white;
    private Hover hover;

    public int Health;

    private List<SpriteRenderer> renders;

    UnityEvent IEnemy.HitEvent => HitEvent;

    int IEnemy.Health { get => Health; set => Health = value; }
    Color IEnemy.Color { get => Color; set => Color = value; }

    // Start is called before the first frame update
    void Start()
    {
        _addPoints = GetComponent<AddPoints>();
        var s = SoundManager.SoundManagerInstance;
        s.Add(HitSound);
        hover = GetComponentInChildren<Hover>();
        rb = GetComponent<Rigidbody>();
        renders = GetComponentsInChildren<SpriteRenderer>().ToList();
        var current = GetComponent<SpriteRenderer>();
        if (current != null)
        {
            renders.Add(current);
        }
        renders.ForEach(p => p.color = Color);
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

    private bool isDead;
    private void Impact()
    {
        HitEvent.Invoke();
      

        if (Health > 0)
        {
            Health--;
            return;
        }
        if(isDead)
            return;
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
        _addPoints.AddPointsToPlayer();
        isDead = true;
    }

}
