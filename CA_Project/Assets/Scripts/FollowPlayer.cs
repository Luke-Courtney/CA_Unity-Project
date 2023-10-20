using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed = 1.0f;
    public Vector3 offset = new Vector3(0,8,-15);
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Sets camera position to that of player position + offset
        targetPos = player.transform.position + offset;   
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}
