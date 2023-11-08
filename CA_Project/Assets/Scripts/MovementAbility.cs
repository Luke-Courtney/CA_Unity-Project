using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MovementAbility : MonoBehaviour
{
    private Light indicator;

    private float lastMove = 0;
    private float moveInterval = 15.0f;

    public float moveForce = 50;

    private float indicatorIntensity;

    private AudioSource audioSource;
    public AudioClip moveSound;

    private Rigidbody playerRb;

    //Has the ability been unlocked?
    //Public for testing
    public bool collected = false;

    //Player
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("Movement Indicator").GetComponent<Light>();
        lastMove = 15;

        //Getting Audio Source component
        audioSource = GetComponent<AudioSource>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive())
        {
            if (collected)
            {
                UpdateIndicatorIntensity();

                //Activate ability
                if (Input.GetKeyDown(KeyCode.Space) && lastMove > moveInterval)
                {
                    //Move
                    lastMove = 0;
                    indicatorIntensity = 0;
                    audioSource.PlayOneShot(moveSound, 0.7f);
                    Move();
                }
                else
                {
                    //Continue counting time since last move
                    lastMove += Time.deltaTime;
                }
            }
            else
            {
                indicator.intensity = 0;
                lastMove = 0;
            }
        }
        else
        {
            indicator.intensity = 0;
            lastMove = 0;
        }

    }


    void UpdateIndicatorIntensity()
    {
        if (indicatorIntensity < 5) { indicatorIntensity = lastMove / 3; }
        indicator.intensity = indicatorIntensity;
    }

    public void Collected(bool isCollected)
    {
        collected = isCollected;
    }

    //Adds inpulse force to the player in their direction of movement when activated
    private void Move()
    {
        //Add force to player
        //Vector3.forward, new Vector3(0,0,1) and a few other things didnt make the player move forward relative to where theyre looking, but this does, so its staying in
        playerRb.AddForce(moveForce * (GameObject.Find("Forward").transform.position - GameObject.Find("Player").transform.position), ForceMode.Impulse);
    }
}
