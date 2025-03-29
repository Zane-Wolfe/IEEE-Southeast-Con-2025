using UnityEngine;
using System;
using System.Collections.Generic;

public class MarketValueHandler : MonoBehaviour
{
    public static MarketValueHandler Instance { get; private set; }

    private Dictionary<int, float> marketMultipliers = new Dictionary<int, float>();
    private System.Random random = new System.Random();
    private const float MIN_MULTIPLIER = 0.2f;
    private const float MAX_MULTIPLIER = 1.8f;
    private const float SALE_REDUCTION = 0.10f;
    private const int MARKET_OPEN_HOUR = 9;
    private const int MARKET_CLOSE_HOUR = 17;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Initialize multipliers for each sculpture type
        for (int i = 0; i < 6; i++)
        {
            marketMultipliers[i] = 1.0f;
        }

        // Start the hourly update coroutine
        StartCoroutine(UpdateMarketValues());
    }

    private System.Collections.IEnumerator UpdateMarketValues()
    {
        while (true)
        {
            // Wait for 1 hour in game time
            yield return new WaitForSeconds(3600f); // Assuming 1 hour = 3600 seconds in game time

            // Check if market is open
            if (IsMarketOpen())
            {
                UpdateAllMultipliers();
            }
        }
    }

    private bool IsMarketOpen()
    {
        int currentHour = DateTime.Now.Hour;
        return currentHour >= MARKET_OPEN_HOUR && currentHour < MARKET_CLOSE_HOUR;
    }

    private void UpdateAllMultipliers()
    {
        for (int i = 0; i < 6; i++)
        {
            float newMultiplier = (float)(random.NextDouble() * (MAX_MULTIPLIER - MIN_MULTIPLIER) + MIN_MULTIPLIER);
            marketMultipliers[i] = newMultiplier;
        }
    }

    public float GetMultiplier(int type)
    {
        return marketMultipliers[type];
    }

    public void OnSculptureSold(int type)
    {
        if (marketMultipliers.ContainsKey(type))
        {
            marketMultipliers[type] = Mathf.Max(MIN_MULTIPLIER, marketMultipliers[type] - SALE_REDUCTION);
        }
    }
}