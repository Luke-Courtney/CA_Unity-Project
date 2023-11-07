using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    private string path = "Assets/Stats/stats.txt";

    //File content list
    private string[] content = new string[5];
    private string kills;
    private string deaths;
    private string pickups;
    private string health;
    private string batteries;

    //Text object
    private TextMeshPro scoreText;

    // Start is called before the first frame update
    void Start()
    {
        ReadFile();

        scoreText = GameObject.Find("Scores").GetComponent<TextMeshPro>();
        scoreText.SetText("Kills:\t\t" + kills + "\n" +
                          "Deaths:\t" + deaths + "\n" +
                          //"Pickups:\t" + pickups + "\n" +
                          "Health:\t" + health + "\n" +
                          "Batteries:\t" + batteries + "\n");
    }

    //Read the stat file
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

        kills = content[0];
        deaths = content[1];
        pickups = content[2];
        health = content[3];
        batteries = content[4];
    }
}
