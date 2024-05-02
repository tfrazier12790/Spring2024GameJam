using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindShearScript : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject scorekeeper;
    [SerializeField] Vector3 endPosition;
    [SerializeField] float timer;
    [SerializeField] float lookAngle;
    [SerializeField] Quaternion lookRot;

    void Start()
    {
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
        timer = ((float)scorekeeper.GetComponent<ScoreKeeper>().GetWindStat() / 5) + 1;
        endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(endPosition.y - transform.position.y, endPosition.x - transform.position.x) * Mathf.Rad2Deg;
        lookRot = Quaternion.Euler(new Vector3(0, 0, lookAngle));
        transform.localScale = new Vector3(.5f, scorekeeper.GetComponent<ScoreKeeper>().GetWindStat() * 0.75f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime, 0);
        transform.position += transform.TransformDirection(new Vector3(speed * Time.deltaTime, 0, 0));
        transform.rotation = lookRot;
        if (timer > 0) { 
            timer -= Time.deltaTime; 
        } else {
            Debug.Log(transform.rotation);
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Happened");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
        }
    }
}
