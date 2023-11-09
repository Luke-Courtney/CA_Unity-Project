using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Audio
    private AudioSource audioSource;

    //Heartbeats
    private int heartbeatLevel = 1;
    public AudioClip heartbeat_1;
    public AudioClip heartbeat_2;
    public AudioClip heartbeat_3;
    public AudioClip heartbeat_4;
    public AudioClip heartbeat_5;

    private int currentFear = 0;

    //StatTracker
    private StatTracker stats;

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

        //Getting Audio Source component
        audioSource = GetComponent<AudioSource>();

        audioSource.Stop();
        audioSource.clip = heartbeat_1;
        audioSource.Play();

        //StatTracker
        stats = GameObject.Find("StatTracker").GetComponent<StatTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        utilityInput();

        if (alive)
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

    void utilityInput()
    { 
        if(Input.GetKeyDown(KeyCode.Escape  ))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void movement()
    {
        //Gets input on vertical and horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        //Move the player
        transform.Translate(forwardInput * speed * Time.deltaTime * Vector3.forward ,Space.World);
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right ,Space.World);


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
        //Finding all enemies in scene with array
        enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");

        //Clearing list
        enemiesList.Clear();

        //Add all enemies from array to list
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            enemiesList.Add(enemiesArray[i]);
        }

        for (int i=0; i<enemiesList.Count; i++)
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
                if(enemiesList[i].GetComponent<EnemyController>().IsChasing())
                {
                    fear++;
                    SetHeartbeat(fear);
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
                    stats.AddDeath();

                    //Save stats
                    stats.SaveStats();
                    break;
            }
        }
    }

    void Heal()
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
                Heal();
                hitTimer = 0;
                invunerable = true; 

                //Turning off health pickup after use
                Destroy(other.gameObject);

                stats.AddHealthpack();
            }
        }

        //If collided with battery pickup, and it isnt null
        if(other.GetComponent<Collider>() != null && other.GetComponent<Collider>().gameObject.CompareTag("Battery"))
        {
            //Reset battery to 100%
            GetComponent<FlashlightDamage>().RechargeBattery();

            //Turning off battery pickup after use
            Destroy(other.gameObject);

            stats.AddBattery();
        }
    }

    void SetHeartbeat(int fear)
    {
        //Checking for change in fear
        if (fear > currentFear)
        {
            //Updating current fear
            currentFear++;
            Heartbeat(currentFear);
        }
    }

    void Heartbeat(int level)
    {
       
        switch (level)
        {
            case 0:
                audioSource.Stop();
                break;

            case 1:
                audioSource.Stop();
                audioSource.clip = heartbeat_1;
                audioSource.Play();
                break;

            case 2:
                audioSource.Stop();
                audioSource.clip = heartbeat_2;
                audioSource.Play();
                break;

            case 3:
                audioSource.Stop();
                audioSource.clip = heartbeat_3;
                audioSource.Play();
                break;

            case 4:
                audioSource.Stop();
                audioSource.clip = heartbeat_4;
                audioSource.Play();
                break;

            case 5:
                audioSource.Stop();
                audioSource.clip = heartbeat_5;
                audioSource.Play();
                break;

            default:
                Debug.LogWarning("Invalid Heartbeat Level");
                break;
        }
    }
}