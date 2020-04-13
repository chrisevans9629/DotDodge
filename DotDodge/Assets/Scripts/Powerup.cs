﻿using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            AddFireRate(collision.gameObject);
        }
        private void OnTriggerEnter(Collider collision)
        {
            AddFireRate(collision.gameObject);
        }

        private bool hasBeenUsed = false;
        private void AddFireRate(GameObject collision)
        {
            if(hasBeenUsed)
                return;
            if (!collision.gameObject.CompareTag("Player")) return;
            var player = collision.gameObject.GetComponent<PlayerBase>();
            if (player == null) return;
            switch (PowerupType)
            {
                case PowerupType.IncreaseFireRate:
                    player.FireRateSeconds *= PowerupValue;
                    break;
                case PowerupType.IncreaseBulletCount:
                    player.AddBullet();
                    break;
                case PowerupType.AddShield:
                    player.AddHealth();
                    break;
            }

            hasBeenUsed = true;
            Impact();
        }

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