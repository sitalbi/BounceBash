
using UnityEngine;
using System;
using Random = System.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle, collectable, spike, bonus;
    [SerializeField] public float obstacleCoolDown;
    [SerializeField] private Vector2 horizontalInterval_Collectable, verticalInterval_Collectable;
    [SerializeField] private Vector2 horizontalInterval_Bonus, verticalInterval_Bonus;
    [SerializeField] private int maxSpawnRate;

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
        Invoke(nameof(SetBonusPosition), rn.Next(25,100));
    }


    void Update() {
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


    private void Spawn(GameObject gmObj) {
        Instantiate(gmObj, transform);
    }

    public void SetCollectablePosition() {
        int x = rn.Next((int)horizontalInterval_Collectable.x, (int)horizontalInterval_Collectable.y);
        int y = rn.Next((int)verticalInterval_Collectable.x, (int)verticalInterval_Collectable.y);
        collectablePosition = new Vector3(x, y);
        canSpawnCollectable = true;
    }
    
    public void SetBonusPosition() {
        int x = rn.Next((int)horizontalInterval_Bonus.x, (int)horizontalInterval_Bonus.y);
        int y = rn.Next((int)verticalInterval_Bonus.x, (int)verticalInterval_Bonus.y);
        bonusPosition = new Vector3(x, y);
        canSpawnBonus = true;
    }

}