using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialInteractables : MonoBehaviour
{
    //Time
    private float seconds;
    public float displaySeconds;
    public TextMeshPro timer;

    // Update is called once per frame
    void Update()
    {
        //Slide 0
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        //Slide 3
        SurviveTimer();

        //Exit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    //Slide 1 flashlight toggle
    void ToggleFlashlight()
    {
        if (GameObject.Find("Slide 1 Flashlight").GetComponent<Light>().intensity == 0)
        {
            GameObject.Find("Slide 1 Flashlight").GetComponent<Light>().intensity = 5;
        }
        else
        {
            GameObject.Find("Slide 1 Flashlight").GetComponent<Light>().intensity = 0;
        }
    }

    void SurviveTimer()
    {
        //Update times
        seconds += Time.deltaTime;
        displaySeconds = Mathf.Round(seconds);

        int minutesSurvived = (int)Mathf.Floor((displaySeconds / 60));
        int secondsSurvived = (int)(displaySeconds % 60);

        if(secondsSurvived < 10)
        {

            timer.SetText(minutesSurvived + ":0" + secondsSurvived);
        }
        else
        {
            timer.SetText(minutesSurvived + ":" + secondsSurvived);
        }
    }
}
