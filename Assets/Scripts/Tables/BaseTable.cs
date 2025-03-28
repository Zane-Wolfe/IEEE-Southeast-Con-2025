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
    private Ice currentIce;

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
    public bool CanPlaceIce(Ice ice)
    {
        return currentIce == null;
    }

    /// <summary>
    /// Places an Ice object on the table if the table is empty.
    /// The Ice object will be parented to the table and positioned at the table's position.
    /// </summary>
    /// <param name="ice">The Ice object to place on the table.</param>
    public void PlaceIce(Ice ice)
    {
        if (CanPlaceIce(ice))
        {
            currentIce = ice;
            ice.transform.SetParent(transform);
            ice.transform.localPosition = Vector3.zero;
            ice.transform.localRotation = Quaternion.identity;
        }
    }

    /// <summary>
    /// Removes the current Ice object from the table if one exists.
    /// </summary>
    /// <returns>The Ice object that was removed, or null if no Ice was present.</returns>
    public Ice RemoveIce()
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
