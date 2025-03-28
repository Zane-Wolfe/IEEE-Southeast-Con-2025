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
        for (int i = ices.Count - 1; i >= 0; i--)
        {
            ices[i].Melt(Time.deltaTime);

            if (ices[i].IsMelted())
            {
                Destroy(ices[i].gameObject);
                ices.RemoveAt(i);
            }
        }
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