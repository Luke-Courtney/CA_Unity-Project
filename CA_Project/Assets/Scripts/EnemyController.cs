using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;    //Navmesh agent component

    private Light eyeLight;

    private GameObject player;    //Player gameobject
    private float playerDistance; //How far is the player?
    private bool chasing;         //Is the enemy chasing the player?
    public float detectionRange = 5;

    void Start()
    {
        player = GameObject.Find("Player");
        eyeLight = GameObject.Find("EyeLight").GetComponent<Light>();
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

                //Setting eyeLight narrower and more intense
                eyeLight.spotAngle = 45f;
                eyeLight.intensity = 5f;
        }
        else
        {
            chasing = false;
            eyeLight.spotAngle = 125f;
            eyeLight.intensity = 1.5f;
        }
    }
}
