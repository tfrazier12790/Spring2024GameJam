using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DotMissTextScript : MonoBehaviour
{
    [SerializeField] TMP_Text thisText;
    [SerializeField] float speed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        thisText.color = Color.Lerp(thisText.color, Color.clear, speed * Time.deltaTime);

        if(thisText.color.a <= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
