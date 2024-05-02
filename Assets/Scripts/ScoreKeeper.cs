using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    static int score;
    [SerializeField] int playerFireStat = 1;
    [SerializeField] int fireCost = 72;
    [SerializeField] int playerWaterStat = 0;
    [SerializeField] int waterCost = 50;
    [SerializeField] int playerWindStat = 0;
    [SerializeField] int windCost = 50;

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
    public void AddToFire()
    {
        if (score >= fireCost && playerFireStat < 10)
        {
            playerFireStat++;
            score -= fireCost;
            fireCost = (int)Mathf.Round(42f + Mathf.Pow((float)playerFireStat + 1f, 1.75f) * 9f);
        }
    }
    public int GetFireStat()
    {
        return playerFireStat;
    }
    public int GetFireCost() { return  fireCost; }
    public void AddToWater()
    {
        if (score >= waterCost && playerWaterStat < 10)
        {
            playerWaterStat++;
            score -= waterCost;
            waterCost = (int)Mathf.Round(42f + Mathf.Pow((float)playerWaterStat + 1f, 1.75f) * 9f);
        }
    }
    public int GetWaterStat()
    {
        return playerWaterStat;
    }
    public int GetWaterCost() { return waterCost; }
    public void AddToWind()
    {
        if (score >= windCost && playerWindStat < 10)
        {
            playerWindStat++;
            score -= windCost;
            windCost = (int)Mathf.Round(42f + Mathf.Pow((float)playerWindStat + 1f, 1.75f) * 9f);
        }
    }
    public int GetWindStat()
    {
        return playerWindStat;
    }
    public int GetWindCost() {  return windCost; }
}
