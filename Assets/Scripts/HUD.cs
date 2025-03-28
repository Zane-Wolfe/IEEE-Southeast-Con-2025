using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HUD : MonoBehaviour
{
    public GameData gameData;
    public TMP_Text money;

    // Update is called once per frame
    void Update()
    {
        money.text = gameData.playerMoney.ToString();
    }
}
