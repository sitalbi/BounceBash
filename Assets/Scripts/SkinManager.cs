using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    [SerializeField] public List<Sprite> skinList;
    [SerializeField] private Image displayedSkin;
    [SerializeField] private GameObject selectedIcon;

    private int skinIndex;

    void Start()
    {
        if (!PlayerPrefs.HasKey("skinId")) {
            PlayerPrefs.SetInt("skinId", 0);
        }
        skinIndex = PlayerPrefs.GetInt("skinId");
    }

    
    void Update()
    {
        displayedSkin.sprite = skinList[skinIndex];
        if (skinIndex == PlayerPrefs.GetInt("skinId")) {
            selectedIcon.SetActive(true);
        }
        else {
            selectedIcon.SetActive(false);
        }
    }

    public void RightButton() {
        if (skinIndex + 1 < skinList.Count) {
            skinIndex++;
        }
        else {
            skinIndex = 0;
        }
    }
    
    public void LeftButton() {
        if (skinIndex - 1 >= 0) {
            skinIndex--;
        }
        else {
            skinIndex = skinList.Count-1;
        }
    }

    public void SelectSkin() {
        PlayerPrefs.SetInt("skinId", skinIndex);
    }
}
