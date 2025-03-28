using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : Ice
{    
    /// <summary>
    /// The quality/value multiplier of the tool used to create the sculpture.
    /// </summary>
    private float sculptureQualityMultiplier = 1.0f;
    
    /// <summary>
    /// Whether the sculpture is locked to a table.
    /// </summary>
    private bool isLockedOnTable = false;

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
