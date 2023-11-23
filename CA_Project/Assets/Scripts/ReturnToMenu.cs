using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    //Sounds
    private AudioSource selectSource;
    public AudioClip enterSound;

    //Enter timer
    //Gives time for sound to play before continuing
    private float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        selectSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Selector();   
    }

    void Selector()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectSource.PlayOneShot(enterSound, 0.7f);

            while (elapsedTime < 125.0f)
            {
                elapsedTime += Time.deltaTime;
            }

            elapsedTime = 0.0f;

            SceneManager.LoadScene("Menu");
        }
    }
}
