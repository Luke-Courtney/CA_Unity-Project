using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnlocker : MonoBehaviour
{
    //StatTracker
    private StatTracker stats;

    //Light Pulse
    private bool hasLightPulse;
    private int pulseUnlockNum = 10;

    //Ping
    private bool hasPing;
    private int pingUnlockNum = 20;

    //Movement
    private bool hasMovement;
    private int movementUnlockNum = 5;

    //Teleport
    private bool hasTeleport;
    private int teleportUnlockNum = 35;

    //Audio
    private AudioSource audioSource;
    public AudioClip unlockSound;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<StatTracker>();

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
        int score = stats.GetScore();

        if (!hasLightPulse) 
        {
            if(score >= pulseUnlockNum)
            {
                hasLightPulse = true;
                GameObject.Find("Player").GetComponent<LightPulse>().Collected(hasLightPulse);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }

        if (!hasPing)
        {
            if (score >= pingUnlockNum)
            {
                hasPing = true;
                GameObject.Find("Player").GetComponent<PingAbility>().Collected(hasPing);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }

        if (!hasMovement)
        {
            if (score >= movementUnlockNum)
            {
                hasMovement = true;
                GameObject.Find("Player").GetComponent<MovementAbility>().Collected(hasMovement);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }

        if (!hasTeleport)
        {
            if (score >= teleportUnlockNum)
            {
                hasTeleport = true;
                GameObject.Find("Player").GetComponent<Teleport>().Collected(hasTeleport);
                audioSource.PlayOneShot(unlockSound, 0.7f);
            }
        }
    }
}
