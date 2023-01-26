
using UnityEngine;
using System;
using Random = System.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle, collectable, spike, bonus;
    [SerializeField] public float obstacleCoolDown;
    [SerializeField] private Vector2 horizontalIntervalCollectable, verticalIntervalCollectable;
    [SerializeField] private Vector2 horizontalIntervalBonus, verticalIntervalBonus;
    [SerializeField] private int maxSpawnRate;
    [SerializeField] private GameManager gameManager;

    [NonSerialized] public int spawnRate;
    [NonSerialized] public bool scoreMoreThan10;

    private float spawnTime;
    private bool canSpawnCollectable, canSpawnSpike, canSpawnBonus;
    private Vector3 collectablePosition, bonusPosition;
    private Random rn;

    void Start() {
        rn = new Random();
        spawnRate = maxSpawnRate;
        Invoke(nameof(SetCollectablePosition), 5f);
        Invoke(nameof(SetBonusPosition), rn.Next(25,50));
    }


    void Update() {
        if (gameManager.inGame)
        {
            if (Time.time >= spawnTime + obstacleCoolDown) {
                spawnTime = Time.time;
                Spawn(obstacle);
                if (scoreMoreThan10) {
                    canSpawnSpike = true;
                }
            }
            else if (canSpawnSpike) {
                canSpawnSpike = false;
            
                if (rn.Next(0, 5) == 0) {
                    Instantiate(spike, new Vector3(transform.position.x, transform.position.y + (obstacleCoolDown * obstacle.transform.localScale.y), 0), Quaternion.identity);
                }
            }
        
            if(canSpawnCollectable) {
                canSpawnCollectable = false;
                Instantiate(collectable, collectablePosition, Quaternion.identity);
            }
        
            if(canSpawnBonus) {
                canSpawnBonus = false;
                Instantiate(bonus, bonusPosition, Quaternion.identity);
            }
        }
        
    }


    private void Spawn(GameObject gmObj) {
        Instantiate(gmObj, transform);
    }

    public void SetCollectablePosition() {
        int x = rn.Next((int)horizontalIntervalCollectable.x, (int)horizontalIntervalCollectable.y);
        int y = rn.Next((int)verticalIntervalCollectable.x, (int)verticalIntervalCollectable.y);
        collectablePosition = new Vector3(x, y);
        canSpawnCollectable = true;
    }
    
    public void SetBonusPosition() {
        int x = rn.Next((int)horizontalIntervalBonus.x, (int)horizontalIntervalBonus.y);
        int y = rn.Next((int)verticalIntervalBonus.x, (int)verticalIntervalBonus.y);
        bonusPosition = new Vector3(x, y);
        canSpawnBonus = true;
    }

}