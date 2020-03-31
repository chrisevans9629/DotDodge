using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public enum ShopAction
{
    BulletCount,
    BulletSpeed,
    BulletMax,
    BulletRate
}

public class ShopButton : MonoBehaviour
{
    public ShopAction Action;
    public int Cost;
    Text cntText;
    private Text costText;
    private PlayerBase player;
    private Button button;

    LevelManager LevelManager;

    // Start is called before the first frame update
    void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        cntText = texts[0];
        costText = texts[1];
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(Clicked);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBase>();
        LevelManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
        UpdateButton();
    }

    void OnGUI()
    {
        UpdateButton();
    }
    private void UpdateButton()
    {
        if (LevelManager.PointsAvailable - Cost >= 0)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

        if (Action == ShopAction.BulletCount)
        {
            cntText.text = $"Bullets: {LevelManager.shopSystem.BulletCount}";
        }
        else if (Action == ShopAction.BulletSpeed)
        {
            cntText.text = $"Bullet Speed: {LevelManager.shopSystem.BulletSpeed}";
        }
        else if (Action == ShopAction.BulletRate)
        {
            cntText.text = $"Fire Rate: {LevelManager.shopSystem.BulletRate}";
        }
        else if (Action == ShopAction.BulletMax)
        {
            cntText.text = $"Max Bullets {LevelManager.shopSystem.MaxBullets}";
        }
        else
        {
            throw new NotImplementedException();
        }

        costText.text = $"Cost: {Cost}";
    }

    private void Clicked()
    {
        if (Action == ShopAction.BulletCount)
        {
            player.BulletCount++;
            LevelManager.shopSystem.BulletCount++;
            UpdateManager();
        }
        else if (Action == ShopAction.BulletSpeed)
        {
            player.BulletSpeed++;
            LevelManager.shopSystem.BulletSpeed++;
            UpdateManager();
        }
        else if (Action == ShopAction.BulletRate)
        {
            player.FireRateSeconds *= 0.9f;
            LevelManager.shopSystem.BulletRate = player.FireRateSeconds;
            UpdateManager();
        }
        else if (Action == ShopAction.BulletMax)
        {
            player.MaxBullets++;
            LevelManager.shopSystem.MaxBullets = player.MaxBullets;
            UpdateManager();
        }
        else
        {
            throw new NotImplementedException();
        }
        UpdateButton();
    }

    private void UpdateManager()
    {
        LevelManager.shopSystem.LevelsUsed += Cost;
        LevelManager.shopSystem.Save();
        LevelManager.UpdateText();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
