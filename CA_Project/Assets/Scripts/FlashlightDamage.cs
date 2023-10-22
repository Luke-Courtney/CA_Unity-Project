using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    //Damage beam
    private float flashlightDefaultIntensity = 5.0f;
    public float flashlightDamageRange = 10.0f;
    public float damage = 10.0f;
    private Light flashlight;

    //Battery
    private float batteryLevel = 100;
    private float defaultBatteryDrainRate = 0.5f;
    public float batteryDrainRate = 0.5f;

    //Wave manager
    private WaveManager waveManager;

    //Playercontroller
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        flashlight = GameObject.Find("Flashlight").GetComponent<Light>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DamageBeam();
        DepleteBattery();
    }

    void DamageBeam()
    {
        // Create a ray that starts at the position of the game object and extends forwards.
        Ray ray = new Ray(transform.position, transform.forward);

        // Perform a raycast using the ray.
        //If enemy is hit
        if (Physics.Raycast(ray, out RaycastHit hit, (flashlightDamageRange*(batteryLevel/100))) && hit.collider.gameObject.tag == "Enemy")
        {
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            enemy.damage(damage, hit.distance);
        }
    }

    void DepleteBattery()
    {
        if(playerController.isAlive())
        {
            batteryLevel = batteryLevel - (batteryDrainRate*Time.deltaTime);
            flashlight.intensity = flashlightDefaultIntensity * (batteryLevel/100);

            //Increasing battery drain rate
            if(batteryDrainRate < 2.5){
                batteryDrainRate = defaultBatteryDrainRate + ((waveManager.GetTime()/60)/2);
            }
        }
        else
        {
            flashlight.intensity = 0;
        }
    }

    //Set the battery back to 100%
    public void RechargeBattery()
    {
        batteryLevel = 100;
    }
}
