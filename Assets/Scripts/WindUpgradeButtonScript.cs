using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the behavior of the upgrade wind button.
/// </summary>

public class WindUpgradeButtonScript : MonoBehaviour
{
    // Reference to this button component
    Button thisButton;

    void Start()
    {
        // Get button and set up listener to increase wind stat
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Attempt to increase the player's wind stat when button is clicked
        ScoreKeeper.Instance.AddToWind();
    }
}
