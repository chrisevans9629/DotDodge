﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public PlayerBase Player;
    public GameObject Spawner;
    public GameObject PauseUI;
    private Vector2 playerStart;
    public GameObject ResumeButton;
    void Start()
    {
        playerStart = Player.transform.position;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
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
        foreach (Transform children in Spawner.transform)
        {
            Destroy(children.gameObject);
            //Destroy(children);
        }
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
