﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText, yourScoreText, bestScoretext, coinText;
    [SerializeField] private GameObject player, spikeSpawner1, spikeSpawner2, inGameUI, gameOverUI, continueButton,blueText, redText;
    [SerializeField] private UILeanTween tween;
    [SerializeField] public ObjectSpawner spawner;
    [SerializeField] private float minCoolDown, coolDownStep;
    [SerializeField] private TimerManager timerManager;
    [SerializeField] private AdsInitializer adsManager;

    [NonSerialized] public int score;
    [NonSerialized] public bool inGame;

    private bool coolDownChanged, isDead, canContinue, buttonClicked;
    [SerializeField] private CoinsManager coinsManager;

    private int screenPressed, coinsNumber;

    void Start() {
        score = 0;
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);
        canContinue = true;
        screenPressed = 0;
        inGame = false;
    }


    void Update() {
        scoreText.text = score.ToString();
        if (score != 0) {
            if (score % 10 == 0) {
                if (spawner.obstacleCoolDown > minCoolDown && !coolDownChanged) {
                    spawner.obstacleCoolDown -= coolDownStep;
                    coolDownChanged = true;
                }
            }

            if (score > 10) {
                spawner.scoreMoreThan10 = true;
            }

            if (score > 35) {
                spikeSpawner1.SetActive(true);
                spikeSpawner2.SetActive(true);
            }
        }
        else {
            coolDownChanged = false;
        }

        if (isDead && !timerManager.timerIsRunning)
        {
            continueButton.SetActive(false);
        }

        if (player.GetComponent<PlayerController>().isOnWall || player.GetComponent<Rigidbody2D>().gravityScale != 0)
        {
            blueText.SetActive(false);
            redText.SetActive(false);
        }
        else
        {
            blueText.SetActive(true);
            redText.SetActive(true);
        }
    }

    public void Death() {
        player.GetComponent<PlayerController>().Death();
        inGame = false;
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);
        tween.LevelComplete();
        if (!canContinue) {
            continueButton.SetActive(false);
        }
        else {
            continueButton.SetActive(true);
        }
        coinText.text = "+" + coinsNumber;
        if (PlayerPrefs.HasKey("Coins")) {
            int oldAmount = PlayerPrefs.GetInt("Coins");
            if (oldAmount < 999) {
                PlayerPrefs.SetInt("Coins", oldAmount+coinsNumber);
            }
        }
        else {
            PlayerPrefs.SetInt("Coins", coinsNumber);
        }
        AddCoins(coinsNumber);
        yourScoreText.text = score.ToString();
        bestScoretext.text = PlayerPrefs.GetInt("HighScore") >= score ? "Best score: " + PlayerPrefs.GetInt("HighScore") : "New Best";
        isDead = true;
        Time.timeScale = 0;
    }

    private void Dead() {
        isDead = true;
    }

    public void Continue() {
        buttonClicked = true;
        canContinue = false;
        coinsNumber = 0;
        player.GetComponent<PlayerController>().Respawn();
        tween.Initialize();
        gameOverUI.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1;
        isDead = false;
    }

    public void Menu() {
        PlayerPrefs.SetInt("gamesCpt", PlayerPrefs.GetInt("gamesCpt")+1);
        if (PlayerPrefs.HasKey("HighScore")) {
            if (score > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else {
            PlayerPrefs.SetInt("HighScore", score);
        }

        if (PlayerPrefs.GetInt("gamesCpt") > 3)
        {
            // Interstitate ad
            adsManager.LoadInterstitialAd();
            PlayerPrefs.SetInt("gamesCpt", 0);
        }
        else
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    public void EarnCoin()
    {
        coinsNumber++;
    }
    
    

    private void AddCoins (int amount)
    {
        coinsManager.Animate(coinText.transform.position, amount);
    }
}