using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTextUpdater : MonoBehaviour
{
    private enum UpgradeType {
        WalkSpeed, CarSpeed, PickQuality, ChiselQuality
    }
    [SerializeField] GameData gameData;
    [SerializeField] GameObject moneyTextObject;
    [SerializeField] GameObject levelTextObject;
    [SerializeField] GameObject buttonTextObject;
    [SerializeField] UpgradeType upgradeType;
    
    private TMPro.TextMeshProUGUI moneyText;
    private TMPro.TextMeshProUGUI levelText;
    private TMPro.TextMeshProUGUI buttonText;
    private int currentCost = -1;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = moneyTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        UpdateMoneyText();
        levelText = levelTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        buttonText = buttonTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        UpdateUpgradeText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateMoneyText() {
        moneyText.text = "Money:\n$" + gameData.playerMoney.ToString();
    }

    public void IncreaseLevel() {
        if(gameData.playerMoney >= currentCost) {
            if(upgradeType == UpgradeType.WalkSpeed) {
                gameData.walkSpeedLevel += 1;
            } else if(upgradeType == UpgradeType.CarSpeed) {
                gameData.carSpeedLevel += 1;
            } else if(upgradeType == UpgradeType.PickQuality) {
                gameData.pickLevel += 1;
            } else if(upgradeType == UpgradeType.ChiselQuality) {
                gameData.chiselLevel += 1;
            }
            gameData.playerMoney -= currentCost;
            UpdateUpgradeText();
            UpdateMoneyText();
        } else {
            // Do something if player does not have enough money to buy upgrade.
        }
        
    }

    private void UpdateUpgradeText() {
        int currentLevel = -1;
        int baseCost = -1;
        int maxLevel = -1;
        if(upgradeType == UpgradeType.WalkSpeed) {
            currentLevel = gameData.walkSpeedLevel;
            baseCost = gameData.walkSpeedUpgradeCost;
            maxLevel = gameData.maxWalkingSpeed;
        } else if(upgradeType == UpgradeType.CarSpeed) {
            currentLevel = gameData.carSpeedLevel;
            baseCost = gameData.carSpeedUpgradeCost;
            maxLevel = gameData.maxCarSpeed;
        } else if(upgradeType == UpgradeType.PickQuality) {
            currentLevel = gameData.pickLevel;
            baseCost = gameData.pickUpgradeCost;
            maxLevel = gameData.maxPickLevel;
        } else if(upgradeType == UpgradeType.ChiselQuality) {
            currentLevel = gameData.chiselLevel;
            baseCost = gameData.chiselUpgradeCost;
            maxLevel = 9999;
        }

        // Update Level Text
        if(currentLevel == maxLevel) {
            levelText.text = "Max Level Reached";
            this.GetComponent<Button>().interactable = false;
        } else {
            levelText.text = "Lvl. " + currentLevel.ToString() + " -> Lvl. " + (currentLevel+1).ToString();
        }

        //Update Button Text
        currentCost = baseCost * currentLevel; // Update formula here
        buttonText.text = "$" + currentCost.ToString();
    }
}
