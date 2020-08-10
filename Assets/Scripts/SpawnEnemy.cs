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

   //private GameObject target1 = GameObject.FindGameObjectWithTag("Target 1");
   //private GameObject target2 = GameObject.FindGameObjectWithTag("Target 2");

    private int waveIndex = 1;
    public float waveMultiplier;
    private float enemiesToSpawn;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //It will start spawning at the very begining, can add a delay later
        spawnTime = Time.time;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        /*while (waveIndex < 4)
        {
            Debug.Log("1st Loop started");
            enemiesToSpawn = waveIndex * waveMultiplier;
            Debug.Log("No. of enemies = " + enemiesToSpawn);

            while (enemiesToSpawn != 0)
            {
                Debug.Log("2nd Loop started");
                if (Time.time - spawnTime >= spawnInterval) //Time to spawn
                {
                    SpawnEnemyPrefab();
                    Debug.Log("Spawn enemy!!");

                    spawnTime = Time.time; //Reset spawn time
                    spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
                    Debug.Log("Time resets");
                }

                enemiesToSpawn--;
                Debug.Log("Enemies left = " + enemiesToSpawn);
            }

            waveIndex++;
            Debug.Log("waveIndex = " + waveIndex);
        }*/
        if (!gameManager.gameOver)
        {
            if (Time.time - spawnTime >= spawnInterval) //Time to spawn again
            {
                SpawnEnemyPrefab();

                spawnTime = Time.time; //Reset spawn time
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }

    void SpawnEnemyPrefab()
    {
        //Spawns enemy at the location of SpawnPoint game object
        enemy_main spawnedEnemy = Instantiate(spawnPrefab, transform.position, transform.rotation);

       //Determine the spawn location of enemy
        /*float distance = Vector3.Distance(spawnPrefab.transform.position, target1.transform.position);
        if (distance == 0)
        {
            //spawnPrefab.where_to_go = initialMoveTarget;
        }
        else
        {
            spawnPrefab.where_to_go =  target2;
        }*/

        spawnedEnemy.where_to_go = initialMoveTarget;
    }
}
