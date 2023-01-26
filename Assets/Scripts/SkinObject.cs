
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Skin")]
public class SkinObject : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public int price;
    public string name;
    public bool isAcquired;
    public bool isAnimated;
    public RuntimeAnimatorController controller;
}
