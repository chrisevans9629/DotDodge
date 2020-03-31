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
            levelSystem.LevelChanged += LevelSystemOnLevelChanged;
            UpdateUi();
            playerBase.ScoreIncremented += PlayerBaseOnScoreIncremented;
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