using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays the cost of each elemental upgrade.
/// </summary>

public class IngredientCostText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_Text fireCostText;
    [SerializeField] TMP_Text waterCostText;
    [SerializeField] TMP_Text windCostText;

    void Update()
    {
        // Display the cost tp upgrade each element
        fireCostText.text = ScoreKeeper.Instance.GetFireStat() < 10 ? $"+{ScoreKeeper.Instance.GetFireCost()}" : "MAX";
        waterCostText.text = ScoreKeeper.Instance.GetWaterStat() < 10 ? $"+{ScoreKeeper.Instance.GetWaterCost()}" : "MAX";
        windCostText.text = ScoreKeeper.Instance.GetWindStat() < 10 ? $"+{ScoreKeeper.Instance.GetWindCost()}" : "MAX";
    }
}
