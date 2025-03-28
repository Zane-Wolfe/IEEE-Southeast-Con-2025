using UnityEngine;

/// <summary>
/// A table that allows the player to transform Ice objects into Sculpture objects.
/// </summary>
public class WorkingTable : BaseTable
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Mesh[] sculptureMeshes; // Array of possible sculpture meshes
    private int hitCount = 0;
    private const int HITS_REQUIRED = 5;

    /// <summary>
    /// Overrides the Interact method to handle the sculpture conversion
    /// After HITS_REQUIRED hits the ice is replaced with a sculpture.
    /// </summary>
    public override void Interact()
    {
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
    /// Transforms an Ice object into a Sculpture object.
    /// The sculpture inherits the ice's properties and adds sculpture multipliers.
    /// </summary>
    /// <param name="ice">The Ice object to transform.</param>
    private void TransformIceToSculpture(Ice ice)
    {
        // Convert the ice object to a sculpture
        Sculpture sculpture = ice.gameObject.AddComponent<Sculpture>();
        Destroy(ice);

        // Swap the mesh if we have sculpture meshes available
        if (sculptureMeshes != null && sculptureMeshes.Length > 0)
        {
            int randomIndex = Random.Range(0, sculptureMeshes.Length);
            sculpture.GetComponent<MeshFilter>().mesh = sculptureMeshes[randomIndex];
        }
        else
        {
            Debug.LogWarning("No sculpture meshes assigned to WorkingTable!");
        }

        // TODO: Add particle effect

        // TODO: Add sound effect

        // TODO: Record the chisel quality
    }
} 