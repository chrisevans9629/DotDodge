using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class SettingSystem
    {
        public bool IsMusicMuted = false;
        public bool IsSoundMuted = false;

        public void Save()
        {
            PlayerPrefs.SetString("Settings",JsonUtility.ToJson(this));
        }

        public static SettingSystem Load()
        {
            if (PlayerPrefs.HasKey("Settings"))
            {
                return JsonUtility.FromJson<SettingSystem>(PlayerPrefs.GetString("Settings"));
            }
            return new SettingSystem();
        }
    }


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
                ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.2f);
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