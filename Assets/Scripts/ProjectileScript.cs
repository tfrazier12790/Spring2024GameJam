using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages projectile behavior including lifetime and player collision damage
/// </summary>

public class ProjectileScript : MonoBehaviour
{
    float damage = .5f;
    float lifetime = 5f;

    private void Start()
    {
        // Destroy projectile adter lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to player on collision and destroy object
            collision.GetComponent<PlayerScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
