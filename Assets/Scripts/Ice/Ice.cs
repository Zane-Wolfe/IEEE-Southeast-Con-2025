using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour, IInteractable, IPickupable 
{
    /// <summary>
    /// The base quality/value of the ice, determined by its source location.
    /// </summary>
    protected int baseValue;

    /// <summary>
    /// The initial size of the ice when created.
    /// Values typically range from 0.8 to 1.2
    /// </summary>
    protected float baseSize;

    /// <summary>
    /// The current size multiplier of the ice, affected by melting.
    /// Values range from 1.0 (full size) to 0.0 (fully melted).
    /// </summary>
    protected float size;

    /// <summary>
    /// The rate at which the size multiplier decreases per second due to melting.
    /// A value of 0.0f means the ice is not currently melting.
    /// </summary>
    protected float currentMeltRate = 0.10f;

    /// <summary>
    /// The quality/value multiplier of the tool used to create the ice.
    /// </summary>
    protected float toolQualityMultiplier = 1.0f;

    /// <summary>
    /// The MeshFilter component attached to the ice object.
    /// </summary>
    protected MeshFilter meshFilter;

    /// <summary>
    /// Creates the ice object with a specified base value and size.
    /// </summary>
    /// <param name="value">The base quality/value of the ice.</param>
    /// <param name="size">The initial size/scale of the ice.</param>
    public void CreateIce(int value, float size) 
    {
        baseValue = value;
        baseSize = size;
        this.size = size;
        UpdateMeshScale();
    }

    /// <summary>
    /// Creates the ice object with a random size/scale between 0.8 and 1.2.
    /// </summary>
    /// <param name="baseValue">The base quality/value of the ice.</param>
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
