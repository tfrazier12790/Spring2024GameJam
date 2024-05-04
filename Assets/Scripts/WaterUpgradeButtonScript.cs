using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUpgradeButtonScript : MonoBehaviour
{
    [SerializeField] Button thisButton;
    [SerializeField] GameObject scorekeeper;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
    }

    private void TaskOnClick()
    {
        Debug.Log("Add To Water Clicked");
        scorekeeper.GetComponent<ScoreKeeper>().AddToWater();
    }
}
