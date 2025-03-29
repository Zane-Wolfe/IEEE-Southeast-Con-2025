using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the melting of ice objects in the game.
/// </summary>
public class IceMeltManager : MonoBehaviour
{
    /// <summary>
    /// The singleton instance of the IceMeltManager.
    /// </summary>
    public static IceMeltManager Instance { get; private set; }

    /// <summary>
    /// The list of ice objects that are currently melting.
    /// </summary>
    private List<Ice> ices = new List<Ice>();

    /// <summary>
    /// List of ice objects to be destroyed in the next frame.
    /// </summary>
    private List<Ice> icesToDestroy = new List<Ice>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Advances the melting process for all ice objects.
    /// </summary>
    private void Update()
    {
        // Process melting for all ice objects
        for (int i = ices.Count - 1; i >= 0; i--)
        {
            Ice ice = ices[i];
            if (ice == null)
            {
                ices.RemoveAt(i);
                continue;
            }

            ice.Melt(Time.deltaTime);

            if (ice.IsMelted())
            {
                icesToDestroy.Add(ice);
                ices.RemoveAt(i);
            }
        }

        // Destroy melted ice objects
        foreach (Ice ice in icesToDestroy)
        {
            if (ice != null)
            {
                Destroy(ice.gameObject);
            }
        }
        icesToDestroy.Clear();
    }

    /// <summary>
    /// Adds an ice object to be melted.
    /// </summary>
    /// <param name="ice">The ice object to add.</param>
    public void AddIce(Ice ice)
    {
        if (!ices.Contains(ice))
        {
            ices.Add(ice);
        }
    }

    /// <summary>
    /// Removes an ice object from the melting process.
    /// </summary>
    /// <param name="ice">The ice object to remove.</param>
    public void RemoveIce(Ice ice)
    {
        ices.Remove(ice);
    }
}