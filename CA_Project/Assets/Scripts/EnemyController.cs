using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;    //Navmesh agent component

    private Light eyeLight;

    public float health = 10.0f;

    private GameObject player;    //Player gameobject
    private float playerDistance; //How far is the player?
    public float detectionRange = 10;   //How far away does the enemy detect the player?
    private bool chasing;

    //setDestination timer
    private float destTimer = 0.5f;

    //Wander
    bool goingToPoint;
    float wanderRange = 15.0f; 
    Vector3 wanderPoint = new Vector3(999,999,999);

    //StatTracker
    private StatTracker stats;

    void Start()
    {
        player = GameObject.Find("Player");
        eyeLight = GameObject.Find("EyeLight").GetComponent<Light>();
        chasing = false;
        //eyes(); 

        stats = GameObject.Find("StatTracker").GetComponent<StatTracker>();
    }

    void Update()
    {
        hunt();
    }

    //Determines if player is close enough, and chases them if they are 
    void hunt()
    {
        //Find player location
        Vector3 playerPos = player.transform.position;

        //How far away is the player?
        float playerDistance = Vector3.Distance (transform.position, player.transform.position);

        //If player is close enough and within LOS
        if (playerDistance < detectionRange)
        {
            //Only casting ray if player is within detection range to save performance
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;  //Direction to player
            RaycastHit hit;
            Physics.Raycast(transform.position, playerDirection, out hit);

            if (hit.transform.CompareTag("Player"))
            {
                //Moving NavMesh Agent
                //Delay between setDestination for performance
                if (destTimer <= 0.5f)
                {
                    agent.SetDestination(playerPos);
                    destTimer = 0;
                }
                else
                {
                    destTimer += Time.deltaTime;
                }

                chasing = true;
                goingToPoint = false;
                agent.speed = 10.5f;
                //eyes();
            }
            else
            {
                chasing = false;
                wander();
            }
        }
        else
        {
            chasing = false;
            wander();
            //eyes();
        } 
    }

    //Sets eyelight state
    void eyes()
    {
        if(eyeLight != null)
        {
            if(chasing)
            {
                eyeLight.spotAngle = 45f;
                eyeLight.intensity = 5f;
            }
            else
            {  
                eyeLight.spotAngle = 125f;
                eyeLight.intensity = 2.5f;
            }
        }
    }

    //Is the enemy chasing the player
    public bool IsChasing()
    {
        return chasing;
    }

    public float getHealth()
    {
        return health;
    }

    //Damage Enemy
    public void damage(float damage, float range)
    {
        health -= (damage*(10-range))*Time.deltaTime;

        if(health<=1)
        {
            Destroy(gameObject);
            stats.AddKill();
        }
    }

    void wander()
    {
        if(Vector3.Distance(transform.position, wanderPoint)< 1.0f || wanderPoint == new Vector3(999,999,999))
        {
            //Generating new wanderPoint
            wanderPoint = new Vector3(transform.position.x + Random.Range(-wanderRange,wanderRange), 0, transform.position.z + Random.Range(-wanderRange,wanderRange));
            wanderPoint = NearestNavmeshPoint(wanderPoint);

            //Going to point
            agent.speed = 3;
            agent.SetDestination(wanderPoint);
            goingToPoint = true;
        }
        else
        {
            if(!chasing)
            {
                agent.speed = 3;
            }
            agent.SetDestination(wanderPoint);
        }
    }

    //Returns nearest point on navmesh to Vector3 Point
    Vector3 NearestNavmeshPoint(Vector3 point)
    {
        UnityEngine.AI.NavMeshHit hit;
        if(UnityEngine.AI.NavMesh.SamplePosition(point, out hit, 15.0f, UnityEngine.AI.NavMesh.AllAreas))
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
