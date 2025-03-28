using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : Ice
{
    private float toolQualityMultiplier = 1.0f;
    
    private float sculptureQualityMultiplier = 1.0f;
    
    private bool isLockedOnTable = false;
    
    private float completionProgress = 0f;

    /// <summary>
    /// How much is this sculpture worth when sold at the shop?
    /// Sculpture value is affected by:
    /// - Base ice value and size (from parent class)
    /// - Tool quality used to create it
    /// - Overall sculpture quality
    /// - Sculpture melt progress
    /// </summary>
    /// <returns>The calculated sell value</returns>
    public override int GetSellValue()
    {
        // Calculate value based on all multipliers
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
            // Cannot pick up if locked to table
            return;
        }
        throw new System.NotImplementedException();
    }
}
