using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float speed = 10.0f;
    public float maxSpeed = 10.0f;
    public float turnSpeed;

    //Lives
    private int lives;
    private bool alive;

    float hitTime = 1;
    float hitTimer = 0;
    bool invunerable = false;

    private Light lifeLight1;
    private Light lifeLight2; 
    private Light lifeLight3; 

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
        //Lives
        lives = 3;
        alive = true;

        lifeLight1 = GameObject.Find("Life Light 1").GetComponent<Light>();
        lifeLight2 = GameObject.Find("Life Light 2").GetComponent<Light>();
        lifeLight3 = GameObject.Find("Life Light 3").GetComponent<Light>();

        //Flashlight component
        flashlight = GameObject.Find("Flashlight").GetComponent<Light>();

        //Enemies
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
        if(alive)
        {
            movement();
            faceMouse();

            // Increment the hit timer
            hitTimer += Time.deltaTime;

            if(hitTimer > hitTime){
                invunerable = false;
            }
        }
    
        fearLevel();
    }

    void LateUpdate()
    {   
        //Late updating fear value to 0 so that the camera zoom script has time to read in the fear value from getFear()
        fear = 0;
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
    }

    public int getFear()
    {
        return fear;
    }

    public bool isAlive()
    {
        return alive;
    }

    //Remove 1 life and turn off light
    void damage()
    {
        if(lives>0)
        {
            lives--;
            switch(lives) 
            {
                case 2:
                    lifeLight3.intensity = 0;
                    break;
                case 1:
                    lifeLight2.intensity = 0;
                    break;
                case 0:
                    lifeLight1.intensity = 0;
                    flashlight.intensity = 0;
                    alive = false;
                    Debug.Log("You are dead");
                    break;
            }
        }
    }

    void heal()
    {
        if(lives>=0)
        {
            lives++;
            switch(lives) 
            {
                case 1:
                    lifeLight1.intensity = 1.91f;
                    break;
                case 2:
                    lifeLight2.intensity = 1.91f;
                    break;
                case 3:
                    lifeLight3.intensity = 1.91f;
                    break;
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {   
        //If collided with enemy, and it isnt null
        if(other.GetComponent<Collider>() != null && other.GetComponent<Collider>().gameObject.CompareTag("Enemy"))
        {
            if(!invunerable){
                damage();
            }

            hitTimer = 0;
            invunerable = true;
        }

        //If collided with health pickup, and it isnt null
        if(other.GetComponent<Collider>() != null && other.GetComponent<Collider>().gameObject.CompareTag("Health"))
        {
            if(lives<3){
                heal();
                hitTimer = 0;
                invunerable = true; 

                //Turning off health pickup after use
                Destroy(other.gameObject);
            }
        }
    }
}