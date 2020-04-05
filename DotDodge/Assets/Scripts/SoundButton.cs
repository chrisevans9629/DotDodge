using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SettingsActions
{
    Music,
    Sound
}


[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    private string Name;
    private Button slider;
    public bool IsMuted = false;
    private Text text;
    public SettingsActions Action;

    // Start is called before the first frame update
    void Start()
    {
        IsMuted = SoundManager.SoundManagerInstance.GetSound(Action);
        slider = GetComponent<Button>();
        slider.onClick.AddListener(Clicked);
        text = slider.GetComponentInChildren<Text>();
        Name = text.text;
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (IsMuted)
        {
            text.text = Name + ": Muted";
        }
        else
        {
            text.text = Name + ": Unmuted";
        }
    }


    void Clicked()
    {
        IsMuted = !IsMuted;
        SoundManager.SoundManagerInstance.UpdateSound(Action, IsMuted);      
        UpdateButton();
    }

}
