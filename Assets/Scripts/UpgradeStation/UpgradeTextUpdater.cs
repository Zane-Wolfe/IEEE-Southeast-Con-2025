using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeTextUpdater : MonoBehaviour
{
    private enum UpgradeType {
        WalkSpeed, CarSpeed, PickQuality, ChiselQuality
    }
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] UpgradeType upgradeType;
    
    private int currentCost = 0;

    // Start is called before the first frame update
    void Start() {
        UpdateMoneyText();
        UpdateUpgradeText();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void UpdateMoneyText() {
        moneyText.text = gameData.playerMoney.ToString();
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
        int baseCost;
        int maxLevel = -1;
        if(upgradeType == UpgradeType.WalkSpeed) {
            currentLevel = gameData.walkSpeedLevel;
            baseCost = gameData.walkSpeedUpgradeCost;
            maxLevel = gameData.maxWalkingSpeed;

            // New Cost Formula
            if(currentLevel == 1) {
                currentCost = baseCost;
            } else {
                currentCost = currentCost - baseCost + (baseCost*2*(currentLevel-1)); // Update Formula Here
            }
        } else if(upgradeType == UpgradeType.CarSpeed) {
            currentLevel = gameData.carSpeedLevel;
            baseCost = gameData.carSpeedUpgradeCost;
            maxLevel = gameData.maxCarSpeed;

            // New Cost Formula
            if(currentLevel == 1) {
                currentCost = baseCost;
            } else {
                currentCost = currentCost - baseCost + (baseCost*2*(currentLevel-1)); // Update Formula Here
            }
        } else if(upgradeType == UpgradeType.PickQuality) {
            currentLevel = gameData.pickLevel;
            baseCost = gameData.pickUpgradeCost;
            maxLevel = gameData.maxPickLevel;

            // New Cost Formula
            if(currentLevel == 1) {
                currentCost = baseCost;
            } else {
                currentCost = currentCost - baseCost + (baseCost*2*(currentLevel-1)); // Update Formula Here
            }
        } else if(upgradeType == UpgradeType.ChiselQuality) {
            currentLevel = gameData.chiselLevel;
            baseCost = gameData.chiselUpgradeCost;
            maxLevel = 9999;

            // New Cost Formula
            if(currentLevel == 1) {
                currentCost = baseCost;
            } else {
                currentCost = currentCost - baseCost + (baseCost*2*(currentLevel-1)); // Update Formula Here
            }
        }

        // Update Level Text
        if(currentLevel == maxLevel) {
            levelText.text = "Max Level Reached";
            this.GetComponent<Button>().interactable = false;
        } else {
            levelText.text = "Lvl. " + currentLevel.ToString() + " -> Lvl. " + (currentLevel+1).ToString();
        }

        //Update Button Text
        buttonText.text = "$" + currentCost.ToString();
    }
}
