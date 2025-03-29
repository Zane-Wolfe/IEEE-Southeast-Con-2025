using System;
using UnityEngine;

/// <summary>
/// A table that allows the player to transform Ice objects into Sculpture objects.
/// </summary>
public class WorkingTable : BaseTable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Mesh[] sculptureMeshes; // Array of possible sculpture meshes
    [SerializeField] private PlayerInteractHandler playerInteractHandler;
    private int hitCount = 0;
    private const int HITS_REQUIRED = 5;

    /// <summary>
    /// Overrides the Interact method to handle the sculpture conversion
    /// After HITS_REQUIRED hits the ice is replaced with a sculpture.
    /// </summary>
    public override void Interact()
    {
        Debug.Log("Interacting with Working Table");
        Ice currentIce = GetCurrentIce();
        if (currentIce != null && !(currentIce is Sculpture))
        {
            hitCount++;
            Debug.Log($"Hit {hitCount}/{HITS_REQUIRED}");

            if (hitCount >= HITS_REQUIRED)
            {
                TransformIceToSculpture(currentIce);
                hitCount = 0;
            }
        }
    }

    /// <summary>
    /// Overrides the RemoveIce method to ensure ice can only be removed after it has been hit 5 times.
    /// </summary>
    /// <returns>The Ice object if it can be removed, null otherwise.</returns>
    public override Ice RemoveIce()
    {
        if (currentIce != null && hitCount >= HITS_REQUIRED)
        {
            Ice iceToReturn = currentIce;
            currentIce = null;
            hitCount = 0; // Reset hit count
            iceToReturn.transform.SetParent(null);
            return iceToReturn;
        }
        return null;
    }

    /// <summary>
    /// Transforms an Ice object into a Sculpture object.
    /// The sculpture inherits the ice's properties and adds sculpture multipliers.
    /// </summary>
    /// <param name="ice">The Ice object to transform.</param>
    private void TransformIceToSculpture(Ice ice)
    {
        Debug.Log("Transforming Ice to Sculpture");
        
        // Remove the ice from the melt manager before destroying it
        IceMeltManager.Instance.RemoveIce(ice);
        
        // Remove the Ice component and add the Sculpture component
        Sculpture sculpture = (Sculpture)ice.gameObject.AddComponent(typeof(Sculpture));

        // Copy all properties from the original ice
        sculpture.CopyFromIce(ice);

        Destroy(ice);

        // Add the sculpture back to the melt manager
        IceMeltManager.Instance.AddIce(sculpture);

        // Swap the mesh if we have sculpture meshes available
        if (sculptureMeshes != null && sculptureMeshes.Length > 0)
        {
            MeshFilter meshFilter = sculpture.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                // Randomly select a sculpture mesh
                meshFilter.mesh = sculptureMeshes[UnityEngine.Random.Range(0, sculptureMeshes.Length)];
            }
        }

        sculpture.IsLockedOnTable = false;
        playerInteractHandler.AddInteractableObject(sculpture.gameObject);

        // TODO: Add particle effect

        // TODO: Add sound effect

        // TODO: Record the chisel quality
    }

    /// <summary>
    /// Checks if the ice can be picked up from the table.
    /// Ice can only be picked up after it has been hit 5 times.
    /// </summary>
    /// <returns>True if the ice can be picked up, false otherwise.</returns>
    public bool CanPickUpIce()
    {
        return currentIce != null && hitCount >= HITS_REQUIRED;
    }

    /// <summary>
    /// Overrides CanPlaceIce to prevent placing ice that has already been hit.
    /// </summary>
    /// <param name="ice">The Ice object to check.</param>
    /// <returns>True if the table is empty and can accept the Ice object, false otherwise.</returns>
    public override bool CanPlaceIce(Ice ice)
    {
        if (playerController.GetHeldItem() == ice.gameObject) 
        {
            return false;
        }
        return currentIce == null && hitCount == 0;
    }
} 