using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 2f;
    Vector2 location;
    // Start is called before the first frame update
    void Start()
    {
        location = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = Vector2.MoveTowards(transform.position, location, speed * Time.deltaTime);
    }
}
