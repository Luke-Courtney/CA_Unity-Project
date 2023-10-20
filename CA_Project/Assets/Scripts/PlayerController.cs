using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float maxSpeed = 10.0f;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        faceMouse();
    }

    void movement()
    {
        //Gets input on vertical and horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        //Move the player
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput ,Space.World);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput ,Space.World);

        //Keeps max speed in check
        if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
         {
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
         }
    }

    //Find the angle between two points
    //https://discussions.unity.com/t/make-a-player-model-rotate-towards-mouse-location/125354
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
		return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

    //Face player towards mouse
    void faceMouse()
    {
        //Get the Screen positions of the object
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		
		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		
		//Get the angle between the points
		float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Adjust angle
        angle = (angle*-1)-90;

		//Rotate
		transform.rotation =  Quaternion.Euler (new Vector3(0f,angle,0f));
    }
}
