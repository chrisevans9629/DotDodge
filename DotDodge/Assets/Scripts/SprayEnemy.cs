using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class SprayEnemy : MonoBehaviour, IEnemy
{
    public ParticleSystem ParticleSystem;

    Rigidbody2D rb2d;
    public AudioSource HitSound;
    private Hover hover;
    private List<SpriteRenderer> renders;

    public UnityEvent _HitEvent;
    public UnityEvent HitEvent => _HitEvent;
    public int _health = 4;
    public int Health { get => _health; set => _health = value; }
    public Color Color { get; set; } = Color.white;
    public Bullet BulletPrefab;
    public GameObject BulletPosition;
    public float ShootingSeconds;
    public float Speed;
    public Rect MoveArea;
    public float FireRate;
    public float AngleIncrement;
    private float angle = 0;
    private bool isDead;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hover = GetComponentInChildren<Hover>();
        renders = GetComponentsInChildren<SpriteRenderer>().ToList();
        var current = GetComponent<SpriteRenderer>();
        if (current != null)
        {
            renders.Add(current);
        }
        renders.ForEach(p => p.color = Color);


        MoveToArea();
    }

    void MoveToArea()
    {
        if (isDead)
            return;
        var pos = new Vector3(Random.Range(MoveArea.xMin, MoveArea.xMax), Random.Range(MoveArea.yMin, MoveArea.yMax));

        movement = LeanTween.move(gameObject, pos, Speed).setEaseInOutExpo().setOnComplete(Shoot);
    }

    private LTDescr movement;
    private bool isShooting = false;
    private void Shoot()
    {
        isShooting = true;
        StartCoroutine(Fire());
        StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(ShootingSeconds);
        isShooting = false;
        MoveToArea();
    }

    private IEnumerator Fire()
    {
        while (isShooting && !isDead)
        {
            yield return new WaitForSeconds(FireRate);
            if (isDead)
                break;
            var result = Instantiate(BulletPrefab, BulletPosition.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            angle += AngleIncrement;
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

        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.angularVelocity = 90;
        }
        if (hover != null)
            hover.StopHovering();
        isDead = true;
        if (movement != null)
        {
            LeanTween.cancel(movement.uniqueId);
        }
    }
}