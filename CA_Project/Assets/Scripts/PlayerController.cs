using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float speed = 10.0f;
    public float maxSpeed = 10.0f;
    public float turnSpeed;

    //Flashlight
    private Light flashlight;

    //All enemies in scene
    public GameObject[] enemiesArray;
    List<GameObject> enemiesList;

    //Fear level
    private int fear = 0;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GameObject.Find("Flashlight").GetComponent<Light>();

        //Finding all enemies in scene with array
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesList = new List<GameObject>();

        //Adding all enemies in scene to list
        for(int i=0; i<enemiesArray.Length; i++)
        {
            enemiesList.Add(enemiesArray[i]);
        }
    }

    int i = 1;
    // Update is called once per frame
    void Update()
    {
        movement();
        faceMouse();
        fearLevel();
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

    //Loops through list and deletes null objects
    void updateEnemyList()
    {
        for(int i=0; i<enemiesList.Count; i++)
            {
                if(enemiesList[i] == null)
                {
                    enemiesList.RemoveAt(i);
                }
            }
    }

    //Determines how many enemies are chasing
    void fearLevel()
    {
        updateEnemyList();

        //Looping through list of all enemies
        if(enemiesList.Count != 0)
        { 
            for(int i=0; i<enemiesList.Count; i++)
            {
                if(enemiesList[i].GetComponent<EnemyController>().isChasing())
                {
                    fear++;
                }
            }
        }

        Debug.Log("Fear: " + fear);

        fear = 0;
    }
}