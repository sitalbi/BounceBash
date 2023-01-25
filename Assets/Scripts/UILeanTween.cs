
using UnityEngine;

public class UILeanTween : MonoBehaviour
{

    [SerializeField] private GameObject button1, button2, score, best, wallL, wallR, inGameScore;
    [SerializeField] private float transitionTime;
    [SerializeField] private PlayerController playerController;

    private Vector3 button1Scale, button2Scale, bestScale, scorePos;

    void Start() {
        Initialize();
    }

    public void LevelComplete() {
        LeanTween.scale(button1, button1Scale, transitionTime).setDelay(.1f).setEase(LeanTweenType.easeInOutQuint)
            .setIgnoreTimeScale(true);
        LeanTween.scale(button2, button2Scale, transitionTime).setDelay(.3f).setEase(LeanTweenType.easeInOutQuint)
            .setIgnoreTimeScale(true);
        LeanTween.scale(best, bestScale, transitionTime).setDelay(.3f).setEase(LeanTweenType.easeInOutQuint)
            .setIgnoreTimeScale(true);
        LeanTween.move(score, scorePos, transitionTime).setDelay((.1f)).setEase(LeanTweenType.easeInOutQuint)
            .setIgnoreTimeScale(true);
    }

    public void Initialize() {
        button1Scale = button1.transform.localScale;
        button2Scale = button2.transform.localScale;
        bestScale = best.transform.localScale;
        scorePos = score.transform.position;

        LeanTween.scale(button1, Vector3.zero, 0f);
        LeanTween.scale(button2, Vector3.zero, 0f);
        LeanTween.scale(best, Vector3.zero, 0f);
        LeanTween.move(score, new Vector3(0, 105), 0f);
        LeanTween.move(wallL, new Vector3(-9.5f, wallL.transform.position.y), 0.5f).setEase(LeanTweenType.easeInOutQuint);
        LeanTween.move(wallR, new Vector3(9.5f, wallR.transform.position.y), 0.5f).setEase(LeanTweenType.easeInOutQuint).setOnComplete(playerController.CanMove);
        LeanTween.moveLocal(inGameScore, new Vector3(inGameScore.transform.localPosition.x, 1100), 0.5f).setEase(LeanTweenType.easeInOutQuint).setOnComplete(playerController.CanMove);
    }
}
