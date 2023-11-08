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

        //Setting the text
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
        killsNum = PlayerPrefs.GetInt("kills", 0).ToString();
        deathsNum = PlayerPrefs.GetInt("deaths", 0).ToString();
        pickupsNum = PlayerPrefs.GetInt("pickups", 0).ToString();
        healthNum = PlayerPrefs.GetInt("health", 0).ToString();
        batteriesNum = PlayerPrefs.GetInt("batteries", 0).ToString();
    }
}
