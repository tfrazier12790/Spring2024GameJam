using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the behavior of the upgrade fire button.
/// </summary>


public class FireUpgradeButtonScript : MonoBehaviour
{
    // Reference to button component
    Button thisButton;

    void Start()
    {
        // Get button and set up listener for increase fire stat
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Attempt to increase the player's fire stat when button is clicked
        ScoreKeeper.Instance.AddToFire();
    }
}
