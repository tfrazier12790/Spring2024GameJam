using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float maxSpeed = 1.0f;
    [SerializeField] float speed;
    [SerializeField] float health = 8;
    [SerializeField] Slider healthBar;
    [SerializeField] float damageScalar = 1.0f;
    [SerializeField] float healthBarOffset = 0.75f;
    [SerializeField] int dolMin;
    [SerializeField] int dolMax;
    //[SerializeField] bool slow = false;
    [SerializeField] float timer;
    [SerializeField] float dotTimer = 0;
    GameObject player;
    GameObject scorekeeper;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void MakeSlow()
    {
        timer = (((float)scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() / 2));
        Debug.Log("MadeSlow");
    }
    public void StartDot()
    {
        dotTimer = 2;
        Debug.Log("DoT Started");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.GetComponent<PlayerMovementScript>().TakeDamage(damageScalar * Time.deltaTime);
        }
    }

    public float GetHealth() { return health; }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = health;
        player = GameObject.FindGameObjectWithTag("Player");
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - healthBarOffset, transform.position.z));
        healthBar.value = health;

        if(health <= 0)
        {
            scorekeeper.GetComponent<ScoreKeeper>().AddToScore(Random.Range(dolMin, dolMax));
            Destroy(gameObject);
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            speed = maxSpeed / (((float)scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() / 2) + ((float)scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() / 2));
        } else
        {
            speed = maxSpeed;
        }

        if(dotTimer > 0)
        {
            dotTimer -= Time.deltaTime;
            health -= (((float)scorekeeper.GetComponent<ScoreKeeper>().GetWindStat() / 5) + ((float)scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() / 5)) * Time.deltaTime;
        }

        if(transform.position.x < player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (transform.position.x > player.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
