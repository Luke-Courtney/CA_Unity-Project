using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    //Playercontroller
    private PlayerController playerController;

    //Wave Manager
    private WaveManager waveManager;

    //Stats
    private StatTracker statTracker;

    //Death text
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI timeSurvivedText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI healthPackText;
    public TextMeshProUGUI batteriesText;

    private bool deathStatsTaken;

    // Start is called before the first frame update
    void Start()
    {
        //Playercontroller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        //WaveManager
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        //StatTracker
        statTracker = GameObject.Find("StatTracker").GetComponent<StatTracker>();

        //Setup UI for start of game
        deathText.gameObject.SetActive(false);
        timeSurvivedText.gameObject.SetActive(false);
        killsText.gameObject.SetActive(false);
        healthPackText.gameObject.SetActive(false);
        batteriesText.gameObject.SetActive(false);
        deathStatsTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Call die function
        Die();
    }

    //Updates UI with death elements
    void Die()
    {

        if(!playerController.isAlive())
        {
            //"You Died" text
            deathText.gameObject.SetActive(true);

            //Time survived
            float totalSeconds = waveManager.displaySeconds;
            int minutesSurvived = (int)Mathf.Floor((totalSeconds/60));
            int secondsSurvived = (int)(totalSeconds % 60);

            //Only update time once
            if (!deathStatsTaken) 
            {
                timeSurvivedText.SetText("Time Survived: " + minutesSurvived + ":" + secondsSurvived);
                killsText.SetText("Kills: " + statTracker.GetCurrentKills());
                healthPackText.SetText("Health Kits: " + statTracker.GetCurrentHealthpacks());
                batteriesText.SetText("Batteries: " + statTracker.GetCurrentBatteries());

                deathStatsTaken = true;
            }
            
            //Set stat text active
            killsText.gameObject.SetActive(true);
            healthPackText.gameObject.SetActive(true);
            batteriesText.gameObject.SetActive(true);
            timeSurvivedText.gameObject.SetActive(true);
        }
    }
}
