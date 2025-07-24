using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles behavior of the upgrade water button.
/// </summary>

public class WaterUpgradeButtonScript : MonoBehaviour
{
    // Reference to the button component
    Button thisButton;
    
    void Start()
    {
        // Get button and set up listener to increase water stat
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Attempt to increase the player's water stat when button is clicked
        ScoreKeeper.Instance.AddToWater();
    }
}
