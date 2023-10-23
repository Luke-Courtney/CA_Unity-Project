using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;    //Navmesh agent component

    private Light eyeLight;

    private float health = 10.0f;

    private GameObject player;    //Player gameobject
    private float playerDistance; //How far is the player?
    public float detectionRange = 10;   //How far away does the enemy detect the player?
    private bool chasing;

    //Wander
    bool goingToPoint;
    float wanderRange = 15.0f; 
    Vector3 wanderPoint = new Vector3(999,999,999);

    void Start()
    {
        player = GameObject.Find("Player");
        eyeLight = GameObject.Find("EyeLight").GetComponent<Light>();
        chasing = false;
        eyes();
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

        //If player is close enough
        if(playerDistance < detectionRange)
        {
            //Moving NavMesh Agent
            agent.SetDestination(playerPos);

            chasing = true;
            goingToPoint = false;
            agent.speed = 10.5f;
            eyes();
        }
        else
        {
            chasing = false;
            wander();
            eyes();
        } 
    }

    //Sets eyelight state
    void eyes()
    {
        if(eyeLight != null)
        {
            if(agent.speed != 3)
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

    //Damage Enemy
    public void damage(float damage, float range)
    {
        health -= (damage*(10-range))*Time.deltaTime;

        if(health<=1)
        {
            Destroy(gameObject);
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
