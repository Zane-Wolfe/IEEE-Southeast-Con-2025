using UnityEngine;
using System.Collections;


/// <summary>
/// A table that can sell Ice and Sculpture objects, adding their sell value to the player's money after a delay.
/// </summary>
public class SellingTable : BaseTable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private MarketValueHandler marketValueHandler;
    [SerializeField] private float minSellDelay = 1f;
    [SerializeField] private float maxSellDelay = 3f;
    [SerializeField] private AudioClip[] hahaSounds; // Array of haha sound effects
    private AudioSource audioSource;
    private Coroutine sellCoroutine;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

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
            int baseSellValue = currentIce.GetSellValue();
            int finalSellValue = baseSellValue;

            // Apply market multiplier if the object is a sculpture
            if (currentIce is Sculpture sculpture)
            {
                float multiplier = MarketValueHandler.Instance.GetMultiplier(sculpture.Type);
                finalSellValue = Mathf.RoundToInt(baseSellValue * multiplier);
                
                // Notify MarketValueHandler about the sale
                MarketValueHandler.Instance.OnSculptureSold(sculpture.Type);
            }

            gameData.playerMoney += finalSellValue;
            
            // Play a random haha sound effect
            if (hahaSounds != null && hahaSounds.Length > 0)
            {
                AudioClip randomSound = hahaSounds[Random.Range(0, hahaSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }
            
            RemoveIce();
            Destroy(currentIce.gameObject);

            // TODO: Add particle effect
            // TODO: Add animation
            // TODO: Add text popup
        }
    }
}
