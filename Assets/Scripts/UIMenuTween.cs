using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuTween : MonoBehaviour
{

    [SerializeField] private GameObject logo, score, playButton, coins, skin, settings;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private float transitionTime;
    private GameObject secondaryMenu;
    private Vector3 logoOriginalPosition, scoreOriginalPosition, playButtonOriginalScale, coinsOriginalPosition, skinOriginalPosition, settingsOriginalPosition, adOriginalPosition;

    void Awake() {
        logoOriginalPosition = logo.transform.localPosition;
        scoreOriginalPosition = score.transform.localPosition;
        playButtonOriginalScale = playButton.transform.localScale;
        coinsOriginalPosition = coins.transform.position;
        skinOriginalPosition = skin.transform.localPosition;
        settingsOriginalPosition = settings.transform.localPosition;
    }

    public void ChangeMenuTransition(GameObject menuToChange) {
        secondaryMenu = menuToChange;
        LeanTween.scale(secondaryMenu, Vector3.zero, 0);
        LeanTween.moveLocal(score, new Vector3(0,700), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(logo, new Vector3(0,1500), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(playButton, new Vector3(0,0), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        //LeanTween.move(coins, new Vector3(coinsOriginalPosition.x,-700), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(skin, new Vector3(skinOriginalPosition.x,-1500), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(settings, new Vector3(settingsOriginalPosition.x,-1500), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(secondaryMenu, Vector3.one, 0).setDelay(transitionTime);
    }

    public void GoBackToMenu() {
        LeanTween.moveLocal(score, scoreOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(logo, logoOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(playButton, playButtonOriginalScale, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        //LeanTween.move(coins, coinsOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(skin, skinOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(settings, settingsOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        menuManager.DeactivateMenus();
    }
}