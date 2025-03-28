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
    [SerializeField] GameObject levelTextObject;
    [SerializeField] GameObject buttonTextObject;
    [SerializeField] UpgradeType upgradeType;
    
    private TMPro.TextMeshProUGUI levelText;
    private TMPro.TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = levelTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        buttonText = buttonTextObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseLevel() {
        if(upgradeType == UpgradeType.WalkSpeed) {
            
        } else if(upgradeType == UpgradeType.CarSpeed) {
            
        } else if(upgradeType == UpgradeType.PickQuality) {
            
        } else if(upgradeType == UpgradeType.ChiselQuality) {
            
        }
    }

    private void UpdateUpgradeText() {
        int currentLevel = -1;
        int currentCost = -1;
        int maxLevel = -1;
        if(upgradeType == UpgradeType.WalkSpeed) {
            currentLevel = gameData.walkSpeedLevel;
            currentCost = gameData.walkSpeedUpgradeCost;
            maxLevel = gameData.maxWalkingSpeed;
        } else if(upgradeType == UpgradeType.CarSpeed) {
            currentLevel = gameData.carSpeedLevel;
            currentCost = gameData.carSpeedUpgradeCost;
            maxLevel = gameData.maxCarSpeed;
        } else if(upgradeType == UpgradeType.PickQuality) {
            currentLevel = gameData.pickLevel;
            currentCost = gameData.pickUpgradeCost;
            maxLevel = gameData.maxPickLevel;
        } else if(upgradeType == UpgradeType.ChiselQuality) {
            currentLevel = gameData.chiselLevel;
            currentCost = gameData.chiselUpgradeCost;
            maxLevel = 9999;
        }
        if(currentLevel == maxLevel) {
            levelText.text = "Max Level Reached";
            this.GetComponent<Button>().interactable = false;
        } else {
            levelText.text = "Lvl. " + currentLevel.ToString() + " -> Lvl. " + (currentLevel+1).ToString();
        }
    }
}
