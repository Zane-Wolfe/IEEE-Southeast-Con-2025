using UnityEngine;

/// <summary>
/// A table that can sell Ice and Sculpture objects, adding their sell value to the player's money.
/// </summary>
public class SellingTable : BaseTable
{
    [SerializeField] private GameData gameData;

    /// <summary>
    /// Overrides the Interact method to handle selling the current Ice or Sculpture object.
    /// Uses the overridden GetSellValue method from each object type to determine the value.
    /// </summary>
    public override void Interact()
    {
        Ice currentIce = GetCurrentIce();
        if (currentIce != null)
        {
            // Ice and Sculpture both have GetSellValue() method where sculpture has additional is overridden
            int sellValue = currentIce.GetSellValue();
            
            gameData.playerMoney += sellValue;
            
            RemoveIce();
            Destroy(currentIce.gameObject);
        }
    }
}
