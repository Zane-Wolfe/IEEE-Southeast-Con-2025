using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : Ice
{
    private float toolQualityMultiplier = 1.0f;
    
    private float sculptureQualityMultiplier = 1.0f;
    
    private bool isLockedOnTable = false;

    /// <summary>
    /// Used to tell which sculpture model it is
    /// </summary>
    private int modelType = -1;
    
    private float completionProgress = 0f;

    public new int GetSellValue()
    {
        return Mathf.RoundToInt(baseValue * size * toolQualityMultiplier * sculptureQualityMultiplier);
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Pickup()
    {
        if(isLockedOnTable)
        {

        }
        throw new System.NotImplementedException();
    }
}
