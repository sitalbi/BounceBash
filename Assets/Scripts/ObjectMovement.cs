using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public int movementSpeed;

    void Start() {
        movementSpeed = 10;
        Destroy(gameObject, 7f);
    }

    void Update() {
        transform.position += Vector3.down * (movementSpeed * Time.deltaTime);
    }
}