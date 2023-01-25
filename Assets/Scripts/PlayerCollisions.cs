
using UnityEngine;
using Random = System.Random;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private int offScreenLayer;
    [SerializeField] private string obstacleTag, collectableTag, wallTag, bonusTag;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource scoreSound;
    [SerializeField] private GameObject wallR, wallL;

    private PlayerController playerController;
    private Random rn = new Random();

    void Start() {
        playerController = GetComponent<PlayerController>();
    }


    void Update() {
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(wallTag)) {
            gameManager.score++;
            playerController.isOnWall = true;
            playerController.xDirection *= -1;
            playerController.yDirection = col.gameObject.GetComponent<WallData>().yDirection;
            playerController.SetGravityScale(0);
            scoreSound.Play(0);
        }

        if (col.gameObject.CompareTag(obstacleTag)) {
            //Death
            gameManager.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag(collectableTag) || col.gameObject.CompareTag(bonusTag)) {
            float time = 3f;
            if (col.gameObject.CompareTag(collectableTag))
            {
                gameManager.EarnCoin();
                Invoke(nameof(SetCollectablePosition), time);
            }

            if (col.gameObject.CompareTag(bonusTag))
            {
                wallL.GetComponent<WallData>().ChangeColor();
                wallR.GetComponent<WallData>().ChangeColor();
                time = rn.Next(25, 100);
                Invoke(nameof(SetBonusPosition), time);
            }
            col.gameObject.GetComponent<CollectableController>().Touched();
        }

        if (col.gameObject.layer == offScreenLayer) {
            gameManager.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag(wallTag)) {
            playerController.isOnWall = false;
            playerController.SetGravityScale(playerController.originalGravityScale);
        }
    }

    private void SetCollectablePosition() {
        gameManager.spawner.SetCollectablePosition();
    }

    private void SetBonusPosition()
    {
        gameManager.spawner.SetBonusPosition();
    }
}