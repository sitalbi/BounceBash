using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField] public GameObject canvas;
    [SerializeField] private AudioSource coinSound;
    void Start()
    {
        canvas.SetActive(false);
    }

    public void Touched() {
        Destroy(gameObject, 0.85f);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        canvas.SetActive(true);
        coinSound.Play(0);
    }
}
