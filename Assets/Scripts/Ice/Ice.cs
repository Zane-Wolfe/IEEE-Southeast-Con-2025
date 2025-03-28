using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour, IInteractable, IPickupable 
{
    // Ice Location base Quality from 1 to 10
    protected int baseValue;

    // size of the ice vended 
    // values from 0.8 to 1.2 ish
    protected float baseSize;

    // size modifier due to melting
    // values range from 1.0 to 0.0
    protected float size;

    // how much the size modifier should be decreased per second
    protected float currentMeltRate = 0.0f;

    protected MeshFilter meshFilter;

    public void CreateIce(int value, float size) 
    {
        baseValue = value;
        baseSize = size;
        this.size = size;
        UpdateMeshScale();
    }

    public void CreateIceWithRandomSize(int baseValue)
    {
        CreateIce(baseValue, Random.Range(0.8f, 1.2f));
    }

    void Start()
    {
        // Get the MeshFilter component
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogWarning("No MeshFilter component found on Ice object!");
        }
    }

    void Update()
    {
        // Update the mesh scale if melting
        if (currentMeltRate > 0)
        {
            UpdateMeshScale();
        }
    }

    /// <summary>
    /// Updates the mesh scale based on the current size
    /// </summary>
    protected void UpdateMeshScale()
    {
        if (meshFilter != null)
        {
            transform.localScale = Vector3.one * size;
        }
    }

    /// <summary>
    /// Melts the ice object by its currentMeltRate normalized by deltaTime
    /// </summary>
    public void Melt(float deltaTime)
    {
        size = size - (currentMeltRate * deltaTime);
        UpdateMeshScale();
    }

    /// <summary>
    /// Sets the rate of size removal by Melt()
    /// </summary>
    /// <param name="rate"></param>
    public void SetMeltRate(float rate)
    {
        currentMeltRate = rate;
    }

    /// <summary>
    /// Returns whether or not the ice should be destroyed
    /// </summary>
    /// <returns></returns>
    public bool IsMelted()
    {
        return size <= 0.0f;
    }

    /// <summary>
    /// How much is this worth when sold at the shop?
    /// Base implementation calculates value based on baseValue and current size
    /// </summary>
    /// <returns>The calculated sell value</returns>
    public virtual int GetSellValue()
    {
        // melting ice will reduce its value
        return Mathf.RoundToInt(baseValue * size);
    }

    public void Interact()
    {
        // Do nothing, Pickup() handles this logic 
    }

    public void Pickup()
    {
        // Run any logic here when picked up
        Debug.Log("Ice picked up");
    }
}
