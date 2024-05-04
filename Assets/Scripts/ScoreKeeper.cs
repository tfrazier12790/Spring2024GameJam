using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    [SerializeField] int score;
    [SerializeField] int playerFireStat = 1;
    [SerializeField] int fireCost = 30;
    [SerializeField] int playerWaterStat = 1;
    [SerializeField] int waterCost = 30;
    [SerializeField] int playerWindStat = 1;
    [SerializeField] int windCost = 30;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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
            fireCost = (int)Mathf.Round(8f + Mathf.Pow((float)playerFireStat, 1.5f) * 2f);
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
            waterCost = (int)Mathf.Round(8f + Mathf.Pow((float)playerWaterStat, 1.5f) * 2f);
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
            windCost = (int)Mathf.Round(8f + Mathf.Pow((float)playerWindStat, 1.5f) * 2f);
        }
    }
    public int GetWindStat()
    {
        return playerWindStat;
    }
    public int GetWindCost() {  return windCost; }
}
