using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStation : MonoBehaviour, IInteractable
{
    [SerializeField] GameData gameData;
    [SerializeField] GameObject upgradesMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        upgradesMenu.SetActive(!upgradesMenu.activeSelf);
    }
}
