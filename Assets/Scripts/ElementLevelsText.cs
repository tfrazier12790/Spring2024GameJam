using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementLevelsText : MonoBehaviour
{
    [SerializeField] TMP_Text fireLevelText;
    [SerializeField] TMP_Text waterLevelText;
    [SerializeField] TMP_Text windLevelText;
    [SerializeField] GameObject scorekeeper;
    // Start is called before the first frame update
    void Start()
    {
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
    }

    // Update is called once per frame
    void Update()
    {
        fireLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetFireStat());
        waterLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat());
        windLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWindStat());
    }
}
