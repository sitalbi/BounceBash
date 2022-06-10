using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private int movementSpeed;

    void Start() {
        movementSpeed = 5;
    }

    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime; 
    }
}
