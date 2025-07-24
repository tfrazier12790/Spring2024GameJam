using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Explosion ability created by mixing two fire ingredients together.
/// Creates an expanding wave that deals damage aand increases in size based on the 
/// player's fire stat.
/// </summary>

public class ExplosionScript : MonoBehaviour
{
    // Variables for explosion size and duration
    float expansionDuration = 0.75f;   // Total time for expansion in seconds
    float elapsedTime;
    Vector3 startSize;
    Vector3 maxSize;

    // Variables for damage
    float minDamage = 3f;
    float maxDamage = 12f;
    

    void Start()
    {
        // Set explosion size based on player fire stat
        maxSize = new Vector3(((float)ScoreKeeper.Instance.GetFireStat() / 3) + 1, ((float)ScoreKeeper.Instance.GetFireStat() / 3) + 1, 1);
        startSize = transform.localScale;
    }

    void Update()
    {
        // Update time elapsed and increase the size of the object toward its final size
        elapsedTime += Time.deltaTime;
        float elapsedTimePercent = Mathf.Clamp01(elapsedTime / expansionDuration);
        transform.localScale = Vector3.Lerp(startSize, maxSize, elapsedTimePercent);

        // Destroy object when duraction reached
        if (elapsedTimePercent >= 1.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Deal damage to enemy based on fire stat, scaling from min to max
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float fireStatPercent = Mathf.Clamp01((ScoreKeeper.Instance.GetFireStat() - 1f) / 9f);
            float damage = Mathf.Lerp(minDamage, maxDamage, fireStatPercent);
            collision.gameObject.SendMessage("TakeDamage", damage); 
        }
    }
}
