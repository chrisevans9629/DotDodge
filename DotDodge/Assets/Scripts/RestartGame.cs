using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public PlayerBase Player;
    public GameObject Spawner;
    public GameObject PauseUI;
    private Vector3 playerStart;
    public GameObject ResumeButton;
    public Button ContinueButton;
    public MainMenu MainMenu;
    public UnityEvent GameRestarted;
    int gameCount = 1;
    public int ShowAdEveryXGame = 5;
    private bool CanContinue = true;
    private LevelManager levelManager;



    void Start()
    {
        AdManager.Instance.ContinueAction = Continue;

        AdManager.SetupButton(ContinueButton);

        //AdManager.Instance.ContinueButton = ContinueButton;
        levelManager = GetComponent<LevelManager>();
        //Advertisement.Initialize("3532261", TestMode);
        playerStart = Player.transform.position;
        Debug.Log("Restart game loaded");
    }

    private void OnDestroy()
    {
        Debug.Log("destroying", this);
    }

    public void Continue()
    {
        if (gameObject == null)
        {
            Debug.LogError("gameObject was null");
        }
        else
        {
            Debug.Log("Continue...");
        }
        CanContinue = false;
        Time.timeScale = 1;
        LeanTween.value(this.gameObject, v => Player.transform.position = v, Player.transform.position, playerStart, 0.5f)
            .setOnComplete(() =>
            {
                Player.GameIsRunning = true;
                Player.IsDead = false;
            });
        PauseUI.SetActive(false);
        if (Player.sprite != null)
        {
            Player.sprite.color = Color.white;
        }
        Player.FireRateSeconds = 1;
        levelManager.SetupPlayerLevel();

        if (Player is Player2D p)
        {
            p.rb.velocity = Vector2.zero;
        }

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(o);
        }



        foreach (Transform children in Spawner.transform)
        {
            Destroy(children.gameObject);
        }

        foreach (var o in GameObject.FindGameObjectsWithTag("PointsText"))
        {
            Destroy(o);
        }

        MainMenu.UpdateHighScoreText();
        GameRestarted.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        //MainMenu.text.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        if (Player.IsDead)
        {
            if (gameCount % ShowAdEveryXGame == 0 && !IAPManager.instance.HasRemovedAds)
            {
                if (Advertisement.IsReady("RestartVideo"))
                {
                    Advertisement.Show("RestartVideo");
                }
            }
            ResumeButton.SetActive(false);
            if (CanContinue)
            {
                ContinueButton.gameObject.SetActive(true);
            }
            else
            {
                ContinueButton.gameObject.SetActive(false);
            }
        }
        else
        {
            ResumeButton.SetActive(true);
            ContinueButton.gameObject.SetActive(false);
        }
        MainMenu.UpdateHighScoreText();

        //MainMenu.text.gameObject.SetActive(true);
    }

    public void GiveUp()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
        }
    }

    //void Update()
    //{
    //    Debug.Log(this);
    //}
    public void Restart()
    {
        gameCount++;
        Player.Score = 0;
        var spawners = Spawner.GetComponents<SpawnerBase>();
        foreach (var item in spawners)
        {
            item.ResetSpawner();
        }
        Continue();
        CanContinue = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
