using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawner;
    [SerializeField] float rotationSpeed = 30.0f;
    [SerializeField] float timerStart = 5;
    [SerializeField] float timer = 0;
    [SerializeField] GameObject enemy1;
    // Start is called before the first frame update

    Vector3 CameraPosition()
    {
        return new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.RotateAround(CameraPosition(), Vector3.forward, rotationSpeed * Time.deltaTime);

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            Instantiate(enemy1, spawner.transform.position, Quaternion.identity);
            timerStart -= 0.05f;
            rotationSpeed += 0.05f;
            timer = timerStart;
        }
    }
}
