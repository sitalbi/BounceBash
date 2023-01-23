using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private int offScreenLayer;
    [SerializeField] private string obstacleTag, collectableTag, wallTag;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource scoreSound;

    private PlayerController playerController;

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
            playerController.SetGravityScale(0);
            scoreSound.Play(0);
        }

        if (col.gameObject.CompareTag(obstacleTag)) {
            //Death
            gameManager.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag(collectableTag)) {
            gameManager.EarnCoin();
            col.gameObject.GetComponent<CollectableController>().Touched();
            Invoke(nameof(SetCollectablePosition),3f);
        }

        if (col.gameObject.layer == offScreenLayer) {
            gameManager.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag(wallTag)) {
            playerController.isOnWall = false;
            playerController.yDirection *= -1;
            playerController.SetGravityScale(playerController.originalGravityScale);
        }
    }

    private void SetCollectablePosition() {
        gameManager.spawner.SetCollectablePosition();
    }
}