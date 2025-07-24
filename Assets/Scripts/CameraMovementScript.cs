using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera movement, moving it smoothly towards the player
/// to remove any jerky camera movements
/// </summary>

public class CameraMovementScript : MonoBehaviour
{
    GameObject player;
    public float speed = 5f; //speed of camera when catching up to the player

    void Start()
    {
        //finds and stores reference to the player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //smooths movement of camera towards the player
        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(player.transform.position.x, player.transform.position.y, -10), //maintains a fixed -10 position on z axis
            speed * Time.deltaTime);
    }
}
