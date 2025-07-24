using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns enemies at a random location at a certain radius around the player. The type of enemy spawned
/// and the amount spawned is determined by the amount of time passed, and also handles the one-off event of a wave of enemies
/// spawning surrounding the player. Spawns three types of enemies and a boss type.
/// </summary>

public class EnemySpawnerScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject gameController;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject boss;
    TimerScript timerScript;

    // Timer variables for spawning enemies
    float timerStart = 2f;
    float timer = 0;

    // Flags for one-time event triggers
    bool bossSpawn = true;
    bool surroundHasTriggered = false;

    void Start()
    {
        // Initialize references to player, game controller, and timer script
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        timerScript = gameController.GetComponent<TimerScript>();
    }

    void Update()
    {
        // Link spawner position to player position
        transform.position = player.transform.position;

        // Update timer until it reaches zero
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {   
            // When timer reaches 0:
            // Randomly rotate the spawner to randomize spawn location
            float angle = Random.Range(0f, 360f);
            transform.eulerAngles = new Vector3(0f, 0f, angle);

            // Get elapsed minutes from timer
            int minutes = timerScript.GetMinutes();

            if (minutes < 1)
            {
                // Minute 1: Spawn 1 weak enemy
                SpawnEnemyAtEdge(enemy1, spawner.transform.position);
            } else if (minutes < 2)
            {
                // Minute 2: Spawn 1 strong enemy or pack of weak enemies
                if(Random.value < 0.5f) {
                    SpawnEnemyAtEdge(enemy2, spawner.transform.position);
                } else
                {
                    SpawnPack(enemy1, 5, spawner.transform.position);
                }
            } else if (minutes < 3)
            {
                // Minute 3: Spawn 1 Ranged enemy or pack of strong enemies
                if (Random.value < 0.5)
                {
                    SpawnEnemyAtEdge(enemy3, spawner.transform.position);
                }
                else
                {
                    SpawnPack(enemy2, 5, spawner.transform.position);
                }
            }

            // Decrease spawn delay and reset timer
            timerStart -= 0.008f;
            timer = timerStart;

            // Clamp minimum spawn delay to .5 seconds
            if(timerStart < .5f)
            {
                timerStart = .5f;
            }
        }

        // At the 2 minute, surround player with weak enemies
        if (timerScript.GetMinutes() == 2 && surroundHasTriggered == false)
        {
            SurroundPlayerWithEnemies(enemy1);
            surroundHasTriggered = true;
        }

        // At the 3 minute mark, spawn boss
        if (timerScript.GetMinutes() == 4 && timerScript.GetSeconds() == 0 && bossSpawn)
        {
            timerScript.SetBoss(Instantiate(boss, spawner.transform.position, Quaternion.identity));
            bossSpawn = false;
        }
    }

    // Function to spawn a pack of enemies with the given amount at the given position
    private void SpawnPack(GameObject enemyPrefab, int amount, Vector3 position)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }

    // Function to spawn a single enemy at the given position
    private void SpawnEnemyAtEdge(GameObject enemyPrefab, Vector3 position)
    {
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    public void SurroundPlayerWithEnemies(GameObject enemyPrefab)
    {
        // Get references to the camera, and find the position of the center of the camera
        Camera cam = Camera.main;
        Vector3 camCenter = cam.transform.position;

        // Calculate the height and width of the camera view
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Get positions just off screen in each direction
        float offset = 1.5f;
        float left = camCenter.x - camWidth / 2 - offset;
        float right = camCenter.x + camWidth / 2 + offset;
        float top = camCenter.y + camHeight / 2 + offset;
        float bottom = camCenter.y - camHeight / 2 - offset;

        // Get exact positions just off screen based on the amount of enemies to spawn
        float xStep = camWidth / 31; // A step of 31 will spawn 32 enemies on the top and bottom of screen
        float yStep = camHeight / 17; // A step of 17 will spawn 18 enemies on left and right of screen

        // Spawn the requisite amount of enemies around the edge of the screen
        for (int i = 0; i < 32; i++)
        {
            float x = left + xStep * i;
            Instantiate(enemyPrefab, new Vector3(x, top, 0), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(x, bottom, 0), Quaternion.identity);
        }

        for (int i = 0; i < 18; i ++)
        {
            float y = bottom + yStep * i;
            Instantiate(enemyPrefab, new Vector3(left, y, 0), Quaternion.identity);
            Instantiate(enemyPrefab, new Vector3(right, y, 0), Quaternion.identity);
        }
    }
}
