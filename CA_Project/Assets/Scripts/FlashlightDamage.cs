using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightDamage : MonoBehaviour
{
    public float flashlightRange = 10.0f;
    public float damage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Create a ray that starts at the position of the game object and extends forwards.
        Ray ray = new Ray(transform.position, transform.forward);

        // Perform a raycast using the ray.
        if (Physics.Raycast(ray, out RaycastHit hit, flashlightRange) && hit.collider.gameObject.tag == "Enemy")
        {
            // If the raycast hits an object, print the name of the object to the console.
            Debug.DrawLine(ray.origin, hit.point, Color.green, 2, false);
            
            //If enemy is hit
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            enemy.damage(damage, hit.distance);
        }
    }
}
