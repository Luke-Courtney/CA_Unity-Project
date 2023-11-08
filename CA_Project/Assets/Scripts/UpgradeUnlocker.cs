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

    //Ping
    private bool hasPing;
    private int pingUnlockNum = 10;

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

    //Checks if the player has met the required number of kills to unlock each ability
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

        if (!hasPing)
        {
            if (stats.GetKills() >= pingUnlockNum)
            {
                hasPing = true;
                GameObject.Find("Player").GetComponent<PingAbility>().Collected(hasPing);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }
    }
}
