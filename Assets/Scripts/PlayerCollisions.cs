using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private int wallLayer, obstacleLayer, collectableLayer;
    private PlayerController playerController;
    
    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == wallLayer) {
            playerController.points++;
            playerController.isOnWall = true;
            playerController.xDirection *= -1;
            playerController.SetGravityScale(0);
        }

        if (col.gameObject.layer == obstacleLayer) {
            //Death
        }

        if (col.gameObject.layer == collectableLayer) {
            playerController.points++;
            Destroy(col.gameObject);
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
