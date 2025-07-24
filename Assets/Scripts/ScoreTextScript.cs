using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Updates the currency (Dol) text displayed on the UI
/// </summary>
public class ScoreTextScript : MonoBehaviour
{
    // reference to text component
    TMP_Text text;

    void Start()
    {
        // Cache the text component from child
        text = gameObject.GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        // Update text to show current dol amount with leading zeros
        text.text = string.Format("Dol: {0:D4}", ScoreKeeper.Instance.GetDol());
    }
}
