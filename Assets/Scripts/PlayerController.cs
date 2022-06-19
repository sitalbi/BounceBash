using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int jumpForce, movementSpeed, wallSpeed;
    [SerializeField] private Sprite defaultSprite;

    private Rigidbody2D rb2D;

    private bool jumpPressed, canMove;

    private Vector3 startPos;

    [NonSerialized] public float originalGravityScale;
    [NonSerialized] public int xDirection, yDirection;
    [NonSerialized] public bool isOnWall;

    void Start() {
        startPos = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        xDirection = 1;
        yDirection = 1;
        originalGravityScale = rb2D.gravityScale;
        rb2D.gravityScale = 0;
    }


    void Update() {
        if (Input.touchCount>0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                jumpPressed = true;
            }
        }


        /*if (Input.GetButtonDown("Jump")) {
            jumpPressed = true;
        }*/
    }

    void FixedUpdate() {
        if (jumpPressed) {
            if (!isOnWall && rb2D.gravityScale == 0) {
                rb2D.gravityScale = originalGravityScale;
            }
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

    public void Death() {
        GetComponent<SpriteRenderer>().sprite = null;
        rb2D.velocity = Vector2.zero;
    }
    
    public void Respawn() {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        transform.position = startPos;
        xDirection = 1;
        yDirection = 1;
        rb2D.gravityScale = 0;
        isOnWall = false;
        jumpPressed = false;
        canMove = false;
    }
}