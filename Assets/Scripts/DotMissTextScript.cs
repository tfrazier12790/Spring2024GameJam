using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Creates "miss" text when the Updraft ability misses that fades to transparent.
/// </summary>

public class DotMissTextScript : MonoBehaviour
{
    [SerializeField] TMP_Text thisText;
    [SerializeField] float speed = 3.5f;    //how quick the text fades

    void Start()
    {
        //sets position to where the mouse clicked
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        //Gradually fade the text to transparent
        thisText.color = Color.Lerp(thisText.color, Color.clear, speed * Time.deltaTime);

        //destroy the text when it is nearly transparent
        if(thisText.color.a <= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
