using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // where it randomly checks to spawn the enemy
    [SerializeField] private GameObject enemy; // the enemy that it spawns
    [SerializeField] private float spawnCD; // how fast the enemy spawns
    private float spawnCDTimer;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnCDTimer)
        {
            spawnCDTimer = Time.time + spawnCD;
            if (spawnCD > 1)
                spawnCD--;
            // spawn enemy randomly at any of the children points
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}