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
    [SerializeField] private AudioClip[] effortSounds; // Array of effort sound effects
    private AudioSource audioSource;
    private int hitCount = 0;
    private const int HITS_REQUIRED = 5;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

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

                // Play a random effort sound effect
                if (effortSounds != null && effortSounds.Length > 0)
                {
                    AudioClip randomSound = effortSounds[UnityEngine.Random.Range(0, effortSounds.Length)];
                    audioSource.PlayOneShot(randomSound);
                }

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
        if (ice == null)
        {
            Debug.LogError("Attempted to transform null ice object");
            return;
        }

        Debug.Log("Transforming Ice to Sculpture");
        
        // Remove the ice from the melt manager before destroying it
        if (IceMeltManager.Instance != null)
        {
            IceMeltManager.Instance.RemoveIce(ice);
        }
        
        // Remove the Ice component and add the Sculpture component
        Sculpture sculpture = (Sculpture)ice.gameObject.AddComponent(typeof(Sculpture));
        if (sculpture == null)
        {
            Debug.LogError("Failed to add Sculpture component");
            return;
        }

        // Copy all properties from the original ice
        sculpture.CopyFromIce(ice);

        Destroy(ice);

        // Add the sculpture back to the melt manager
        if (IceMeltManager.Instance != null)
        {
            IceMeltManager.Instance.AddIce(sculpture);
        }

        // Swap the mesh if we have sculpture meshes available
        if (sculptureMeshes != null && sculptureMeshes.Length > 0)
        {
            MeshFilter meshFilter = sculpture.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                // Randomly select a sculpture mesh
                var randomSculptureMesh = sculptureMeshes[UnityEngine.Random.Range(0, sculptureMeshes.Length)];
                meshFilter.mesh = randomSculptureMesh;

                // Try to update the glow mesh if it exists
                if (sculpture.transform.childCount > 0)
                {
                    MeshFilter glowMeshFilter = sculpture.transform.GetChild(0).GetComponent<MeshFilter>();
                    if (glowMeshFilter != null)
                    {
                        glowMeshFilter.mesh = randomSculptureMesh;
                    }
                }
            }
            else
            {
                Debug.LogWarning("MeshFilter component missing from sculpture object");
            }
        }
        else
        {
            Debug.LogWarning("No sculpture meshes available for transformation");
        }

        sculpture.IsLockedOnTable = false;
        
        // Clean up the table's state
        currentIce = null;
        sculpture.transform.SetParent(null);
        
        if (playerInteractHandler != null)
        {
            playerInteractHandler.AddInteractableObject(sculpture.gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerInteractHandler reference is missing on WorkingTable");
        }

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
        if (playerController != null && playerController.GetHeldItem() == ice.gameObject) 
        {
            return false;
        }
        return currentIce == null && hitCount == 0;
    }

    /// <summary>
    /// Overrides PlaceIce to prevent sculptures from being locked when placed back on the table.
    /// </summary>
    public override void PlaceIce(Ice ice)
    {
        if (CanPlaceIce(ice))
        {
            Rigidbody rb = ice.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
            
            currentIce = ice;
            ice.transform.SetParent(transform.parent);
            ice.transform.position = snapPoint.position;
            ice.transform.rotation = snapPoint.rotation;
            
            // Only lock if it's not a sculpture
            if (!(ice is Sculpture))
            {
                ice.IsLockedOnTable = true;
            }
            else
            {
                ice.IsLockedOnTable = false;
                ice.transform.SetParent(null);
                currentIce = null;
            }
        }
    }
} 