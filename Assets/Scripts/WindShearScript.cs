using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Wind Shear projectile created by mixing two wind ingredients into the potion.
/// The projectile moves in the direction of the mouse cursor and lasts for a certain amount of 
/// time based on the player's wind stat. It also scales in size accordingly.
/// </summary>

public class WindShearScript : MonoBehaviour
{
    // Projectile movement speed
    float speed = 3f;

    // Projectile lifetime based on wind stat
    float lifetime;

    // Direction and rotation toward mouse position
    Vector3 targetPosition;
    Quaternion lookRot;

    // Reference to main camera
    Camera mainCam;

    void Start()
    {
        // Cache the main cam
        mainCam = Camera.main;

        // Set lifetime based on wind stat
        lifetime = (ScoreKeeper.Instance.GetWindStat() / 5) + 1;

        // Determine the direction based on mouse position
        targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        lookRot = Quaternion.Euler(0, 0, angle);

        // Scale size based on wind stat
        float windStat = ScoreKeeper.Instance.GetWindStat();
        transform.localScale = new Vector3(.5f * windStat * 0.75f, windStat * 0.75f, 1);
    }

    void Update()
    {
        // Move the projectile forward in its local right direction
        transform.position += transform.right * speed * Time.deltaTime;

        // Apply rotation
        transform.rotation = lookRot;

        // Reduce lifetime, destroy object when time expires
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) 
        {
            Destroy(gameObject); 
        }
    }
}
