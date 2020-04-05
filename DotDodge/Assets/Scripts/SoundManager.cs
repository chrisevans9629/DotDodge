using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private SettingSystem settingSystem;
    [HideInInspector]
    private List<AudioSource> SoundEffects = new List<AudioSource>();

    public static SoundManager SoundManagerInstance;
    public AudioSource Music;
    void Awake()
    {
        SoundManagerInstance = this;
        settingSystem = SettingSystem.Load();
    }

    void Start()
    {
        UpdateSound();
    }

    private void UpdateSound()
    {
        Music.mute = settingSystem.IsMusicMuted;
        foreach (var soundEffect in SoundEffects)
        {
            if (soundEffect != null)
                soundEffect.mute = settingSystem.IsSoundMuted;
        }
    }

    public void Add(AudioSource audioSource)
    {
        SoundEffects.Add(audioSource);
        audioSource.mute = settingSystem.IsSoundMuted;
    }
    public bool GetSound(SettingsActions action)
    {
        if (action == SettingsActions.Music)
        {
            return settingSystem.IsMusicMuted;
        }
        else if (action == SettingsActions.Sound)
        {
            return settingSystem.IsSoundMuted;
        }
        throw new NotImplementedException();
    }

    public void UpdateSound(SettingsActions action, bool isMuted)
    {
        if (action == SettingsActions.Music)
        {
            settingSystem.IsMusicMuted = isMuted;
        }
        else if (action == SettingsActions.Sound)
        {
            settingSystem.IsSoundMuted = isMuted;
        }
        settingSystem.Save();
        UpdateSound();
    }
}