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

    //Score (Kills, but only from current round)
    int score = 0;

    //Score
    public void AddScore()
    {
        score++;
    }

    //Current score amount
    public int GetScore()
    {
        return score;
    }
  
    //Combat
    //Flashlight kills
    public void AddKill()
    {
        AddScore();
        kills++;
        PlayerPrefs.SetInt("kills", (PlayerPrefs.GetInt("kills") + 1));
    }

    //Total kill amount
    public int GetKills()
    {
       return PlayerPrefs.GetInt("kills",0);
    }

    //Current game kill amount
    public int GetCurrentKills()
    {
       return kills;
    }

    //Deaths
    public void AddDeath()
    {
        deaths++;
        PlayerPrefs.SetInt("deaths", (PlayerPrefs.GetInt("death") + 1));
    }

    public int GetDeaths()
    {
        return PlayerPrefs.GetInt("deaths",0);
    }

    public int GetCurrentDeaths()
    {
        return deaths;
    }

    //Pickups
    //Total pickups
    public void SetPickups()
    {
        GetPickups();
    }

    public int GetPickups()
    {
        PlayerPrefs.SetInt("pickups", (PlayerPrefs.GetInt("health") + PlayerPrefs.GetInt("batteries")));
        return PlayerPrefs.GetInt("pickups",0);
    }

    public int GetCurrentPickups()
    {
        return pickups;
    }

    //Healthpacks
    public void AddHealthpack()
    {
        healthPacks++;
        PlayerPrefs.SetInt("health", (PlayerPrefs.GetInt("health") + 1));
    }

    //Total healthpack amounts
    public int GetHealthpacks()
    {
        return PlayerPrefs.GetInt("health", 0);
    }

    //Current healthpack amount
    public int GetCurrentHealthpacks()
    {
        return healthPacks;
    }

    //Batteries
    public void AddBattery()
    {
        batteries++;
        PlayerPrefs.SetInt("batteries", (PlayerPrefs.GetInt("batteries") + 1)); ;
    }

    public int GetBatteries()
    {
        return PlayerPrefs.GetInt("batteries",0);
    }

    //current battery amount
    public int GetCurrentBatteries()
    {
        return batteries;
    }

    //Save stats
    public void SaveStats()
    {
        PlayerPrefs.Save();
    }

    //Test function
    //Calls all stats
    public void testStats()
    {
        Debug.Log("\n\n");
        Debug.Log("kills: " + PlayerPrefs.GetInt("kills").ToString());
        Debug.Log("deaths: " + PlayerPrefs.GetInt("deaths").ToString());
        Debug.Log("pickups: " + PlayerPrefs.GetInt("pickups").ToString());
        Debug.Log("healthPacks: " + PlayerPrefs.GetInt("health").ToString());
        Debug.Log("batteries: " + PlayerPrefs.GetInt("batteries").ToString());
    }
}
