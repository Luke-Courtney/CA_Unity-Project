using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    //Audio source
    private AudioSource source;

    //Enemycontroller
    private EnemyController controller;

    //Is it already playing a clip?
    bool isChasing;

    //Random pitch change when chasing
    float chasePitch;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        controller = GetComponent<EnemyController>();

        isChasing = false;
        chasePitch = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.IsChasing())
        {
            isChasing = true;
            source.pitch = chasePitch;
        }
        else
        {
            isChasing = false;
            source.pitch = 1;
        }
    }
}
