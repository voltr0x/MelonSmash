using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    /* public bool enemySpawner; */
    public enemy_main spawnPrefab;
    public enemy_target initialMoveTarget;

    public float minSpawnInterval = 3.0f;
    public float maxSpawnInterval = 6.0f;

    public float spawnTime;
    public float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        //It will start spawning at the very begining, can add a delay later
        spawnTime = Time.time;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime >= spawnInterval) //Time to spawn again
        {
            /*if (enemySpawner) */
            SpawnEnemyPrefab();
            /* else if (this.transform.childCount == 0)
            {
                SpawnMelonPrefab();
            } */

            spawnTime = Time.time; //Reset spawn time
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnEnemyPrefab()
    {
        //Spawns enemy at the location of SpawnPoint game object
        Instantiate(spawnPrefab, transform.position, transform.rotation);
        spawnPrefab.where_to_go = initialMoveTarget;
    }
}
