using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Updates the text in the store screen to display the correct levels of the 
/// players three elemental stats.
/// </summary>
/// 
public class ElementLevelsText : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] TMP_Text fireLevelText;
    [SerializeField] TMP_Text waterLevelText;
    [SerializeField] TMP_Text windLevelText;

    void Update()
    {
        // Display the level of each element, showing "MAX" if the stat is at 10
        fireLevelText.text = ScoreKeeper.Instance.GetFireStat() < 10 ? $"+{ScoreKeeper.Instance.GetFireStat()}" : "MAX";
        waterLevelText.text = ScoreKeeper.Instance.GetWaterStat() < 10 ? $"+{ScoreKeeper.Instance.GetWaterStat()}" : "MAX";
        windLevelText.text = ScoreKeeper.Instance.GetWindStat() < 10 ? $"+{ScoreKeeper.Instance.GetWindStat()}" : "MAX";
    }
}
