using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        selection = initalSelection;
        selectionIndicator = GameObject.Find("Selector");
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
        }
    }
}
