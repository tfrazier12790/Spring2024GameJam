using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of the Tidal Wave ability,
/// created by mixing two water ingredients together.
/// The wave expands based on the player's water stat and pushes enemies away.
/// </summary>

public class TidalWaveScript : MonoBehaviour
{
    // Variables for wave size and duration
    float expansionDuration = 0.75f;
    float elapsedTime;
    Vector3 startSize;
    Vector3 maxSize;

    void Start()
    {
        // Set final size based on player water stat
        float size = (ScoreKeeper.Instance.GetWaterStat() / 2) + 3;
        maxSize = new Vector3(size, size, 1);
    }

    void Update()
    {
        // Expand the wave over time based on elapsed time
        elapsedTime += Time.deltaTime;
        float elapsedTimePercent = Mathf.Clamp01(elapsedTime / expansionDuration);
        transform.localScale = Vector3.Lerp(startSize, maxSize, elapsedTimePercent);

        // Destroy the wave after reaching max size
        if (elapsedTimePercent >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
