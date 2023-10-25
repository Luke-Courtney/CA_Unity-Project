using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class StatWriter : MonoBehaviour
{
    private StatTracker stats;
    private string path = "Assets/Stats/stats.txt";

    //File content list
    private string[] content = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        //Getting stats component
        stats = GetComponent<StatTracker>();
    }

    public void UpdateStats()
    {
        ReadFile();
        WriteToFile();
    }

    void ReadFile()
    {
        //Reader
        StreamReader reader = new StreamReader(path);

        //Read each line one by one
        int line = 0;
        while (!reader.EndOfStream)
        {
            content[line] = reader.ReadLine();
            line++;
        }

        reader.Close();
    }

    void WriteToFile()
    {
        StreamWriter writer = new StreamWriter(path, false);

        //Stat values
        int kills, deaths, pickups, healthpacks, batteries = 0;

        if (content[0] != null)
        {
            string killsString = content[0];
            kills = int.Parse(killsString) + stats.GetKills();
        }
        else { kills = 0; }

        if (content[1] != null)
        {
            string deathsString = content[1];
            deaths = int.Parse(deathsString) + stats.GetDeaths();
        }
        else { deaths = 0; }

        if (content[2] != null)
        {
            string pickupsString = content[2];
            pickups = int.Parse(pickupsString) + stats.GetPickups();
        }
        else { pickups = 0; }

        if (content[3] != null)
        {
            string healthpacksString = content[3];
            healthpacks = int.Parse(healthpacksString) + stats.GetHealthpacks();
        }
        else { healthpacks = 0; }

        if (content[4] != null)
        {
            string batteriesString = content[4];
            batteries = int.Parse(batteriesString) + stats.GetBatteries();
        }
        else {  batteries = 0; }

        writer.WriteLine(kills);         //Kills
        writer.WriteLine(deaths);        //Deaths
        writer.WriteLine(pickups);       //Pickups
        writer.WriteLine(healthpacks);   //Healthpacks
        writer.WriteLine(batteries);     //Batteries

        writer.Close();
    }
}
