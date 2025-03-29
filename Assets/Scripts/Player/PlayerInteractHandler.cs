using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractHandler : MonoBehaviour
{

    private bool showInteractIcon = false;
    [SerializeField] private List<GameObject> interactableObjectsNearby = new List<GameObject>();
    private PlayerController playerController;

    private GameObject closestInteractableItem;
    


    void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        GameObject newClosestInteractableItem = findClosestItem();
        // Found a new closest item or none are in range anymore, deselect the old item
        if(newClosestInteractableItem != closestInteractableItem && closestInteractableItem != null)
        {
            //
            //  DESELECT ITEM SHADER HERE @CC
            //
            closestInteractableItem.transform.GetChild(0).gameObject.SetActive(false);
            if (closestInteractableItem.tag == "Ice") {
                closestInteractableItem.GetComponent<MeshRenderer>().material.SetFloat("_Refract", 0.05f);
            }
        }
        closestInteractableItem = newClosestInteractableItem;
        if (closestInteractableItem != null)
        {
            // SELECT ITEM SHADER HERE @CC
            closestInteractableItem.transform.GetChild(0).gameObject.SetActive(true);
            if (closestInteractableItem.tag == "Ice") {
                closestInteractableItem.GetComponent<MeshRenderer>().material.SetFloat("_Refract", 0.38f);
            }
        }

        // Clean any destroyed objects from the list
        interactableObjectsNearby.RemoveAll(obj => obj == null);
        
        // If more than one interactable object is nearby, show the interact icon
        showInteractIcon = interactableObjectsNearby.Count > 0;
        
        // Handle Interact Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (showInteractIcon && interactableObjectsNearby[0] != null)
            {
                playerController.HandleInteract(closestInteractableItem);
            }
            else
            {
                Debug.Log("Nothing to interact with!");
            }
        }

    }

    private GameObject findClosestItem()
    {
        if (interactableObjectsNearby.Count == 0) return null;
        if(interactableObjectsNearby.Count == 1) return interactableObjectsNearby[0];
        // Calculate the correct item to interact with
        // interactableObjectsNearby contains a list of all objects in the colider cone
        Vector3 playerPos = playerController.transform.position;
        GameObject closetObj = interactableObjectsNearby[0];
        float closestLength = Mathf.Infinity;
        foreach (GameObject obj in interactableObjectsNearby)
        {
            float distance = Vector3.Distance(obj.transform.position, playerPos);
            if (distance < closestLength)
            {
                closestLength = distance;
                closetObj = obj;
            }
        }
        return closetObj;
    }

    public GameObject getClosestTargetItem()
    {
        return closestInteractableItem;
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
