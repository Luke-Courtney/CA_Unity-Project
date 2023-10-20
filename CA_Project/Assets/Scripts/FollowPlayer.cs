using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed = 1.0f;
    public Vector3 defaultOffset = new Vector3(0,15,0);
    public Vector3 offset;
    private Vector3 targetPos;

    void Update()
    {
        fearZoom();
    }

    void LateUpdate()
    {
        //Sets camera position to that of player position + offset
        targetPos = player.transform.position + offset;   
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

        Debug.Log(offset);
    }

    void fearZoom()
    {
        int zoom = GameObject.Find("Player").GetComponent<PlayerController>().getFear();
        if(zoom > 8)
        {
            zoom = 8;
        }
        Debug.Log(zoom);
        offset = new Vector3(defaultOffset.x ,defaultOffset.y-zoom, defaultOffset.z);
    }
}
