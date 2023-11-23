using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Light indicator;

    private float lastTeleport = 0;
    private float lightTeleportInterval = 300.0f;   //5 minutes

    private float indicatorIntensity;

    private AudioSource audioSource;
    public AudioClip teleportSound;

    //Has the ability been unlocked?
    //Public for testing
    public bool collected = false;

    //Player
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("Teleport Indicator").GetComponent<Light>();
        lastTeleport = 15;

        //Getting Audio Source component
        audioSource = GetComponent<AudioSource>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive())
        {
            if (collected)
            {
                UpdateIndicatorIntensity();

                //Teleport
                if (Input.GetKeyDown(KeyCode.T) && lastTeleport > lightTeleportInterval)
                {
                    //Activate
                    lastTeleport = 0;
                    indicatorIntensity = 0;
                    activateTeleport();
                    audioSource.PlayOneShot(teleportSound, 0.7f);
                }
                else
                {
                    //Continue counting time since last use
                    lastTeleport += Time.deltaTime;
                }
            }
            else
            {
                indicator.intensity = 0;
                lastTeleport = 0;
            }
        }
        else
        {
            indicator.intensity = 0;
            lastTeleport = 0;
        }

    }


    void UpdateIndicatorIntensity()
    {
        if (indicatorIntensity < 5) { indicatorIntensity = lastTeleport / 3; }
        indicator.intensity = indicatorIntensity;
    }

    public void Collected(bool isCollected)
    {
        collected = isCollected;
    }

    void activateTeleport()
    {
        //Generate random point
        Vector3 randPoint = new Vector3(Random.Range(-100, 100), 2, Random.Range(-100, 100)); 

        //Move point to navmesh
        randPoint = NearestNavmeshPoint(randPoint);

        //Move point above ground
        randPoint.y += 1;

        //Move the player
        GameObject.Find("Player").transform.position = randPoint;
    }

    //Returns nearesst point on navmesh to given point
    Vector3 NearestNavmeshPoint(Vector3 point)
    {
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(point, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas))
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
