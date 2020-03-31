using UnityEngine;

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
        public float BulletSpeed = 6;
        public float BulletRate = 1;

        public void Save()
        {
            PlayerPrefs.SetString("Shop",JsonUtility.ToJson(this));
        }

        public static ShopSystem Load()
        {
            if (PlayerPrefs.HasKey("Shop"))
            {
                return JsonUtility.FromJson<ShopSystem>(PlayerPrefs.GetString("Shop"));
            }
            return new ShopSystem();
        }
    }
}