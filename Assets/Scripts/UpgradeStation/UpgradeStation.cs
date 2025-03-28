using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStation : MonoBehaviour, IInteractable
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject upgradesMenu;
    [SerializeField] GameObject moneyTextObject;

    private TMPro.TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        // upgradesMenu.SetActive(false);
        moneyText = moneyTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        moneyText.text = "Money:\n$" + gameData.playerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Entered Upgrade Station");
        upgradesMenu.SetActive(true);
    }
}
