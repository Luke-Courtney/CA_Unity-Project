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
        }
        else
        {
            chasing = false;
        }

        eyes();
    }

    //Sets eyelight state
    void eyes()
    {
        if(eyeLight != null)
        {
            if(chasing)
            {
                Debug.Log("Tracking");
                eyeLight.spotAngle = 45f;
                eyeLight.intensity = 5f;
            }
            else
            {
                Debug.Log("Not Tracking");
                eyeLight.spotAngle = 125f;
                eyeLight.intensity = 2.5f;
            }
        }
    }

    //Is the enemy chasing the player
    public bool isChasing()
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
}
