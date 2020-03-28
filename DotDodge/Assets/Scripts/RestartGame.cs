using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public PlayerBase Player;
    public GameObject Spawner;
    public GameObject PauseUI;
    private Vector3 playerStart;
    public GameObject ResumeButton;
    public MainMenu MainMenu;
    void Start()
    {
        playerStart = Player.transform.position;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        MainMenu.text.gameObject.SetActive(false);
    }

    public void Pause()
    {
        MainMenu.UpdateHighScoreText();
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        if (Player.IsDead)
        {
            ResumeButton.SetActive(false);
        }
        else
        {
            ResumeButton.SetActive(true);
        }
        MainMenu.text.gameObject.SetActive(true);
    }

    public void GiveUp()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;

        LeanTween.value(this.gameObject, v => Player.transform.position = v, Player.transform.position, playerStart, 0.5f)
            .setOnComplete(() =>
            {
                Player.GameIsRunning = true;
                Player.IsDead = false;
            });
        PauseUI.SetActive(false);

        //Player.transform.position = playerStart;
        Player.Score = 0;
        
        if (Player.sprite != null)
        {
            Player.sprite.color = Color.white;
        }

        Player.FireRateSeconds = 1;
        Player.BulletCount = 1;
        Player.ResetHealth();
        if (Player is Player2D p)
        {
            p.rb.velocity = Vector2.zero;
        }
        
        foreach (Transform children in Spawner.transform)
        {
            Destroy(children.gameObject);
            //Destroy(children);
        }
        MainMenu.text.gameObject.SetActive(false);
        MainMenu.UpdateHighScoreText();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
