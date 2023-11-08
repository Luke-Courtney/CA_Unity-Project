using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    private string path = "Assets/Stats/stats.txt";

    //File content list
    private string killsNum;
    private string deathsNum;
    private string pickupsNum;
    private string healthNum;
    private string batteriesNum;

    //Text object
    private TextMeshPro scoreText;

    // Start is called before the first frame update
    void Start()
    {
        ReadStats();

        scoreText = GameObject.Find("Scores").GetComponent<TextMeshPro>();
        scoreText.SetText("Kills:\t\t" + killsNum + "\n" +
                          "Deaths:\t" + deathsNum + "\n" +
                          //"Pickups:\t" + pickupsNum + "\n" +
                          "Health:\t" + healthNum + "\n" +
                          "Batteries:\t" + batteriesNum + "\n");
    }

    //Read the stat file
    void ReadStats()
    {
        Debug.Log("Reading stats for StatDisplay");

        //Reads playerprefs for stats
        if (PlayerPrefs.HasKey("kills")) { killsNum = PlayerPrefs.GetInt("kills").ToString(); }
        if (PlayerPrefs.HasKey("deaths")) { deathsNum = (PlayerPrefs.GetInt("deaths")).ToString(); }
        if (PlayerPrefs.HasKey("pickups")) { pickupsNum = (PlayerPrefs.GetInt("pickups")).ToString(); }
        if (PlayerPrefs.HasKey("health")) { healthNum = (PlayerPrefs.GetInt("health")).ToString(); }
        if (PlayerPrefs.HasKey("batteries")) { batteriesNum = (PlayerPrefs.GetInt("batteries")).ToString(); }

        Debug.Log(killsNum);
        Debug.Log(deathsNum);
        Debug.Log(pickupsNum);
        Debug.Log(healthNum);
        Debug.Log(batteriesNum);
    }
}
