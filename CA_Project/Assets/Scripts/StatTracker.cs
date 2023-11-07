using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    //StatWriter
    private StatWriter statWriter;

    private void Start()
    {
        statWriter = GetComponent<StatWriter>();
    }

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
    //Total pickups
    public int GetPickups()
    {
        return pickups;
    }
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

    //Writing stats to file
    public void LogStats()
    {
        //Writing stats
        Debug.Log("Updating stats file");
        statWriter.UpdateStats();

        //Resetting stats in-game
        kills = 0;
        deaths = 0;
        pickups = 0;
        healthPacks = 0;
        batteries = 0;
    }
}
