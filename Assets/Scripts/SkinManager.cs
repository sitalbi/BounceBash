using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] public List<SkinObject> skinList;
    [SerializeField] private Image displayedImage;
    [SerializeField] private GameObject selectedIcon, lockedIcon;
    [SerializeField] private TMP_Text selectButtonText;

    private int skinIndex;
    private SkinObject displayedSkin;

    void Start()
    {
        if (!PlayerPrefs.HasKey("skinId")) {
            PlayerPrefs.SetInt("skinId", 0);
        }
        skinIndex = PlayerPrefs.GetInt("skinId");
        displayedSkin = skinList[skinIndex];
    }

    
    void Update()
    {
        displayedImage.sprite = displayedSkin.sprite;
        if (skinIndex == PlayerPrefs.GetInt("skinId")) {
            selectedIcon.SetActive(true);
        }
        else {
            selectedIcon.SetActive(false);
        }

        if (displayedSkin.acquired == false) {
            selectButtonText.text = "Buy " + displayedSkin.price;
            lockedIcon.SetActive(true);
        }
        else {
            selectButtonText.text = "Select";
            lockedIcon.SetActive(false);
        }
    }

    public void RightButton() {
        if (skinIndex + 1 < skinList.Count) {
            skinIndex++;
        }
        else {
            skinIndex = 0;
        }
        displayedSkin = skinList[skinIndex];
    }
    
    public void LeftButton() {
        if (skinIndex - 1 >= 0) {
            skinIndex--;
        }
        else {
            skinIndex = skinList.Count-1;
        }
        displayedSkin = skinList[skinIndex];
    }

    public void SelectSkin() {
        if (displayedSkin.acquired == false) {
            if (PlayerPrefs.GetInt("Coins") >= displayedSkin.price) {
                displayedSkin.acquired = true;
                int coinsAmount = PlayerPrefs.GetInt("Coins");
                PlayerPrefs.SetInt("Coins", coinsAmount-displayedSkin.price);
            }
            else {
                return;
            }
        }
        PlayerPrefs.SetInt("skinId", skinIndex);
    }
}
