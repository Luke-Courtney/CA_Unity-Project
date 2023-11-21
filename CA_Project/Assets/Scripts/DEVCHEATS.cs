using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVCHEATS : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //AltGR + D
        //Delete PlayerPrefs
        if(Input.GetKey(KeyCode.AltGr) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs Deleted");
        }

        //AltGR +  L
        //Overhead Light
        if (Input.GetKey(KeyCode.AltGr) && Input.GetKeyDown(KeyCode.L))
        {
            if (GameObject.Find("CheatLight").GetComponent<Light>().intensity == 0)
            {
                GameObject.Find("CheatLight").GetComponent<Light>().intensity = 1;
            }
            else
            {

                GameObject.Find("CheatLight").GetComponent<Light>().intensity = 0;
            }

            Debug.Log("Overhead light Toggled");
        }

    }
}
