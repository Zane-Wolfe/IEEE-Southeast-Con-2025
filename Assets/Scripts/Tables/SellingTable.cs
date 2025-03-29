using UnityEngine;
using System.Collections;

/// <summary>
/// A table that can sell Ice and Sculpture objects, adding their sell value to the player's money after a delay.
/// </summary>
public class SellingTable : BaseTable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private float minSellDelay = 1f;
    [SerializeField] private float maxSellDelay = 3f;
    private Coroutine sellCoroutine;

    /// <summary>
    /// Overrides CanPlaceIce to accept both Ice and Sculpture objects.
    /// </summary>
    public override bool CanPlaceIce(Ice ice)
    {
        if (playerController.GetHeldItem() == ice.gameObject) 
        {
            return false;
        }
        return currentIce == null;
    }

    /// <summary>
    /// Overrides PlaceIce to start the selling process when an object is placed.
    /// </summary>
    public override void PlaceIce(Ice ice)
    {
        if (CanPlaceIce(ice))
        {
            base.PlaceIce(ice);
            
            // Start the selling process
            if (sellCoroutine != null)
            {
                StopCoroutine(sellCoroutine);
            }
            sellCoroutine = StartCoroutine(SellObjectAfterDelay());
        }
    }

    /// <summary>
    /// Overrides RemoveIce to stop the selling process if the object is removed.
    /// </summary>
    public override Ice RemoveIce()
    {
        if (sellCoroutine != null)
        {
            StopCoroutine(sellCoroutine);
            sellCoroutine = null;
        }
        return base.RemoveIce();
    }

    /// <summary>
    /// Coroutine that sells the object after a random delay between minSellDelay and maxSellDelay.
    /// </summary>
    private IEnumerator SellObjectAfterDelay()
    {
        float delay = Random.Range(minSellDelay, maxSellDelay);
        yield return new WaitForSeconds(delay);

        Ice currentIce = GetCurrentIce();
        if (currentIce != null)
        {
            int sellValue = currentIce.GetSellValue();
            gameData.playerMoney += sellValue;
            
            RemoveIce();
            Destroy(currentIce.gameObject);

            // TODO: Add particle effect
            // TODO: Add sound effect
            // TODO: Add animation
            // TODO: Add text popup
        }
    }
}
