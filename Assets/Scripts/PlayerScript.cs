using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls player movement, health management, and death state.
/// </summary>

public class PlayerScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider healthBar;

    // Variables for health
    float maxHealth = 10;
    float health;

    // Variables for movement
    public float speed = 2f;
    Vector2 moveToLocation;

    // Flag for tracking player death
    bool isDead = false;

    void Start()
    {
        // Set initial health and bar values, and prevent movement before input
        health = maxHealth;
        moveToLocation = transform.position;  // Done to keep the player from moving prematurely
        healthBar.maxValue = health;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Update destination to the clicked mouse position
            moveToLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Move player toward target location 
        transform.position = Vector2.MoveTowards(transform.position, moveToLocation, speed * Time.deltaTime);

        // Flips sprite horizontally based on movement direction
        if (transform.position.x < moveToLocation.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (transform.position.x > moveToLocation.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // Set health bar value and position below player
        healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z));
        healthBar.value = health;

        // Mark player as dead if health hits 0
        if (health <= 0)
        {
            isDead = true;
        }

        // Clamp health to maximum value
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    // Reduce health by given amount
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    // Increase health by given amount
    public void AddHealth(float healthAdded)
    {
        health += healthAdded;
    }

    // Returns whether player is alive or dead
    public bool IsPlayerDead()
    {
        return isDead;
    }
}
