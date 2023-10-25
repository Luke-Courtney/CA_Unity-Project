using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnlocker : MonoBehaviour
{
    //StatTracker
    private StatTracker stats;

    //Light Pulse
    private bool hasLightPulse;
    private int pulseUnlockNum = 5;

    //Audio
    private AudioSource audioSource;
    public AudioClip unlockSound;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("StatTracker").GetComponent<StatTracker>();

        //Getting Audio Source component
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnlocks();
    }

    private void CheckUnlocks()
    {
        if(!hasLightPulse) 
        {
            if(stats.GetKills() >= pulseUnlockNum)
            {
                hasLightPulse = true;
                GameObject.Find("Player").GetComponent<LightPulse>().Collected(hasLightPulse);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }
    }
}
