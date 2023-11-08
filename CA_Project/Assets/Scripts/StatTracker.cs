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
        PlayerPrefs.SetInt("kills", (PlayerPrefs.GetInt("kills") + 1));
    }

    public int GetKills()
    {
       return PlayerPrefs.GetInt("kills",0);
    }

    //Deaths
    public void AddDeath()
    {
        PlayerPrefs.SetInt("death", (PlayerPrefs.GetInt("death") + 1));
    }

    public int GetDeaths()
    {
        return PlayerPrefs.GetInt("death",0);
    }

    //Pickups
    //Total pickups
    public void SetPickups()
    {
        PlayerPrefs.SetInt("pickups", (PlayerPrefs.GetInt("health") + PlayerPrefs.GetInt("batteries")));
    }

    public int GetPickups()
    {
        return PlayerPrefs.GetInt("pickups",0);
    }
    //Healthpacks
    public void AddHealthpack()
    {
        PlayerPrefs.SetInt("health", (PlayerPrefs.GetInt("health") + 1));
    }

    public int GetHealthpacks()
    {
        return PlayerPrefs.GetInt("health", 0);
    }

    //Batteries
    public void AddBattery()
    {
        PlayerPrefs.SetInt("batteries", (PlayerPrefs.GetInt("batteries") + 1)); ;
    }

    public int GetBatteries()
    {
        return PlayerPrefs.GetInt("batteries",0);
    }

    //Save stats
    public void SaveStats()
    {
        PlayerPrefs.Save();
    }

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
