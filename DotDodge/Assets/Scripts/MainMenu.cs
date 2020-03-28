using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        //public Text text;
        void Start()
        {
            UpdateHighScoreText();
        }

        public void UpdateHighScoreText()
        {
            if (PlayerPrefs.HasKey("score"))
            {
                var texts = GameObject.FindGameObjectsWithTag("HighscoreText");
                foreach (var text in texts)
                {
                    if (text != null)
                    {
                        text.GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetFloat("score", 0).ToString(CultureInfo.InvariantCulture);
                    }
                }
            }
        }
    }
}