using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : Ice
{    
    /// <summary>
    /// The type of sculpture this is. Int 0-5.
    /// </summary>
    public int Type { get; set; }
    
    /// <summary>
    /// The quality/value multiplier of the tool used to create the sculpture.
    /// </summary>
    public float SculptureQualityMultiplier { get; set; } = 1.0f;
    
    /// <summary>
    /// Copies all properties from an Ice object to this sculpture.
    /// </summary>
    /// <param name="ice">The Ice object to copy properties from.</param>
    public void CopyFromIce(Ice ice)
    {
        BaseValue = ice.BaseValue;
        BaseSize = ice.BaseSize;
        Size = ice.Size;
        CurrentMeltRate = ice.CurrentMeltRate;
        ToolQualityMultiplier = ice.ToolQualityMultiplier;
    }

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
        return Mathf.RoundToInt(BaseValue * Size * ToolQualityMultiplier * SculptureQualityMultiplier);
    }

    /// <summary>
    /// Unlocks the sculpture from the table.
    /// </summary>
    public void UnlockFromTable()
    {
        IsLockedOnTable = false;
    }

    public override void Interact()
    {
        // Do nothing, Pickup() handles this logic 
    }

    public override void Pickup()
    {
        // Do nothing, Interact() handles this logic
    }
}
