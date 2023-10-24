using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    //Selection and selection indicator
    private int selection;
    private GameObject selectionIndicator;
    private int selectionSpacing = 10;

    //Selector speed
    private float speed = 2.5f;

    //Z Position
    private int initalSelection = 10;

    //Sounds
    private AudioSource selectSource;
    public AudioClip selectSound;
    public AudioClip enterSound;

    //Enter timer
    //Gives time for sound to play before continuing
    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        selection = initalSelection;
        selectionIndicator = GameObject.Find("Selector");

        selectSource = GameObject.Find("Select_Sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Selector();
    }

    void Selector()
    {
        GetInput();
        selectionIndicator.transform.position = Vector3.Lerp(selectionIndicator.transform.position, new Vector3(-10, 1, selection), Time.deltaTime * speed);
    }


    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(selection == 0)
            {
                selection = selectionSpacing;
            }
            else if(selection == -selectionSpacing)
            {
                selection = 0;
            }

            selectSource.PlayOneShot(selectSound, 0.7f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selection == selectionSpacing)
            {
                selection = 0;
            }
            else if (selection == 0)
            {
                selection = -selectionSpacing;
            }

            selectSource.PlayOneShot(selectSound, 0.7f);
        }


        if(Input.GetKeyDown(KeyCode.Return))
        {
            selectSource.PlayOneShot(enterSound, 0.7f);

            while(elapsedTime < 75.0f)
            {
                elapsedTime += Time.deltaTime;
            }

            elapsedTime = 0.0f;

            switch (selection)
            {
                case 10:
                    SceneManager.LoadScene("Game");
                    break;

                case 0:
                    Debug.Log("Scores");
                    break;

                case -10:
                    Application.Quit();
                    break;
            }
        }
    }
}
