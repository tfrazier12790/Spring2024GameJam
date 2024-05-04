using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameController;
    [SerializeField] GameObject spawner;
    [SerializeField] float rotationSpeed = 30.0f;
    [SerializeField] float timerStart = 2f;
    [SerializeField] float timer = 0;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject boss;
    [SerializeField] bool bossSpawn = true;
    // Start is called before the first frame update

    Vector3 CameraPosition()
    {
        return new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
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
            if (gameController.GetComponent<TimerScript>().GetMinutes() < 2)
            {
                Instantiate(enemy1, spawner.transform.position, Quaternion.identity);
            }
            if (gameController.GetComponent<TimerScript>().GetMinutes() >= 2 && gameController.GetComponent<TimerScript>().GetMinutes() < 3)
            {
                Instantiate(enemy2, spawner.transform.position,Quaternion.identity);
            }

            timerStart -= 0.008f;
            rotationSpeed += 0.1f;
            timer = timerStart;
            if(timerStart < .5f)
            {
                timerStart = .5f;
            }
        }
        if (gameController.GetComponent<TimerScript>().GetMinutes() == 3 && gameController.GetComponent<TimerScript>().GetSeconds() == 0 && bossSpawn)
        {
            Instantiate(boss, spawner.transform.position, Quaternion.identity);
            bossSpawn = false;
        }
    }
}
