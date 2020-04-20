using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    public bool ShouldKillEnemy = true;

    public bool ShouldUseUp = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ShouldKillEnemy && collision.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldKillEnemy && other.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldUseUp)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.up * Speed * Time.deltaTime;
        }
    }
}
