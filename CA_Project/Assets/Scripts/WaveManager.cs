using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Enemy, health and battery prefabs
    public GameObject enemyPrefab; 
    public GameObject healthPrefab;
    public GameObject batteryPrefab;

    //Spawn parameters
    private float spawnRangeX = 49;
    private float spawnRangeZ = 49;

    private float startDelay = 5.0f;
    private float defaultEnemySpawnInterval = 10.0f;
    public float enemySpawnInterval = 10.0f;
    public float healthSpawnInterval = 45.0f;
    public float batterySpawnInterval = 30.0f;

    //Playercontroller
    private PlayerController playerController;

    //Time
    private float seconds;
    public float displaySeconds;

    // Start is called before the first frame update
    void Start()
    {
        //Find playercontroller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        //Start spawning enemies and health
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnInterval);
        InvokeRepeating("SpawnHealth", startDelay, healthSpawnInterval);
        InvokeRepeating("SpawnBattery", startDelay, batterySpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        //Update times
        seconds += Time.deltaTime;
        displaySeconds = Mathf.Round(seconds);

        //Update enemy spawn interval based on time into game
        //Every minute into the game reduces spawn interval by 1 second
        //To a minimum of 2 seconds.
        if(enemySpawnInterval > 2)
        {
            enemySpawnInterval = defaultEnemySpawnInterval - (seconds/60);
        }
    }

    public float GetTime()
    {
        return seconds;
    }

    //Spawns enemy within range
    void SpawnEnemy()
    {     
        //Only spawn new enemies if player is alive
        if(playerController.isAlive())
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX), 1.0f, Random.Range(-spawnRangeZ,spawnRangeZ));
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
        
    }

    //Spawns health pickups
    void SpawnHealth()
    {   
        //Only spawn new enemies if player is alive
        if(playerController.isAlive())
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX), 0.1f, Random.Range(-spawnRangeZ,spawnRangeZ));
            Instantiate(healthPrefab, spawnPos, healthPrefab.transform.rotation);
        }
        
    }

    //Spawns battery pickups
    void SpawnBattery()
    {   
        //Only spawn new enemies if player is alive
        if(playerController.isAlive())
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX), 0.1f, Random.Range(-spawnRangeZ,spawnRangeZ));
            Instantiate(batteryPrefab, spawnPos, batteryPrefab.transform.rotation);
        }
        
    }
}
