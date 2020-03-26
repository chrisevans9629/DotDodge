using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Powerup : MoveableObject
    {
        public UnityEvent HitEvent;
        public AudioSource HitSound;
        public float PowerupValue;
        public SpriteRenderer SpriteRenderer;
        public float DeathDelay = 1;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            AddFireRate(collision.gameObject);
        }
        private void OnTriggerEnter(Collider collision)
        {
            AddFireRate(collision.gameObject);
        }

        private void AddFireRate(GameObject collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<PlayerBase>();
                if (player != null)
                {
                    player.FireRateSeconds *= PowerupValue;
                    Impact();
                }
            }
        }
        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Player"))
        //    {
        //        Impact();
        //    }
        //}

        private void Impact()
        {
            HitEvent.Invoke();
            if (HitSound != null)
                HitSound?.PlayOneShot(HitSound.clip);
            if (SpriteRenderer != null)
            {
                var from = SpriteRenderer.color;
                var to = SpriteRenderer.color;
                to.a = 0;
                LeanTween.value(gameObject, color => SpriteRenderer.color = color, from, to, DeathDelay);
            }
            Destroy(gameObject, DeathDelay);
        }
    }
}