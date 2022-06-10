using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private int wallLayer, obstacleLayer, collectableLayer, offScreenLayer;
    [SerializeField] private GameManager gameManager;
    
    private PlayerController playerController;
    
    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == wallLayer) {
            gameManager.score++;
            playerController.isOnWall = true;
            playerController.xDirection *= -1;
            playerController.SetGravityScale(0);
        }

        if (col.gameObject.layer == obstacleLayer) {
            //Death
            gameManager.Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == collectableLayer) {
            gameManager.score++;
            Destroy(col.gameObject);
        }
        
        if (col.gameObject.layer == offScreenLayer) {
            gameManager.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.layer == wallLayer) {
            playerController.isOnWall = false;
            playerController.yDirection *= -1;
            playerController.SetGravityScale(playerController.originalGravityScale);
        }
    }
}
