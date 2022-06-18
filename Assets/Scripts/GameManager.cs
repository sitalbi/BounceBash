using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText, loseText;
    [SerializeField] private GameObject player, spikeSpawner1, spikeSpawner2, panel, continueButton;
    [SerializeField] public ObjectSpawner spawner;
    [SerializeField] private float minCoolDown, coolDownStep;
    [SerializeField] public int collectablePoints;
    [SerializeField] private ObjectMovement objectMovement;
    
    [NonSerialized] public int score;

    private bool coolDownChanged, isDead;

    void Start() {
        score = 0;
        loseText.text = "";
        continueButton.SetActive(false);
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
            /*if (Input.touchCount>0) {
                SceneManager.LoadScene("Scenes/MainMenu");
            }*/
            
            if (Input.GetButtonDown("Jump")) {
                if (PlayerPrefs.HasKey("HighScore")) {
                    if (score > PlayerPrefs.GetInt("HighScore")) {
                        PlayerPrefs.SetInt("HighScore", score);
                    }
                }
                else {
                    PlayerPrefs.SetInt("HighScore", score);
                }
                SceneManager.LoadScene("Scenes/MainMenu");
            }
        }
    }

    public void Death() {
        player.GetComponent<PlayerController>().Death();
        panel.SetActive(true);
        continueButton.SetActive(true);
        loseText.text = "Lost !";
        isDead = true;
        Time.timeScale = 0;
    }

    private void Dead() {
        isDead = true;
    }

    public void Continue() {
        player.GetComponent<PlayerController>().Respawn();
        panel.SetActive(false);
        continueButton.SetActive(false);
        loseText.text = "";
        Time.timeScale = 1;
        isDead = false;
    }
}