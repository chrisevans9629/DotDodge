using UnityEngine;

namespace Assets.Scripts
{
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
}