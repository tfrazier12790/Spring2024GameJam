using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTideScript : MonoBehaviour
{
    [SerializeField] float expansionSpeed = 10.0f;
    [SerializeField] Vector3 finishedSize;

    [SerializeField] GameObject scorekeeper;
    // Start is called before the first frame update
    void Start()
    {
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper");
        finishedSize = new Vector3(((float)scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() / 6) + ((float)scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() / 6) + 1, 
                                   ((float)scorekeeper.GetComponent<ScoreKeeper>().GetWaterStat() / 6) + ((float)scorekeeper.GetComponent<ScoreKeeper>().GetFireStat() / 6) + 1,
                                   1);
        transform.localScale = finishedSize;
        Debug.Log(finishedSize);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, expansionSpeed * Time.deltaTime);
        if (Vector3.Equals(transform.localScale, Vector3.zero))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Happened");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
            other.gameObject.SendMessage("MakeSlow");
        }
    }
}
