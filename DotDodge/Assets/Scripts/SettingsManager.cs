using Assets.Scripts;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [HideInInspector]
    public SettingSystem settingSystem;
    public static SettingsManager instance;
    void Awake()
    {
        settingSystem = SettingSystem.Load();
        instance = this;
    }
}