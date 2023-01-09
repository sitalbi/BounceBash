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
    }
    
    void Update() {
        if (highscore != null) {
            highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
        }
        
    }

    public void Play() {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void Settings() {
        settingsMenu.SetActive(true);
        tween.ChangeMenuTransition(settingsMenu);
    }

    public void Menu() {
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
