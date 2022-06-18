using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highscore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        highscore.text = "Best score\n" + PlayerPrefs.GetInt("HighScore");
    }

    public void Play() {
        SceneManager.LoadScene("Scenes/Game");
    }
}
