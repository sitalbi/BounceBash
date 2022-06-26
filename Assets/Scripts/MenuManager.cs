using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore, coins;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        if (highscore != null) {
            highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
        }
        if (coins != null) {
            coins.text = "Coins\n" + PlayerPrefs.GetInt("Coins");
        }
    }

    public void Play() {
        SceneManager.LoadScene("Scenes/Game");
    }
}
