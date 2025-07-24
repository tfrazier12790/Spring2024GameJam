using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the size and slowing effect of the Steam effect 
/// created when mixing the fire and water ingredients.
/// </summary>

public class Steam : MonoBehaviour
{
    // Variables for size
    float expansionSpeed = 10.0f;
    Vector3 finishedSize;

    void Start()
    {
        // Calculate size based on player stats
        float size = (ScoreKeeper.Instance.GetWaterStat() / 6) + (ScoreKeeper.Instance.GetFireStat() / 6) + 1;
        finishedSize = new Vector3(size, size, 1);
        transform.localScale = finishedSize;
    }

    void Update()
    {
        // Shrink size down, destroy object when fully shrunk
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, expansionSpeed * Time.deltaTime);
        if (Vector3.Equals(transform.localScale, Vector3.zero))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Applies slow effect to enemies that enter steam
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SendMessage("MakeSlow");
        }
    }
}
