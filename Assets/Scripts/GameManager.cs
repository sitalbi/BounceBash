using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject player;
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private float minCoolDown, coolDownStep;
    [NonSerialized] public int score;
    
    private bool coolDownChanged;

    void Start() {
        score = 0;
    }

    
    void Update() {
        scoreText.text = score.ToString();
        
        if (score != 0 && score % 10 == 0) {
            if (spawner.obstacleCoolDown > minCoolDown && !coolDownChanged) {
                spawner.obstacleCoolDown -= coolDownStep;
                coolDownChanged = true;
            }
        }
        else {
            coolDownChanged = false;
        }
    }

    public void Death() {
        Destroy(player);
        Time.timeScale = 0;
    }
}
