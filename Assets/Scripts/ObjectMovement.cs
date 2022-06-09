using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private int movementSpeed;
    private Rigidbody2D rb;

    void Start() {
        movementSpeed = 1100;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = Vector2.down * movementSpeed * Time.deltaTime; 
    }
}
