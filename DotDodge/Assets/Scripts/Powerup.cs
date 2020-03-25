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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<Player2D>();
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
            HitSound?.PlayOneShot(HitSound.clip);

            var from = SpriteRenderer.color;

            var to = SpriteRenderer.color;
            to.a = 0;

            LeanTween.value(gameObject, color => SpriteRenderer.color = color, from, to, 1f);
            Destroy(gameObject, 1f);
        }
    }
}