using System;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class LevelSystem
    {
        public int Level = 1;
        public int Experience;
        public int ExperienceToNextLevel = 1000;
        public event EventHandler LevelChanged;
        public LevelSystem()
        {
            
        }

        public void Save()
        {
            PlayerPrefs.SetString("Level", JsonUtility.ToJson(this));
        }

        public static LevelSystem Load()
        {
            if (PlayerPrefs.HasKey("Level"))
            {
                return JsonUtility.FromJson<LevelSystem>(PlayerPrefs.GetString("Level"));
            }
            return new LevelSystem();
        }
        public void AddExperience(int amt)
        {
            Experience += amt;
            if (Experience >= ExperienceToNextLevel)
            {
                Experience -= ExperienceToNextLevel;
                ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.1f);
                Level++;
                OnLevelChanged();
            }
        }

        public float ExperienceNormal => (float)Experience  / ExperienceToNextLevel;

        protected virtual void OnLevelChanged()
        {
            LevelChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}