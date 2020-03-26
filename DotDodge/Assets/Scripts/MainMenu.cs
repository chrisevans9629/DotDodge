﻿using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public Text text;
        void Start()
        {
            UpdateHighScoreText();
        }

        public void UpdateHighScoreText()
        {
            if (PlayerPrefs.HasKey("score"))
            {
                text.text = "Highscore: " + PlayerPrefs.GetFloat("score", 0).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}