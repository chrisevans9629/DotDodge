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
}