using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float maxHealth = 10;
    [SerializeField] float health;
    [SerializeField] GameObject scorekeeper;
    [SerializeField] Slider healthBar;


    //[SerializeField] TMP_Text scoreText;
    bool isDead = false;
    public float speed = 2f;
    Vector2 location;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void AddHealth(float healthAdded)
    {
        health += healthAdded;
    }
    public bool PlayerStatus()
    {
        return isDead;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        location = transform.position;
        healthBar.maxValue = health;
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");

    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = string.Format("Dol: {0:D4}", scorekeeper.GetComponent<ScoreKeeper>().GetScore());
        if(Input.GetMouseButtonDown(0))
        {
            location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (transform.position.x < location.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (transform.position.x > location.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, location, speed * Time.deltaTime);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z));
        healthBar.value = health;

        if (health <= 0)
        {
            isDead = true;
        }
    }
}
