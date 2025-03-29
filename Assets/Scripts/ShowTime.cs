using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShowTime : MonoBehaviour
{
    public GameData gameData;
    public TMP_Text time;
    
    // Update is called once per frame
    void Update()
    {
        string text = "";
        int h = gameData.hour;
        int m = gameData.minute;
        if (h < 10)
        {
            text += "0" + h.ToString();
        }
        else
        {
            text += h.ToString();
        }
        text += ":";
        
        if (m < 10)
        {
            text += "0" + m.ToString();
        }
        else
        {
            text += m.ToString();
        }
        time.text = text;
    }
}
