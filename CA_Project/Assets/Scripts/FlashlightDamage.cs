using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    //Stat tracker
    private StatTracker stats;

    //Damage beam
    private bool flashlightOn = true;
    private float flashlightDefaultIntensity = 5.0f;
    public float flashlightDamageRange = 10.0f;
    public float damage = 10.0f;
    private Light flashlight;

    //Light Pulse
    private GameObject[] enemies;
    private float pulseRange = 5.0f;
    private float pulseDamage = 80.0f;
    private Light pulseLight;

    //Ping light
    private Light pingLight;

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
        pulseLight = GameObject.Find("LightPulse Light").GetComponent<Light>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        stats = GameObject.Find("StatTracker").GetComponent<StatTracker>();
        pingLight = GameObject.Find("Ping Light").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        DamageBeam();
        DepleteBattery();
        ToggleFlashlight();
    }

    void DamageBeam()
    {
        //If flashlight on
        if (flashlightOn)
        {
            // Create a ray that starts at the position of the game object and extends forwards.
            Ray ray = new Ray(transform.position, transform.forward);

            // Perform a raycast using the ray.
            //If enemy is hit
            if (Physics.Raycast(ray, out RaycastHit hit, (flashlightDamageRange * (batteryLevel / 100))) && hit.collider.gameObject.tag == "Enemy")
            {
                EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                enemy.damage(damage, hit.distance);
            }
        }
    }

    void DepleteBattery()
    {
        if (playerController.isAlive() && flashlightOn)
        {
            batteryLevel = batteryLevel - (batteryDrainRate * Time.deltaTime);
            flashlight.intensity = flashlightDefaultIntensity * (batteryLevel / 100);

            //Increasing battery drain rate
            if (batteryDrainRate < 2.5)
            {
                batteryDrainRate = defaultBatteryDrainRate + ((waveManager.GetTime() / 60) / 2);
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

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlightOn)
            {
                flashlight.intensity = 0;
                flashlightOn = false;
            }
            else
            {
                flashlight.intensity = flashlightDefaultIntensity * (batteryLevel / 100);
                flashlightOn = true;
            }
        }
    }

    //Light pulse
    //Creates a ring around the player, briefly damaging any enemies inside
    public void lightPulse()
    {
        //Pulse light
        StartCoroutine(LightPulseLight(0.1f));

        //Check if any enemies are in range
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            //If enemy is close enough
            if (Vector3.Distance(GameObject.Find("Player").transform.position, enemies[i].transform.position) < pulseRange)
            {
                enemies[i].GetComponent<EnemyController>().damage(pulseDamage, Vector3.Distance(GameObject.Find("Player").transform.position, enemies[i].transform.position));
            }
        }
    }

    //Flash lightPulse light
    private IEnumerator LightPulseLight(float waitTime)
    {
        float elapsedTime = 0;
        float runTime = 1.5f;

        bool lightOff = true;

        do
        {
            //Keeping track of time
            elapsedTime += Time.deltaTime;

            //Turning on light
            if (lightOff)
            {
                if (pulseLight.intensity < 5)
                {
                    pulseLight.intensity += 1.0f;
                }
                else
                {
                    lightOff = false;
                }
            }
            else
            {
                if (pulseLight.intensity > 0)
                {
                    pulseLight.intensity -= 2.0f;
                }
                else
                {
                    lightOff = true;
                    yield break;
                }
            }

            yield return new WaitForSeconds(waitTime);

        } while (elapsedTime < runTime);
    }

    //Ping
    public void Ping()
    {
        //Pulse ping light
        StartCoroutine(pingLightFlash(0.1f));

        //Check if any enemies are in range
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //Loop through all enemies
        for(int i = 0; i < enemies.Length; i++)
        {
            //Setting all currently alive enemies ping lights to an intensity of 3
            enemies[i].transform.Find("Ping").GetComponent<Light>().intensity = 3;
        }
    }

    //Flash lightPulse light
    private IEnumerator pingLightFlash(float waitTime)
    {
        float elapsedTime = 0;
        float runTime = 1.5f;

        bool lightOff = true;

        do
        {
            //Keeping track of time
            elapsedTime += Time.deltaTime;

            //Turning on light
            if (lightOff)
            {
                if (pingLight.intensity < 5)
                {
                    pingLight.intensity += 1.0f;
                }
                else
                {
                    lightOff = false;
                }
            }
            else
            {
                if (pingLight.intensity > 0)
                {
                    pingLight.intensity -= 2.0f;
                }
                else
                {
                    lightOff = true;
                    yield break;
                }
            }

            yield return new WaitForSeconds(waitTime);

        } while (elapsedTime < runTime);
    }

}
