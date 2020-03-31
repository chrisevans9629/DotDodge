﻿using System;
using System.Net.Configuration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public ShopSystem shopSystem;
        private LevelSystem levelSystem;
        public Image levelfill;
        public Text text;
        private PlayerBase playerBase;
        public Text PointsAvailableText;
        
        public int PointsAvailable => levelSystem.Level - shopSystem.LevelsUsed;
        public bool TestSystem = true;
        void Awake()
        {
            if (TestSystem)
            {
                levelSystem = new LevelSystem();
                shopSystem = new ShopSystem();
            }
            else
            {
                levelSystem = LevelSystem.Load();
                shopSystem = ShopSystem.Load();
            }
            
        }

        void OnGUI()
        {
            UpdateText();
           // UpdateUi();
        }
        void Start()
        {
            playerBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
            levelSystem.LevelChanged += LevelSystemOnLevelChanged;
            UpdateUi();
            playerBase.ScoreIncremented += PlayerBaseOnScoreIncremented;
            SetupPlayerLevel();
            UpdateText();
        }

        public void ResetProgress()
        {
            levelSystem = new LevelSystem();
            levelSystem.Save();
            shopSystem = new ShopSystem();
            shopSystem.Save();
            UpdateUi();
            UpdateText();
        }
        public void SetupPlayerLevel()
        {
            playerBase.BulletCount = shopSystem.BulletCount;
            playerBase.BulletSpeed = shopSystem.BulletSpeed;
            playerBase.FireRateSeconds = shopSystem.BulletRate;
            playerBase.MaxBullets = shopSystem.MaxBullets;
        }

        public void UpdateText()
        {
            PointsAvailableText.text = $"Points: {PointsAvailable}";
        }

        private void LevelSystemOnLevelChanged(object sender, EventArgs e)
        {
            LeanTween.scale(text.gameObject, Vector3.one * 1.5f, 0.2f).setOnComplete(() =>
                {
                    LeanTween.scale(text.gameObject, Vector3.one, 0.2f);
                });
        }

        private void PlayerBaseOnScoreIncremented(object sender, int e)
        {
            if (e > 0)
            {
                levelSystem.AddExperience(e);
                UpdateUi();
            }
        }

        public void UpdateLevel()
        {
            //levelSystem.AddExperience(playerBase.Score);
            levelSystem.Save();
            UpdateUi();
        }

        private void UpdateUi()
        {
            levelfill.fillAmount = levelSystem.ExperienceNormal;
            text.text = "Level: " + levelSystem.Level;
        }

        public void Save()
        {
            levelSystem.Save();
        }
    }
}