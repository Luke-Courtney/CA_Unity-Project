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

    //Death text
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI timeSurvivedText;
    private bool deathTimeTaken;

    // Start is called before the first frame update
    void Start()
    {
        //Playercontroller
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        //WaveManager
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        //Setup UI for start of game
        deathText.gameObject.SetActive(false);
        timeSurvivedText.gameObject.SetActive(false);
        deathTimeTaken = false;
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
            if (!deathTimeTaken) 
            {
                timeSurvivedText.SetText("Time Survived: " + minutesSurvived + ":" + secondsSurvived);
                deathTimeTaken = true;
            }
            
            timeSurvivedText.gameObject.SetActive(true);
        }
    }
}
