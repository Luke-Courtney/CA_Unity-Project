using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    //Combat stats
    int kills = 0;
    int deaths = 0;

    //Pickup stats
    int pickups = 0;
    int healthPacks = 0;
    int batteries = 0;

  
    //Combat
    //Flashlight kills
    public void AddKill()
    {
        kills++;
    }

    public int GetKills()
    {
       return kills;
    }

    //Deaths
    public void AddDeath()
    {
        deaths++;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    //Pickups
    //Healthpacks
    public void AddHealthpack()
    {
        healthPacks++;
    }

    public int GetHealthpacks()
    {
        return healthPacks;
    }

    //Batteries
    public void AddBattery()
    {
        batteries++;
    }

    public int GetBatteries()
    {
        return batteries;
    }

    public void testStats()
    {
        Debug.Log("\n\n");
        Debug.Log("kills: " + kills);
        Debug.Log("deaths: " + deaths);
        Debug.Log("pickups: " + pickups);
        Debug.Log("healthPacks: " + healthPacks);
        Debug.Log("batteries: " + batteries);
    }
}
