using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    public int slideNum;
    private int lastSlideNum = 3;

    private int speed = 1;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        slideNum = 0;
        targetPos = new Vector3(slideNum * 21, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    //Handle input for camera movement
    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.A) && slideNum > 0)
        {
            slideNum--;
        }

        if (Input.GetKeyDown(KeyCode.D) && slideNum < lastSlideNum)
        {
            slideNum++;
        }
    }

    void MoveCamera()
    {
        Vector3 targetPos = new Vector3(slideNum*21, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}
