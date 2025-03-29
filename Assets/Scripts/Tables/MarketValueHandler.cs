using UnityEngine;
using System;
using System.Collections.Generic;

public class MarketValueHandler : MonoBehaviour
{
    public static MarketValueHandler Instance { get; private set; }

    private Dictionary<int, float> marketMultipliers = new Dictionary<int, float>();
    private Dictionary<int, List<float>> marketMultipliersHistory = new Dictionary<int, List<float>>();
    private const float MIN_MULTIPLIER = 0.2f;
    private const float MAX_MULTIPLIER = 1.8f;
    private const float SALE_REDUCTION = 0.10f;
    private const int MARKET_OPEN_HOUR = 9;
    private const int MARKET_CLOSE_HOUR = 17;
    [SerializeField] private nighty ingameTime;
    [SerializeField] private PlayerController playerController;
    private int lastUpdatedHour = -1;

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
        lastUpdatedHour = -1;
        List<float> marketHistoryZero = new List<float>();
        for(int i = 0; i < 24; i++)
        {
            marketHistoryZero.Add(0);
        }
        // Initialize multipliers for each sculpture type
        for (int i = 0; i < 6; i++)
        {
            marketMultipliers[i] = 1.0f;
            marketMultipliersHistory[i] = marketHistoryZero;
        }
    }

    private void Update()
    {
        if (IsMarketOpen() && ingameTime.getCurrentHourOfDay() != lastUpdatedHour)
        {
            lastUpdatedHour = ingameTime.getCurrentHourOfDay();
            UpdateAllMultipliers();
        }
        // Show at 5pm
        if (ingameTime.getCurrentHourOfDay() == 17)
        {
            displayGraphs();
        }
    }

    [SerializeField] RectTransform stockPanel;
    // There are 6 of these
    [SerializeField] List<RectTransform> stocks;
    [SerializeField] GameObject currentStatue;
    
    private void displayGraphs()
    {
        playerController.freezePlayer = true;
        stockPanel.gameObject.SetActive(true);
        stocks[0].gameObject.SetActive(true);
        // SET STATUE HERE
        // currentStatue = 
    }
    int currentStockViewing = 0;
    public void clickNextStock()
    {
        
        stocks[currentStockViewing].gameObject.SetActive(false);
        currentStockViewing++;
        // Close Stock menu
        if(currentStockViewing == 6)
        {
            stockPanel.gameObject.SetActive(false);
            playerController.freezePlayer = false;
            currentStockViewing = 0;
            return;
        }
        LineRendererHUD lrh = stocks[currentStockViewing].gameObject.GetComponent<LineRendererHUD>();
        lrh.setGraphData(marketMultipliersHistory[currentStockViewing]);

        stocks[currentStockViewing].gameObject.SetActive(true);
        // SET NEXT STATUE HERE
        // currentStatue = 

    }

    private bool IsMarketOpen()
    {
        int currentHour = ingameTime.getCurrentHourOfDay();
        return currentHour >= MARKET_OPEN_HOUR && currentHour < MARKET_CLOSE_HOUR;
    }

    private void UpdateAllMultipliers()
    {
        for (int i = 0; i < 6; i++)
        {
            float newMultiplier = (float)(UnityEngine.Random.Range(0, 2) * (MAX_MULTIPLIER - MIN_MULTIPLIER) + MIN_MULTIPLIER);
            marketMultipliers[i] = newMultiplier;

            marketMultipliersHistory[i].RemoveAt(0);
            marketMultipliersHistory[i].Add(newMultiplier);
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