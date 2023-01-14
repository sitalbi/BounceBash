using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore;
    [SerializeField] private GameObject mainMenu, settingsMenu, skinMenu, coinsObject, coinsButton;
    [SerializeField] private UIMenuTween tween;
    [SerializeField] private CoinsManager coinsManager;
    
    void Start() {
        Time.timeScale = 1f;
        if (highscore != null) {
            highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
        }

        if (PlayerPrefs.GetString("AdTime") != null)
        {
            if (DateTime.Today.Date.Subtract(DateTime.Parse(PlayerPrefs.GetString("AdTime")).Date) >=
                TimeSpan.FromDays(1) && PlayerPrefs.GetInt("HighScore") != 0)
            {
                coinsButton.SetActive(true);
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("HighScore") != 0)
                PlayerPrefs.SetString("AdTime",DateTime.Today.Date.ToString());
        }

        
    }
    
    void Update() {
        
        
    }

    public void Play() {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void Settings() {
        settingsMenu.SetActive(true);
        coinsObject.SetActive(false);
        tween.ChangeMenuTransition(settingsMenu);
    }

    public void Menu() {
        coinsObject.GetComponent<CoinsManager>().UpdateCoinAmount();
        mainMenu.SetActive(true);
        tween.GoBackToMenu();
        coinsObject.SetActive(true);
        skinMenu.GetComponent<SkinManager>().InitializeDisplayedSkin();
        skinMenu.GetComponent<SkinManager>().notificationManager.CheckNotifications();
    }
    
    public void Skins() {
        skinMenu.SetActive(true);
        tween.ChangeMenuTransition(skinMenu);
        coinsObject.GetComponent<CoinsManager>().UpdateCoinAmount();
    }

    public void DeactivateMenus() {
        settingsMenu.SetActive(false);
        skinMenu.SetActive(false);
    }

    public void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
    
    public void AddCoins ()
    {
        coinsManager.Animate(coinsButton.transform.position, 50);
        coinsButton.SetActive(false);
        PlayerPrefs.SetString("AdTime",DateTime.Today.Date.ToString());
        if (PlayerPrefs.HasKey("Coins")) {
            int oldAmount = PlayerPrefs.GetInt("Coins");
            if (oldAmount < 999) {
                PlayerPrefs.SetInt("Coins", oldAmount+50);
            }
        }
        else {
            PlayerPrefs.SetInt("Coins", 50);
        }
    }
}
