
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Sprite soundOn, soundOff, vibrationOn, vibrationOff;
    [SerializeField] private GameObject sound, vibration;
    
    private int value;
    private int newValue;

    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
            value = 1;
        }
        sound.GetComponent<Image>().sprite = PlayerPrefs.GetInt("sound") == 1 ? soundOn : soundOff;
        
        if (!PlayerPrefs.HasKey("vibration"))
        {
            PlayerPrefs.SetInt("vibration", 1);
            value = 1;
        }
        vibration.GetComponent<Image>().sprite = PlayerPrefs.GetInt("vibration") == 1 ? vibrationOn : vibrationOff;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSound()
    {
        value = PlayerPrefs.GetInt("sound");
        newValue = value == 1 ? 0 : 1;
        PlayerPrefs.SetInt("sound", newValue);
        sound.GetComponent<Image>().sprite = newValue == 1 ? soundOn : soundOff;
    }
    
    public void SetVibration()
    {
        value = PlayerPrefs.GetInt("vibration");
        newValue = value == 1 ? 0 : 1;
        PlayerPrefs.SetInt("vibration", newValue);
        vibration.GetComponent<Image>().sprite = newValue == 1 ? vibrationOn : vibrationOff;
    }
    
    
}
