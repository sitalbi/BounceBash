using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int jumpForce, movementSpeed, wallSpeed;

    private Rigidbody2D rb2D;

    private bool jumpPressed, canMove;

    [NonSerialized] public float originalGravityScale;
    [NonSerialized] public int xDirection, yDirection;
    [NonSerialized] public bool isOnWall;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        xDirection = 1;
        yDirection = 1;
        originalGravityScale = rb2D.gravityScale;
    }


    void Update() {
        if (Input.touchCount>0) {
            jumpPressed = true;
        }
    }

    void FixedUpdate() {
        if (jumpPressed) {
            rb2D.velocity = new Vector2(xDirection * movementSpeed * 50 * Time.deltaTime,
                50 * jumpForce * Time.deltaTime);
            jumpPressed = false;
        }

        if (isOnWall) {
            //Up or down depending on the wall
            rb2D.velocity = new Vector2(rb2D.velocity.x, yDirection  * wallSpeed * 50 * Time.deltaTime);
        }
    }

    public void SetGravityScale(float value) {
        if (value == 0) {
            rb2D.velocity = Vector2.zero;
        }

        rb2D.gravityScale = value;
    }
}