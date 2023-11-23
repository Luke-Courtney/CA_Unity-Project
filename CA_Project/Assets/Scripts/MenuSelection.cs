using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour
{
    //Selection and selection indicator
    public float selection;
    private GameObject selectionIndicator;
    private float selectionSpacing = 7.5f;

    //Selector speed
    private float speed = 2.5f;

    //Z Position
    private float initalSelection = 0f;

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
        SelectorPos();
    }

    void SelectorPos()
    {
        switch(selection)
        {
            case 0:
                selectionIndicator.transform.position = Vector3.Lerp(selectionIndicator.transform.position, new Vector3(-10, 1, 7.5f), Time.deltaTime * speed);
                break;

            case 1:
                selectionIndicator.transform.position = Vector3.Lerp(selectionIndicator.transform.position, new Vector3(-10, 1, 0f), Time.deltaTime * speed);
                break;

            case 2:
                selectionIndicator.transform.position = Vector3.Lerp(selectionIndicator.transform.position, new Vector3(-10, 1, -7.5f), Time.deltaTime * speed);
                break;

            case 3:
                selectionIndicator.transform.position = Vector3.Lerp(selectionIndicator.transform.position, new Vector3(-10, 1, -15f), Time.deltaTime * speed);
                break;
        }
    }


    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selection > 0)
            {
                selection--;
            }

            selectSource.PlayOneShot(selectSound, 0.7f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if(selection < 3)
            {
                selection++;
            }

            selectSource.PlayOneShot(selectSound, 0.7f);
        }


        if(Input.GetKeyDown(KeyCode.Return))
        {
            selectSource.PlayOneShot(enterSound, 0.7f);

            while(elapsedTime < 125.0f)
            {
                elapsedTime += Time.deltaTime;
            }

            elapsedTime = 0.0f;

            switch (selection)
            {
                case 0:
                    SceneManager.LoadScene("Game");
                    break;

                case 1:
                    SceneManager.LoadScene("Scores");
                    break;

                case 2:
                    SceneManager.LoadScene("Tutorial");
                    break;

                case 3:
                    Application.Quit();
                    break;
            }
        }
    }
}
