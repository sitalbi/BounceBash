using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinGrid : MonoBehaviour
{
    [SerializeField] public List<SkinObject> skinList;
    [SerializeField] private GameObject skinUIElement, selectButton;
    [SerializeField] private TMP_Text nameText, buttonText;
    [SerializeField] private Sprite unacquiredSprite;
    [SerializeField] private CoinsManager coinsManager;

    private SkinObject displayedSkin;
    private List<GameObject> skinUIElements;


    // Start is called before the first frame update
    void Start()
    {
        skinUIElements = new List<GameObject>();
        
        if (!PlayerPrefs.HasKey("SelectedSkinId"))
        {
            PlayerPrefs.SetInt("SelectedSkinId", 0);
        }
        
        if (!PlayerPrefs.HasKey("AcquiredSkins"))
        {
            List<SkinObject> skins = new List<SkinObject> { skinList[0] };
            PlayerPrefsExtra.SetList("AcquiredSkins", skins);
        }
        
        foreach (SkinObject skin in skinList)
        {
            // Reference the SkinObject in the UIElement
            skinUIElement.GetComponent<SkinImage>().skin = skin;
            // UI checks
            if (PlayerPrefsExtra.GetList<SkinObject>("AcquiredSkins").Contains(skin))
            {
                skinUIElement.GetComponent<Image>().sprite = skin.sprite;
                if (skin.id == PlayerPrefs.GetInt("SelectedSkinId"))
                {
                    displayedSkin = skin;
                }
            }
            else
            {
                skinUIElement.GetComponent<Image>().sprite = unacquiredSprite;
            }
            
            // BUG: la list contient que le Skin3 (3 instances)
            // Solution : Faire un tableau classique au lieu d'une liste?
            skinUIElements.Add(skinUIElement);
            
            foreach (var s in skinUIElements)
            {
                Debug.Log(s.GetComponent<SkinImage>().skin);
            }
            
            Instantiate(skinUIElement, transform);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // Name
        nameText.text = displayedSkin.name;
        
        // Select Button
        if (PlayerPrefsExtra.GetList<SkinObject>("AcquiredSkins").Contains(displayedSkin)) 
        {
            if (PlayerPrefs.GetInt("SelectedSkinId") != displayedSkin.id)
            {
                selectButton.SetActive(true);
                buttonText.text = "Select";
            }
            else
            {
                selectButton.SetActive(false);
            }
            
        } else
        {
            selectButton.SetActive(true);
            buttonText.text = displayedSkin.price.ToString();
        }
        
        // Update les skins NOT WORKING
        /*foreach (Transform skinUI in transform)
        {
            if (PlayerPrefsExtra.GetList<SkinObject>("AcquiredSkins").Contains(skinUI.GetComponent<SkinImage>().skin))
            {
                skinUI.GetComponent<Image>().sprite = skinUI.GetComponent<SkinImage>().skin.sprite;
            }
            else
            {
                skinUI.GetComponent<Image>().sprite = unacquiredSprite;
            }
            
        }*/
        
    }

    public void ChangeDisplaySkin(SkinObject sk)
    {
        displayedSkin = sk;
    }

    public void SelectOnClick()
    {
        Debug.Log("click");
        List<SkinObject> list = PlayerPrefsExtra.GetList<SkinObject>("AcquiredSkins");
        // Buy
        if (!list.Contains(displayedSkin))
        {
            int oldValue = PlayerPrefs.GetInt("Coins");
            if (oldValue >= displayedSkin.price)
            {
                PlayerPrefs.SetInt("Coins", oldValue - displayedSkin.price);
                list.Add(displayedSkin);
                // TODO: add sound when buying
                coinsManager.UpdateCoinAmount();
                PlayerPrefs.SetInt("SelectedSkinId", displayedSkin.id);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SelectedSkinId", displayedSkin.id);
        }
        PlayerPrefsExtra.SetList("AcquiredSkins", list);
    }
}