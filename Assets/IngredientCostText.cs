using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientCostText : MonoBehaviour
{
    [SerializeField] TMP_Text fireCostText;
    [SerializeField] TMP_Text waterCostText;
    [SerializeField] TMP_Text windCostText;
    [SerializeField] GameObject scorekeeper;
    // Start is called before the first frame update
    void Start()
    {
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
    }

    // Update is called once per frame
    void Update()
    {
        fireCostText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetFireCost());
        waterCostText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWaterCost());
        windCostText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWindCost());
    }
}
