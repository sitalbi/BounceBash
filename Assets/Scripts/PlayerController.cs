using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int jumpForce, movementSpeed;

    private Rigidbody2D rb2D;
    
    private bool jumpPressed, canMove;

    [NonSerialized] public int direction;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            jumpPressed = true;
        }
    }

    void FixedUpdate() {
        
        if (jumpPressed) {
            rb2D.velocity = new Vector2(direction * movementSpeed, 10*jumpForce * Time.deltaTime);
            jumpPressed = false;
        }
    }
}
