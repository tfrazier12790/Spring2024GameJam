using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] float expansionSpeed = 10.0f;
    [SerializeField] Vector3 finishedSize = new Vector3 (2, 2, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, finishedSize, expansionSpeed * Time.deltaTime);
        if(Vector3.Equals(transform.localScale, finishedSize))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Happened");
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
            collision.gameObject.SendMessage("TakeDamage", 3);
        }
    }
}
