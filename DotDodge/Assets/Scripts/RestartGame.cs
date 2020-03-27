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
    private Vector2 playerStart;
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
        PauseUI.SetActive(false);
        Player.transform.position = playerStart;
        Player.Score = 0;
        Player.GameIsRunning = true;
        Player.IsDead = false;
        if (Player.sprite != null)
        {
            Player.sprite.color = Color.white;
        }

        Player.FireRateSeconds = 1;
        Player.BulletCount = 1;
        Player.ShieldCount = 0;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
