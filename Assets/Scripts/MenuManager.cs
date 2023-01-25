
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
        
        AudioListener.volume = PlayerPrefs.GetInt("sound");
        
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
        AudioListener.volume = PlayerPrefs.GetInt("sound");
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
}
