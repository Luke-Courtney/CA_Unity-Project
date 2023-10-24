using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    private Light indicator;

    private float lastLightPulse = 0;
    private float lightPulseInterval = 15.0f;

    private float indicatorIntensity;

    private AudioSource audioSource;
    public AudioClip pulseSound;

    //Has the ability been unlocked?
    //Public for testing
    public bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("LightPulse Indicator").GetComponent<Light>();
        lastLightPulse = 15;

        //Getting Audio Source component
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collected)
        {
            UpdateIndicatorIntensity();

            //Lightpulse
            if (Input.GetMouseButton(0) && lastLightPulse > lightPulseInterval)
            {
                //Attack
                lastLightPulse = 0;
                indicatorIntensity = 0;
                gameObject.GetComponent<FlashlightDamage>().lightPulse();
                audioSource.PlayOneShot(pulseSound, 0.7f);
            }
            else
            {
                //Continue counting time since last attack
                lastLightPulse += Time.deltaTime;
            }
        }
        else
        {
            indicator.intensity = 0;
            lastLightPulse = 0;

        }
        
    }


    void UpdateIndicatorIntensity()
    {
            if (indicatorIntensity < 5) { indicatorIntensity = lastLightPulse / 3; }
            indicator.intensity = indicatorIntensity;
    }

    public void Collected(bool isCollected)
    {
        collected = isCollected;
    }
}
