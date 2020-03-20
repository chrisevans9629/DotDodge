﻿using System.Collections;
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
        ParticleSystem.Play();
        if (rb != null)
            rb.isKinematic = false;
        if (rb2d != null)
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }
}
