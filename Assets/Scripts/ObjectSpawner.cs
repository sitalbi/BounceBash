using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject collectable;
    [SerializeField] private float obstacleCoolDown;

    private float spawnTime;
    private bool canSpawnCollectable;

    void Start()
    {
        
    }
    
    
    void Update()
    {
        if (Time.time >= spawnTime + obstacleCoolDown) {
            spawnTime = Time.time;
            Spawn(obstacle);
            canSpawnCollectable = true;
        }
        else if(canSpawnCollectable) {
            canSpawnCollectable = false;
            Random rn = new Random();
            if (rn.Next(0, 5) == 0) {
                Instantiate(collectable, new Vector3(transform.position.x, transform.position.y + (1.5f*obstacle.transform.localScale.y),0), Quaternion.identity);
            }
        }
    }

    private void Spawn(GameObject gameObject) {
        Instantiate(gameObject, transform);
    }
}
