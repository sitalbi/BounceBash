using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore;
    [SerializeField] private GameObject mainMenu, settingsMenu, skinMenu, coinsObject;
    [SerializeField] private UIMenuTween tween;
    
    void Start() {
        Time.timeScale = 1f;
        if (highscore != null) {
            highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
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
    }
    
    public void Skins() {
        skinMenu.SetActive(true);
        tween.ChangeMenuTransition(skinMenu);
    }

    public void DeactivateMenus() {
        settingsMenu.SetActive(false);
        skinMenu.SetActive(false);
    }

    public void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
