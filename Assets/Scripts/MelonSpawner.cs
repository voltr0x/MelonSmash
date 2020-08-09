using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonSpawner : MonoBehaviour
{    
    public GameObject melon;
    [HideInInspector] GameObject spawnedMelon;

    public float minSpawnInterval = 3.0f;
    public float maxSpawnInterval = 6.0f;

    public float spawnTime;
    public float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        //It will start spawning at the very begining, can add a delay later
        spawnTime = 0;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount == 0)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnInterval) //Time to spawn again
            {
                SpawnMelonPrefab();
                spawnTime = 0; //Reset spawn time
                spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }

    void SpawnMelonPrefab()
    {
        //Spawns enemy at the location of SpawnPoint game object
        spawnedMelon = Instantiate(melon, transform.position, transform.rotation);
        spawnedMelon.transform.SetParent(this.transform);
    }
}