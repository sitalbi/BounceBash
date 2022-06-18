using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText, loseText;
    [SerializeField] private GameObject player, spikeSpawner1, spikeSpawner2, panel;
    [SerializeField] public ObjectSpawner spawner;
    [SerializeField] private float minCoolDown, coolDownStep;
    [SerializeField] public int collectablePoints;
    [SerializeField] private ObjectMovement objectMovement;
    
    [NonSerialized] public int score;

    private bool coolDownChanged, isDead;

    void Start() {
        score = 0;
        loseText.text = "";
    }


    void Update() {
        scoreText.text = score.ToString();
        if (score != 0) {
            if (score % 10 == 0) {
                if (spawner.obstacleCoolDown > minCoolDown && !coolDownChanged) {
                    spawner.obstacleCoolDown -= coolDownStep;
                    coolDownChanged = true;
                    if (spawner.spawnRate > 2) {
                        spawner.spawnRate--;
                    }
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

        if (isDead) {
            if (Input.touchCount>0) {
                SceneManager.LoadScene("Scenes/MainMenu");
            }
        }
    }

    public void Death() {
        if (PlayerPrefs.HasKey("HighScore")) {
            if (score > PlayerPrefs.GetInt("HighScore")) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Destroy(player);
        panel.SetActive(true);
        loseText.text = "Lost !";
        Invoke(nameof(Dead), 0.7f);
    }

    private void Dead() {
        isDead = true;
    }
}