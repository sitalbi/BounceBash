﻿
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skin")]
public class SkinObject : ScriptableObject
{
    public Sprite sprite;
    public int price;
    public string name;
    public bool isAquired;
    public bool isAnimated;
    public RuntimeAnimatorController controller;
}
