using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed = 1.0f;
    public Vector3 defaultOffset = new Vector3(0,18,0);
    public Vector3 offset;
    private Vector3 targetPos;
    private bool playerAlive;

    void Update()
    {
        fearZoom();
        playerAlive = player.GetComponent<PlayerController>().isAlive();
    }

    void LateUpdate()
    {
        if (playerAlive)
        {
            //Sets camera position to that of player position + offset
            targetPos = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
        else
        {
            //If the player is dead, camera zooms down through the floor
            targetPos = player.transform.position + offset;
            targetPos.y = -1.5f;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
    }

    void fearZoom()
    {
        int zoom = GameObject.Find("Player").GetComponent<PlayerController>().getFear();
        zoom = zoom * 2;
        if(zoom > 15)
        {
            zoom = 15;
        }
        offset = new Vector3(defaultOffset.x ,defaultOffset.y-zoom, defaultOffset.z);
    }
}
