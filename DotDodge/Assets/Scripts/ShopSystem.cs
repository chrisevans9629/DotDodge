﻿using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class ShopSystem
    {
        public int LevelsUsed;
        public int BulletCount = 1;
        public int StartingHealth = 0;
        public int MaxBullets = 5;
        public int MaxHealth = 3;

        public void Save()
        {
            PlayerPrefs.SetString("Shop",JsonUtility.ToJson(this));
        }

        public static ShopSystem Load()
        {
            return JsonUtility.FromJson<ShopSystem>(PlayerPrefs.GetString("Shop"));
        }
    }
}