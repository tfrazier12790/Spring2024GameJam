using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float speed = .5f;
    [SerializeField] int health = 8;
    [SerializeField] Slider healthBar;
    [SerializeField] float damageScalar = 1.0f;
    GameObject player;
    GameObject scorekeeper;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<PlayerMovementScript>().TakeDamage(damageScalar * Time.deltaTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = health;
        player = GameObject.FindGameObjectWithTag("Player");
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z));
        healthBar.value = health;

        if(health <= 0)
        {
            scorekeeper.GetComponent<ScoreKeeper>().AddToScore(Random.Range(1, 4));
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
