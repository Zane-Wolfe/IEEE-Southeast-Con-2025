using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStation : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject upgradesMenu;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(upgradesMenu.ToString());
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
