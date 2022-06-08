using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private int wallLayers;
    private PlayerController playerController;
    
    void Start() {
        playerController = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == wallLayers) {
            playerController.isOnWall = true;
            playerController.xDirection *= -1;
            playerController.SetGravityScale(0);
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.layer == wallLayers) {
            playerController.isOnWall = false;
            playerController.yDirection *= -1;
            playerController.SetGravityScale(playerController.originalGravityScale);
        }
    }
}
