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
        if (scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() < 10)
        {
            fireLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetFireStat());
        } else
        {
            fireLevelText.text = "MAX";
        }

        if (scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() < 10)
        {
            waterLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat());
        }
        else
        {
            waterLevelText.text = "MAX";
        }

        if (scorekeeper.GetComponent<ScoreKeeper>().GetWindStat() < 10)
        {
            windLevelText.text = string.Format("+{0}", scorekeeper.GetComponent<ScoreKeeper>().GetWindStat());
        }
        else
        {
            windLevelText.text = "MAX";
        }
    }
}
