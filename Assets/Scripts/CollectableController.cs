using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CollectableController : MonoBehaviour
{
    [SerializeField] public GameObject canvas, text;
    [SerializeField] private AudioSource coinSound, switchSound;
    [SerializeField] private string collectableTag, bonusTag;
    
    void Start()
    {
        if(canvas != null) canvas.SetActive(false);
    }

    public void Touched() {
        Destroy(gameObject, 0.85f);
        gameObject.GetComponent<ParticleSystem>().Play();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (gameObject.CompareTag(collectableTag))
        {
            coinSound.Play(0);
            canvas.SetActive(true);
            LeanTween.scale(text, Vector3.zero, 0.85f).setEase(LeanTweenType.easeInElastic);
        }
        else if(gameObject.CompareTag(bonusTag))
        {
            switchSound.Play(0);
            GetComponent<Animator>().enabled = true;
        }
    }
}
