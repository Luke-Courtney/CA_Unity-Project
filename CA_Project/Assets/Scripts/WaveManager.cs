using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Number of enemies spawned
    public int enemyCount = 1;

    //Enemy, health and battery prefabs
    public GameObject enemyPrefab; 
    public GameObject healthPrefab;
    public GameObject batteryPrefab;

    //Special Enemies
    public GameObject largeEnemyPrefab;
    public GameObject smallEnemyPrefab;
    private int specialEnemyInterval = 20;
    private bool decrementSpecialSpawnInterval; //Should the special spawn interval be reduced this turn?

    //Spawn parameters
    private float spawnRangeX = 99;
    private float spawnRangeZ = 99;

    private float startDelay = 5.0f;

    //Enemy
    private float defaultEnemySpawnInterval = 8.0f;
    public float enemySpawnInterval = 8.0f;
    public float minEnemySpawnInterval = 0.125f;

    //Pickups
    public float healthSpawnInterval = 60.0f;
    public float batterySpawnInterval = 20.0f;

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

        decrementSpecialSpawnInterval = false;
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
        if(enemySpawnInterval > minEnemySpawnInterval)
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
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 1.0f, Random.Range(-spawnRangeZ, spawnRangeZ));
            spawnPos = NearestNavmeshPoint(spawnPos);
            spawnPos.y += 2.5f;

            //Check if large enemy spawn interval has been reached and spawn appropriate enemy
            if (enemyCount % specialEnemyInterval != 0)
            {
                Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
            }
            else
            {
                //Which special type to spawn?
                //0 - Large
                //1 - Small
                int randNum = Random.Range(0, 2);

                switch(randNum)
                {
                    case 0: //Large
                        Instantiate(largeEnemyPrefab, spawnPos, enemyPrefab.transform.rotation);
                        break;

                    case 1: //Small
                        Instantiate(smallEnemyPrefab, spawnPos, enemyPrefab.transform.rotation);
                        Instantiate(smallEnemyPrefab, spawnPos, enemyPrefab.transform.rotation);
                        Instantiate(smallEnemyPrefab, spawnPos, enemyPrefab.transform.rotation);
                        break;
                }

                //Should the special enemy spawn interval be decremented this time?
                //Decrements interval every second spawn
                if(decrementSpecialSpawnInterval)
                {
                    specialEnemyInterval--;
                    decrementSpecialSpawnInterval = false;
                }
                else
                {
                    decrementSpecialSpawnInterval = true;
                }
            }

            enemyCount++;
        }
        
    }

    //Spawns health pickups
    void SpawnHealth()
    {   
        //Only spawn new enemies if player is alive
        if(playerController.isAlive())
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX,spawnRangeX), 0.1f, Random.Range(-spawnRangeZ,spawnRangeZ));
            spawnPos = NearestNavmeshPoint(spawnPos);
            spawnPos.y += 0.05f;
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
            spawnPos = NearestNavmeshPoint(spawnPos);
            spawnPos.y += 0.05f;
            Instantiate(batteryPrefab, spawnPos, batteryPrefab.transform.rotation);
        }
        
    }

    //Returns nearest point on navmesh to Vector3 Point
    Vector3 NearestNavmeshPoint(Vector3 point)
    {
        UnityEngine.AI.NavMeshHit hit;
        if(UnityEngine.AI.NavMesh.SamplePosition(point, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.Log("No closest point on Navmesh found");
            return point;
        }
    }
}
