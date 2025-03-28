using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour
{

    private bool showInteractIcon = false;
    [SerializeField] private List<GameObject> interactableObjectsNearby = new List<GameObject>();
    private PlayerController playerController;
    


    void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        // Clean any destroyed objects from the list
        interactableObjectsNearby.RemoveAll(obj => obj == null);
        
        // If more than one interactable object is nearby, show the interact icon
        showInteractIcon = interactableObjectsNearby.Count > 0;
        
        // Handle Interact Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (showInteractIcon && interactableObjectsNearby[0] != null)
            {
                playerController.HandleInteract(interactableObjectsNearby[0]);
            }
            else
            {
                Debug.Log("Nothing to interact with!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore if game object doesn't implment interface IInteractable
        if (other.gameObject.GetComponent<IInteractable>() == null) return;

        interactableObjectsNearby.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        interactableObjectsNearby.Remove(other.gameObject);

    }

}
