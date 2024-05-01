using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] GameObject scorekeeper;
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
        text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("Dol: {0:D4}", scorekeeper.GetComponent<ScoreKeeper>().GetScore());
    }
}
