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
    [SerializeField] private GameObject selectedIcon, lockedIcon, coinImage;
    [SerializeField] private TMP_Text selectButtonText;
    [SerializeField] private AudioSource coin, select;

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
        displayedImage.sprite = displayedSkin.sprite;
        if (skinIndex == PlayerPrefs.GetInt("skinId")) {
            selectedIcon.SetActive(true);
        }
        else {
            selectedIcon.SetActive(false);
        }

        if (PlayerPrefsExtra.GetBool(skinBool) == false) {
            selectButtonText.text = "Buy " + displayedSkin.price;
            selectButtonText.alignment = TextAlignmentOptions.Left;
            selectButtonText.margin = new Vector4(20,0,0,0);
            coinImage.SetActive(true);
            lockedIcon.SetActive(true);
        }
        else {
            selectButtonText.text = "Select";
            selectButtonText.alignment = TextAlignmentOptions.Center;
            selectButtonText.margin = Vector4.zero;
            lockedIcon.SetActive(false);
            coinImage.SetActive(false);
        }
    }

    public void RightButton() {
        if (skinIndex + 1 < skinList.Count) {
            skinIndex++;
        }
        else {
            skinIndex = 0;
        }
        displayedSkin = PlayerPrefsExtra.GetList<SkinObject>("skinList")[skinIndex];
    }
    
    public void LeftButton() {
        if (skinIndex - 1 >= 0) {
            skinIndex--;
        }
        else {
            skinIndex = skinList.Count-1;
        }
        displayedSkin = PlayerPrefsExtra.GetList<SkinObject>("skinList")[skinIndex];
    }

    public void SelectSkin() {
        if (PlayerPrefsExtra.GetBool(skinBool) == false) {
            if (PlayerPrefs.GetInt("Coins") >= displayedSkin.price) {
                PlayerPrefsExtra.SetBool("skin" + skinIndex,true);
                int coinsAmount = PlayerPrefs.GetInt("Coins");
                PlayerPrefs.SetInt("Coins", coinsAmount-displayedSkin.price);
                coin.Play();
                
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
