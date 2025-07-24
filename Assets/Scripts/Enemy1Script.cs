using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls all behaviors of the simple enemy type, including movement, health,
/// the amount of dol the player receives when the enemy is defeated, any over time effects
/// the enemy has active including slow and damage over time, and dealing damage
/// to the player on contact.
/// </summary>

public class Enemy1Script : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 1.0f;

    [Header("Health")]
    [SerializeField] float health = 8;
    [SerializeField] Slider healthBar;
    [SerializeField] float damageScalar = 1.0f; // Value that damage gets multiplied by; higher values for stronger enemies
    [SerializeField] float healthBarOffset = 0.75f;

    [Header("Drops")]
    [SerializeField] int dolMin;
    [SerializeField] int dolMax;
    
    // Variables for Over Time effects
    float slowTimer;
    float dotTimer = 0;

    // Variables for references
    GameObject player;

    void Start()
    {
        // Find and store references to the player object
        player = GameObject.FindGameObjectWithTag("Player");

        // Initialize default values for health and speed
        healthBar.maxValue = health;
        speed = maxSpeed;
    }

    void Update()
    {
        // Position the health bar beneath the enemy and update its value
        healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - healthBarOffset, transform.position.z));
        healthBar.value = health;

        // Destroys the object if the enemy health reaches 0
        if(health <= 0)
        {
            ScoreKeeper.Instance.GetComponent<ScoreKeeper>().AddToDol(Random.Range(dolMin, dolMax));
            Destroy(gameObject);
        }

        // Apply a slow effect based on player stats while timer is active
        if (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            speed = maxSpeed / (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 2));
        } else
        {
            speed = maxSpeed;
        }

        // Apply damage over time based on player stats while timer is active
        if(dotTimer > 0)
        {
            dotTimer -= Time.deltaTime;
            health -= (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWindStat() / 5) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 5)) * Time.deltaTime;
        }

        // Updates the sprite to always look at the player
        if(transform.position.x < player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Move the enemy toward the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    // Public function to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Public function to apply a slow effect
    public void MakeSlow()
    {
        slowTimer = (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 2));
    }

    // Public function to apply a damage over time effect
    public void StartDot()
    {
        dotTimer = 2;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Deal contact damage to player over time
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerScript>().TakeDamage(damageScalar * Time.deltaTime);
        }
    }

    // Public function to get health value
    public float GetHealth() { return health; }
}
