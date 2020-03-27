using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public enum PowerupType
    {
        IncreaseFireRate,
        IncreaseBulletCount,
        AddShield,
    }

    public static class PowerupExt
    {
        public static Color GetColor(PowerupType type)
        {
            switch (type)
            {
                case PowerupType.IncreaseFireRate:
                    return Color.white;
                case PowerupType.AddShield:
                    return Color.green;
                case PowerupType.IncreaseBulletCount:
                    return Color.red;
                default:
                    return Color.white;
            }
        }


        public static PowerupType GetTypeFromRandom(float value)
        {
            if (value >= 0.5f)
            {
                return PowerupType.IncreaseFireRate;
            }

            if (value > 0.2f)
            {
                return PowerupType.AddShield;
            }

            return PowerupType.IncreaseBulletCount;
        }
    }

    public class Powerup : MoveableObject
    {
        public UnityEvent HitEvent;
        public AudioSource HitSound;
        public float PowerupValue;
        public SpriteRenderer SpriteRenderer;
        public float DeathDelay = 1;
        public PowerupType PowerupType;
        void Start()
        {
            PowerupType = PowerupExt.GetTypeFromRandom(Random.Range(0f,1f));
            SpriteRenderer.color = PowerupExt.GetColor(PowerupType);
            MoveUp();
            //LeanTween.value(this.gameObject, f => transform.position += new Vector3(0, f, 0), transform.position.y,
            //    transform.position.y + 1f, 2f);
        }

        void MoveUp()
        {
            LeanTween.moveY(gameObject, transform.position.y + 1, 2).setEaseInOutQuad().setOnComplete(MoveDown);
        }

        void MoveDown()
        {
            LeanTween.moveY(gameObject, transform.position.y - 1, 2).setEaseInOutQuad().setOnComplete(MoveUp);
        }

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
            if (!collision.gameObject.CompareTag("Player")) return;
            var player = collision.gameObject.GetComponent<PlayerBase>();
            if (player == null) return;
            switch (PowerupType)
            {
                case PowerupType.IncreaseFireRate:
                    player.FireRateSeconds *= PowerupValue;
                    break;
                case PowerupType.IncreaseBulletCount:
                    player.BulletCount++;
                    break;
                case PowerupType.AddShield:
                    player.AddHealth();
                    break;
            }
            Impact();
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