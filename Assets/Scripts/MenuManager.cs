using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore, coins;
    [SerializeField] private GameObject mainMenu, settingsMenu, skinMenu, coinsObject;
    
    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        if (highscore != null) {
            highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
        }
        if (coins != null) {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }

    public void Play() {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void Settings() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        coinsObject.SetActive(false);
    }

    public void Menu() {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        skinMenu.SetActive(false);
        coinsObject.SetActive(true);
    }
    
    public void Skins() {
        mainMenu.SetActive(false);
        skinMenu.SetActive(true);
    }

    public void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
