using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinImage : MonoBehaviour
{
    [HideInInspector] public SkinObject skin;

    void Start()
    {
    }

    public void OnClick()
    {
        
        transform.GetComponentInParent<SkinGrid>().ChangeDisplaySkin(skin);
    }
}
