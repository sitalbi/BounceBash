using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText, loseText;
    [SerializeField] private GameObject player, spikeSpawner1, spikeSpawner2, panel;
    [SerializeField] public ObjectSpawner spawner;
    [SerializeField] private float minCoolDown, coolDownStep;
    [SerializeField] public int collectablePoints;
    
    [NonSerialized] public int score;

    private bool coolDownChanged;

    void Start() {
        score = 0;
        loseText.text = "";
    }


    void Update() {
        scoreText.text = score.ToString();

        if (score != 0 && score % 10 == 0) {
            if (spawner.obstacleCoolDown > minCoolDown && !coolDownChanged) {
                spawner.obstacleCoolDown -= coolDownStep;
                coolDownChanged = true;
                if (spawner.spawnRate > 2) {
                    spawner.spawnRate--;
                }
            }
            if (score > 35) {
                spikeSpawner1.SetActive(true);
                spikeSpawner2.SetActive(true);
            }
        }
        else {
            coolDownChanged = false;
        }
    }

    public void Death() {
        Destroy(player);
        Time.timeScale = 0;
        panel.SetActive(true);
        loseText.text = "Lose !";
    }
}