using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks player stats and handles upgrades and in-game currency (Dol).
/// Implements singleton pattern for global access.
/// </summary>

public class ScoreKeeper : MonoBehaviour
{
    // Static variable for referencing this script
    public static ScoreKeeper Instance;

    // Variables to store currency
    int dolCount;

    // Enum to represent all upgradeable player stats
    public enum StatType { Fire, Water, Wind }

    // Initialize stat values and costs
    private Dictionary<StatType, int> statLevels = new Dictionary<StatType, int>() 
    {
        {StatType.Fire, 1 },
        {StatType.Water, 1 },
        {StatType.Wind, 1 },
    };

    private Dictionary<StatType, int> upgradeCosts = new Dictionary<StatType, int>()
    {
        {StatType.Fire, 10 },
        {StatType.Water, 10 },
        {StatType.Wind, 10 },
    };

    private void Awake()
    {
        // Classic singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Add to dol (money) amount
    public void AddToDol(int dolToAdd) => dolCount += dolToAdd;

    // Get dol amount
    public int GetDol() => dolCount;

    // Gets current level of given stat
    public int GetStat(StatType stat) => statLevels[stat];

    // Gets current upgrade cost of given stat
    public int GetCost(StatType stat) => upgradeCosts[stat];

    // Attempt to upgrade specified stat if stat is not max and current dol is sufficient
    public void UpgradeStat(StatType stat)
    {
        if(dolCount >= upgradeCosts[stat] && statLevels[stat] < 10)
        {
            statLevels[stat]++;
            dolCount -= upgradeCosts[stat];
            upgradeCosts[stat] = Mathf.RoundToInt(8f + Mathf.Pow(statLevels[stat], 1.5f) * 2f);
        }
    }

    // Functions compatible with existing code
    public int GetFireStat() => GetStat(StatType.Fire);
    public int GetWaterStat() => GetStat(StatType.Water);
    public int GetWindStat() => GetStat(StatType.Wind);

    public int GetFireCost() => GetCost(StatType.Fire);
    public int GetWaterCost() => GetCost(StatType.Water);
    public int GetWindCost() => GetCost(StatType.Wind);

    public void AddToFire() => UpgradeStat(StatType.Fire);
    public void AddToWater() => UpgradeStat(StatType.Water);
    public void AddToWind() => UpgradeStat(StatType.Wind);
}
