using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    static int score;

    private void Awake()
    {
        instance = this; 
        DontDestroyOnLoad(gameObject);
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
    public int GetScore()
    {
        return score;
    }
}
