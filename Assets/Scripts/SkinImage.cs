using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinImage : MonoBehaviour
{
    [HideInInspector] public SkinObject skin;
    [SerializeField] public GameObject selectedIcon;
    [SerializeField] public Image cadre;

    void Start()
    {
    }

    public void OnClick()
    {
        
        transform.GetComponentInParent<SkinGrid>().ChangeDisplaySkin(skin);
    }
}
