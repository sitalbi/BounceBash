using System;
using System.Collections;
using System.Collections.Generic;
using RDG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] public List<SkinObject> skinList, acquiredSkins, unlockedSkins;
    [SerializeField] private Image displayedImage;
    [SerializeField] private GameObject selectedIcon, lockedIcon, coinImage, coinAmountObject;
    [SerializeField] private TMP_Text selectButtonText, nameText;
    [SerializeField] private AudioSource coin, select;
    [SerializeField] private ParticleSystem particles;

    private int skinIndex;
    private SkinObject displayedSkin;
    private string skinBool;
    

    void Start()
    {
        if (!PlayerPrefs.HasKey("skinId")) {
            PlayerPrefs.SetInt("skinId", 0);
        }
        InitializeDisplayedSkin();

        for (int i = 0; i < skinList.Count; i++) {
            if (!PlayerPrefsExtra.GetBool("skin" + i)) {
                if (i == 0) {
                    PlayerPrefsExtra.SetBool("skin" + i,true);
                }
                else {
                    PlayerPrefsExtra.SetBool("skin" + i,false);
                } 
            }
        }
    }

    
    void Update() {
        skinBool = "skin" + skinIndex;
        displayedImage.GetComponent<Image>().sprite = displayedSkin.sprite;
        if (skinIndex == PlayerPrefs.GetInt("skinId")) {
            selectedIcon.SetActive(true);
        }
        else {
            selectedIcon.SetActive(false);
        }

        if (PlayerPrefsExtra.GetBool(skinBool) == false) {
            selectButtonText.text = "Buy " + displayedSkin.price;
            selectButtonText.alignment = TextAlignmentOptions.Left;
            selectButtonText.margin = new Vector4(115,0,0,0);
            coinImage.SetActive(true);
            lockedIcon.SetActive(true);
        }
        else {
            selectButtonText.text = "Select";
            selectButtonText.alignment = TextAlignmentOptions.Center;
            selectButtonText.margin = Vector4.zero;
            lockedIcon.SetActive(false);
            displayedSkin.isAquired = true;
            coinImage.SetActive(false);
        }

        if (!acquiredSkins.Contains(displayedSkin))
        {
            unlockedSkins = PlayerPrefsExtra.GetList<SkinObject>("unlockedSkin");
            if (unlockedSkins == null)
            {
                unlockedSkins = new List<SkinObject>();
                PlayerPrefsExtra.SetList("unlockedSkin", unlockedSkins);
            }
            if (displayedSkin.price > PlayerPrefs.GetInt("Coins") && !unlockedSkins.Contains(displayedSkin))
            {
                displayedImage.color = Color.black;
                nameText.text = "???";
                selectButtonText.text = "Locked";
                selectButtonText.alignment = TextAlignmentOptions.Center;
                coinImage.SetActive(false);
                lockedIcon.SetActive(true);
            }
            else
            {
                nameText.text = displayedSkin.name;
                displayedImage.color = Color.white;
                if (!unlockedSkins.Contains(displayedSkin))
                {
                    unlockedSkins.Add(displayedSkin);
                    PlayerPrefsExtra.SetList("unlockedSkin", unlockedSkins);
                }
            }
        }
    }

    public void RightButton() {
        if (PlayerPrefs.GetInt("vibration") == 1)
        {
            Vibration.Vibrate(100, 50);
        }
        if (skinIndex + 1 < skinList.Count) {
            skinIndex++;
        }
        else {
            skinIndex = 0;
        }
        displayedSkin = PlayerPrefsExtra.GetList<SkinObject>("skinList")[skinIndex];
        displayedSkin = skinList[skinIndex];
    }
    
    public void LeftButton() {
        if (PlayerPrefs.GetInt("vibration") == 1)
        {
            Vibration.Vibrate(100, 50);
        }
        if (skinIndex - 1 >= 0) {
            skinIndex--;
        }
        else {
            skinIndex = skinList.Count-1;
        }
        displayedSkin = PlayerPrefsExtra.GetList<SkinObject>("skinList")[skinIndex];
        displayedSkin = skinList[skinIndex];
    }

    public void SelectSkin() {
        if (PlayerPrefsExtra.GetBool(skinBool) == false) {
            if (PlayerPrefs.GetInt("Coins") >= displayedSkin.price) {
                PlayerPrefsExtra.SetBool("skin" + skinIndex,true);
                int coinsAmount = PlayerPrefs.GetInt("Coins");
                PlayerPrefs.SetInt("Coins", coinsAmount-displayedSkin.price);
                coinAmountObject.GetComponent<CoinsManager>().UpdateCoinAmount();
                if (PlayerPrefsExtra.GetList<SkinObject>("acquiredSkin") != null)
                {
                    acquiredSkins = PlayerPrefsExtra.GetList<SkinObject>("acquiredSkin");
                    acquiredSkins.Add(displayedSkin);
                    PlayerPrefsExtra.SetList("acquiredSkin", acquiredSkins);
                }
                coin.Play();
                //Particles
                particles.Play();
            }
            else {
                return;
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("skinId") != skinIndex) select.Play();
        }
        PlayerPrefs.SetInt("skinId", skinIndex);
    }

    public void InitializeDisplayedSkin() {
        skinIndex = PlayerPrefs.GetInt("skinId");
        PlayerPrefsExtra.SetList("skinList",skinList);
        displayedSkin = skinList[skinIndex];
    }
}
