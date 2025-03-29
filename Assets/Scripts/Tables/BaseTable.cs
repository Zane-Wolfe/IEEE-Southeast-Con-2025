using UnityEngine;

/// <summary>
/// A base table class that can hold one Ice object or any object that extends from Ice.
/// The table implements IInteractable.
/// </summary>
public class BaseTable : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The current Ice object being held by the table. Can be null if no Ice is present.
    /// </summary>
    protected Ice currentIce;

    /// <summary>
    /// The point to snap the Ice object to when it is placed on the table.
    /// </summary>
    [SerializeField] protected Transform snapPoint;

    /// <summary>
    /// The player controller script.
    /// </summary>
    [SerializeField] protected PlayerController playerController;

    /// <summary>
    /// Handles the logic for when an Ice object is placed on the table.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ice"))
        {
            Ice ice = other.GetComponent<Ice>();
            if (ice != null && CanPlaceIce(ice))
            {
                PlaceIce(ice);
            }
        }
    }

    /// <summary>
    /// Handles interaction with the table. Currently just logs a debug message.
    /// </summary>
    public virtual void Interact()
    {
        // Handle interaction logic here
        Debug.Log("Table interacted with");
    }

    /// <summary>
    /// Checks if an Ice object can be placed on the table. Accepts any object that extends from Ice.
    /// </summary>
    /// <param name="ice">The Ice object to check.</param>
    /// <returns>True if the table is empty and can accept the Ice object, false otherwise.</returns>
    public virtual bool CanPlaceIce(Ice ice)
    {
        if (playerController.GetHeldItem() == ice.gameObject) 
        {
            return false;
        }
        return currentIce == null;
    }

    /// <summary>
    /// Places an Ice object on the table if the table is empty.
    /// The Ice object will be parented to the table's parent and positioned at the table's position.
    /// </summary>
    /// <param name="ice">The Ice object to place on the table.</param>
    public virtual void PlaceIce(Ice ice)
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
            ice.IsLockedOnTable = true;
        }
    }

    /// <summary>
    /// Removes the current Ice object from the table.
    /// </summary>
    /// <returns>The Ice object that was removed, or null if no Ice was present.</returns>
    public virtual Ice RemoveIce()
    {
        if (currentIce != null)
        {
            Ice iceToReturn = currentIce;
            currentIce = null;
            iceToReturn.transform.SetParent(null);
            return iceToReturn;
        }
        return null;
    }

    /// <summary>
    /// Checks if the table currently has an Ice object.
    /// </summary>
    /// <returns>True if the table has an Ice object, false otherwise.</returns>
    public bool HasIce()
    {
        return currentIce != null;
    }

    /// <summary>
    /// Gets the current Ice object being held by the table.
    /// </summary>
    /// <returns>The current Ice object, or null if no Ice is present.</returns>
    public Ice GetCurrentIce()
    {
        return currentIce;
    }
}
