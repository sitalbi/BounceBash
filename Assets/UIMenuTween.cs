using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuTween : MonoBehaviour
{

    [SerializeField] private GameObject logo, playButton, coins, bottomButtons;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private float transitionTime;
    private GameObject secondaryMenu;
    private Vector3 logoOriginalPosition, playButtonOriginalScale, coinsOriginalPosition, bottomOriginalPosition;

    void Start() {
        logoOriginalPosition = logo.transform.position;
        playButtonOriginalScale = playButton.transform.localScale;
        coinsOriginalPosition = coins.transform.position;
        bottomOriginalPosition = bottomButtons.transform.position;
    }

    public void ChangeMenuTransition(GameObject menuToChange) {
        secondaryMenu = menuToChange;
        LeanTween.scale(secondaryMenu, Vector3.zero, 0);
        LeanTween.moveLocal(logo, new Vector3(0,700), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(playButton, new Vector3(0,0), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.move(coins, new Vector3(coinsOriginalPosition.x,-700), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(bottomButtons, new Vector3(bottomOriginalPosition.x,-700), transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(secondaryMenu, Vector3.one, 0).setDelay(transitionTime);
    }

    public void GoBackToMenu() {
        LeanTween.moveLocal(logo, logoOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.scale(playButton, playButtonOriginalScale, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.move(coins, coinsOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        LeanTween.moveLocal(bottomButtons, bottomOriginalPosition, transitionTime).setEase(LeanTweenType.easeInOutQuint).setIgnoreTimeScale(true);
        menuManager.DeactivateMenus();
    }
}