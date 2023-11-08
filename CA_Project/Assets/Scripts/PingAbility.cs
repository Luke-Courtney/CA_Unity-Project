using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingAbility : MonoBehaviour
{
    private Light indicator;

    private float lastPing = 0;
    private float lightPingInterval = 30.0f;

    private float indicatorIntensity;

    private AudioSource audioSource;
    public AudioClip pingSound;

    //Has the ability been unlocked?
    //Public for testing
    public bool collected = false;

    //Player
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("Ping Indicator").GetComponent<Light>();
        lastPing = 15;

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

                //Ping
                if (Input.GetMouseButtonDown(1) && lastPing > lightPingInterval)
                {
                    //Attack
                    lastPing = 0;
                    indicatorIntensity = 0;
                    gameObject.GetComponent<FlashlightDamage>().Ping();
                    audioSource.PlayOneShot(pingSound, 0.7f);
                }
                else
                {
                    //Continue counting time since last attack
                    lastPing += Time.deltaTime;
                }
            }
            else
            {
                indicator.intensity = 0;
                lastPing = 0;
            }
        }
        else
        {
            indicator.intensity = 0;
            lastPing = 0;
        }

    }


    void UpdateIndicatorIntensity()
    {
        if (indicatorIntensity < 5) { indicatorIntensity = lastPing / 3; }
        indicator.intensity = indicatorIntensity;
    }

    public void Collected(bool isCollected)
    {
        collected = isCollected;
    }
}
