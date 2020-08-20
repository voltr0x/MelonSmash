using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private GameManager gameManager;
    
    public enemy_main spawnPrefab;
    public enemy_target initialMoveTarget;

    public float minSpawnInterval = 3.0f;
    public float maxSpawnInterval = 6.0f;

    public float spawnTime;
    public float spawnInterval;

    //For waves
    public enum SpawnState {SPAWNING, COOLDOWN, WAITING};
    
    [System.Serializable]
    public class WaveHandler
    {
        public string name;
        public int count;
        public float rate;
    }

    public WaveHandler[] waves;
    private int waveIndex = 0;
    public float timeBetweenWaves = 5.0f;
    public float waveCoolDown;
    private float searchCountdown = 1.0f;

    private SpawnState state = SpawnState.COOLDOWN;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //Can have a random number generated
        waveCoolDown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if wave is cleared
        if (state == SpawnState.WAITING)
        {
            if (!EnemyAlive())
            {
                Debug.Log("Enemies dead");
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (!gameManager.gameOver)
        {
            Debug.Log("Wave begins");
            if(waveCoolDown <= 0)
            {
                //Prevents from spawining at every frame
                if(state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[waveIndex]));
                }
            }
            else
            {
                //Makes the cooldown relevant with time
                waveCoolDown -= Time.deltaTime;
            }
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave cleared!");
        state = SpawnState.COOLDOWN;
        waveCoolDown = timeBetweenWaves;

        if (waveIndex + 1 > waves.Length - 1)
        {
            //Level is completed
            Debug.Log("Level complete!");
            gameManager.BeatLevel();
        }
        else
        {
            //Wave clear screen
            waveIndex++;
        }
    }

    //Check is enemies are alive
    bool EnemyAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(WaveHandler spawnWave)
    {
        state = SpawnState.SPAWNING;
        for (int i=0; i<spawnWave.count; i++)
        {
            SpawnEnemyPrefab();
            yield return new WaitForSeconds(1f / spawnWave.rate);
            Debug.Log("Still in loop");
        }

        Debug.Log("Waiting");
        state = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemyPrefab()
    {
        //Spawns enemy at the location of SpawnPoint game object
        enemy_main spawnedEnemy = Instantiate(spawnPrefab, transform.position, transform.rotation);
        spawnedEnemy.where_to_go = initialMoveTarget;
    }
}