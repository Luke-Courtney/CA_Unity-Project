using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Sounds
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Getting audiosource and setting loop to om
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;

        //Playing music
        audioSource.Play();
    }
}
