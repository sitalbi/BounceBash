using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILeanTween : MonoBehaviour
{

    [SerializeField] private GameObject button1, button2, score, best;

    void Start() {
        Initialize();
    }

    public void LevelComplete() {
        LeanTween.scale(button1, new Vector3(0.15f, 0.15f, 0.15f), 0.8f).setDelay(.1f).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(button2, new Vector3(0.15f, 0.15f, 0.15f), 0.8f).setDelay(.5f).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(score, new Vector3(0,50), 1f).setDelay((.1f)).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
    }

    public void Initialize() {
        LeanTween.scale(button1, new Vector3(0f, 0f, 0f), 0f);
        LeanTween.scale(button2, new Vector3(0f, 0f, 0f), 0f);
        LeanTween.moveLocal(score, new Vector3(0,150), 0f);
    }
}
