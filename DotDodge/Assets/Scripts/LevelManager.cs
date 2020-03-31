using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        private LevelSystem levelSystem;
        public Image levelfill;
        public Text text;
        private PlayerBase playerBase;
        void Start()
        {
            playerBase = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
            levelSystem = LevelSystem.Load();
            UpdateUi();
            playerBase.ScoreIncremented += PlayerBaseOnScoreIncremented;
        }

        private void PlayerBaseOnScoreIncremented(object sender, EventArgs e)
        {
            levelSystem.AddExperience(1);
            UpdateUi();
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