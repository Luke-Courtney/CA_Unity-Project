using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class StatWriter : MonoBehaviour
{
    private StatTracker stats;

    //File content list
    private int killsNum;
    private int deathsNum;
    private int pickupsNum;
    private int healthNum;
    private int batteriesNum;

    // Start is called before the first frame update
    void Start()
    {
        //Getting stats component
        stats = GetComponent<StatTracker>();
    }

    public void UpdateStats()
    {
        ReadStats();
        WriteStats();
    }

    //Reads playerprefs for stats and sets variable values accordingly
    void ReadStats()
    {
        if (PlayerPrefs.HasKey("kills")) { killsNum = PlayerPrefs.GetInt("kills");}
        else { PlayerPrefs.SetInt("kills", 0); }

        if (PlayerPrefs.HasKey("deaths")) { deathsNum = PlayerPrefs.GetInt("deaths"); }
        else { PlayerPrefs.SetInt("deaths", 0); }

        if (PlayerPrefs.HasKey("pickups")) { pickupsNum = PlayerPrefs.GetInt("pickups"); }
        else { PlayerPrefs.SetInt("pickups", 0); }

        if (PlayerPrefs.HasKey("health")) { healthNum = PlayerPrefs.GetInt("health"); }
        else { PlayerPrefs.SetInt("health", 0); }

        if (PlayerPrefs.HasKey("batteries")) { batteriesNum = PlayerPrefs.GetInt("batteries"); }
        else { PlayerPrefs.SetInt("batteries", 0); }

        Debug.Log("Playerprefs:");
        Debug.Log(PlayerPrefs.GetInt("kills"));
        Debug.Log(PlayerPrefs.GetInt("deaths"));
        Debug.Log(PlayerPrefs.GetInt("health"));
    }

    //Update and save stats
    void WriteStats()
    {
        //kills
        if(PlayerPrefs.HasKey("kills"))
        {
            PlayerPrefs.SetInt("kills", (killsNum + PlayerPrefs.GetInt("kills")));
        }

        //Deaths
        if (PlayerPrefs.HasKey("deaths"))
        {
            PlayerPrefs.SetInt("deaths", (deathsNum + PlayerPrefs.GetInt("deaths")));
        }

        //pickups
        if (PlayerPrefs.HasKey("pickups"))
        {
            PlayerPrefs.SetInt("pickups", (pickupsNum + PlayerPrefs.GetInt("pickups")));
        }

        //health
        if (PlayerPrefs.HasKey("health"))
        {
            PlayerPrefs.SetInt("health", (healthNum + PlayerPrefs.GetInt("health")));
        }

        //batteries
        if (PlayerPrefs.HasKey("batteries"))
        {
            PlayerPrefs.SetInt("batteries", (batteriesNum + PlayerPrefs.GetInt("batteries")));
        }

        //Save stat changes
        PlayerPrefs.Save();

        Debug.Log("StatWriter:");
        Debug.Log(killsNum);
        Debug.Log(deathsNum);
        Debug.Log(pickupsNum);
        Debug.Log(healthNum);
        Debug.Log(batteriesNum);
    }
}
