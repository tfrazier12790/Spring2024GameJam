using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls all behaviors of the ranged enemy type, including movement, health,
/// the amount of dol the player receives when the enemy is defeated, any over time effects
/// the enemy has active including slow and damage over time, and dealing damage
/// to the player on contact as well as shooting a projectile at the player.
/// </summary>

public class Enemy2Script : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 1.0f;
    [SerializeField] float speed;

    [Header("Health")]
    [SerializeField] float health = 8;
    [SerializeField] Slider healthBar;
    [SerializeField] float damageScalar = 1.0f;
    [SerializeField] float healthBarOffset = 0.75f;

    [Header("Drops")]
    [SerializeField] int dolMin;
    [SerializeField] int dolMax;

    [Header("Ranged Attack Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] float shootCooldown = 3f;
    [SerializeField] float attackRange = 6f;
    [SerializeField] float projectileSpeed = 6f;
    float shootTimer = 0f;

    // Variables for over time effects
    float timer;
    float dotTimer = 0;

    // Variables for references
    GameObject player;

    // Start is called before the first frame update
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

        // Destroy this object if the enemy health reaches 0
        if (health <= 0)
        {
            ScoreKeeper.Instance.GetComponent<ScoreKeeper>().AddToDol(Random.Range(dolMin, dolMax));
            Destroy(gameObject);
        }

        // Apply a slow effect based on player stats while timer is active
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            speed = maxSpeed / (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 2));
        }
        else
        {
            speed = maxSpeed;
        }

        // Apply damage over time based on player stats while timer is active
        if (dotTimer > 0)
        {
            dotTimer -= Time.deltaTime;
            health -= (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWindStat() / 5) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 5)) * Time.deltaTime;
        }

        // Updates the sprite to always look at the player
        if (transform.position.x < player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Find distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // If the player is not within range, move toward the player
        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        } 
        // If the player is in attack range, fire at the player
        else if(distanceToPlayer < attackRange)
        {
            shootTimer-= Time.deltaTime;
            if (shootTimer <= 0)
            {
                ShootAtPlayer();
                shootTimer = shootCooldown;
            }
        }
    }

    // Public function to reduce health
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Public function to apply a slow effect
    public void MakeSlow()
    {
        timer = (((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)ScoreKeeper.Instance.GetComponent<ScoreKeeper>().GetFireStat() / 2));
        Debug.Log("MadeSlow");
    }

    // Public function to apply a damage over time effect
    public void StartDot()
    {
        dotTimer = 2;
        Debug.Log("DoT Started");
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

    void ShootAtPlayer()
    {
        // Instantiate a projectile and fire it toward the player
        Vector2 direction = (player.transform.position - firingPoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
