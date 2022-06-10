using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle, collectable, spike;
    [SerializeField] public float obstacleCoolDown;

    private float spawnTime;
    private bool canSpawnCollectable, canSpawnSpike;

    void Start() {
        canSpawnSpike = true;
    }
    
    
    void Update()
    {
        if (Time.time >= spawnTime + obstacleCoolDown) {
            spawnTime = Time.time;
            Spawn(obstacle);
            canSpawnCollectable = true;
        }
        else if(canSpawnCollectable) {
            if (canSpawnSpike) {
                GameObject objectToSpawn; 
                canSpawnCollectable = false;
                Random rn = new Random();
                int choice = rn.Next(0, 2);
                if (choice == 0) {
                    objectToSpawn = collectable;
                }
                else {
                    objectToSpawn = spike;
                }
                if (rn.Next(0, 5) == 0) {
                    Instantiate(objectToSpawn, new Vector3(transform.position.x, transform.position.y + (obstacleCoolDown*obstacle.transform.localScale.y),0), Quaternion.identity);
                }
            }
            else {
                canSpawnCollectable = false;
                Random rn = new Random();
                if (rn.Next(0, 5) == 0) {
                    Instantiate(collectable, new Vector3(transform.position.x, transform.position.y + (obstacleCoolDown*obstacle.transform.localScale.y),0), Quaternion.identity);
                } 
            }
            
        }
    }

    private void Spawn(GameObject gameObject) {
        Instantiate(gameObject, transform);
    }
}
