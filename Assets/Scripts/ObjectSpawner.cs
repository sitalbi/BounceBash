using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle, collectable, spike;
    [SerializeField] public float obstacleCoolDown;
    [SerializeField] private Vector2 horizontalInterval, verticalInterval;
    [SerializeField] private int maxSpawnRate;

    [NonSerialized] public int spawnRate;

    private float spawnTime;
    private bool canSpawnCollectable, canSpawnSpike, scoreMoreThan10;
    private Vector3 collectablePosition;
    private Random rn;

    void Start() {
        rn = new Random();
        spawnRate = maxSpawnRate;
        Invoke(nameof(SetCollectablePosition), 5f);
    }


    void Update() {
        if (Time.time >= spawnTime + obstacleCoolDown) {
            spawnTime = Time.time;
            Spawn(obstacle);
            canSpawnSpike = true;
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
    }


    private void Spawn(GameObject gameObject) {
        Instantiate(gameObject, transform);
    }

    public void SetCollectablePosition() {
        canSpawnCollectable = true;
        int x = rn.Next((int)horizontalInterval.x, (int)horizontalInterval.y);
        int y = rn.Next((int)verticalInterval.x, (int)verticalInterval.y);
        collectablePosition = new Vector3(x, y);
    }

}