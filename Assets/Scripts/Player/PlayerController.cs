using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerController : MonoBehaviour
{
    private GameObject heldItem = null;
    private Camera mainCamera = null;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        heldItem = null;
        mainCamera = Camera.main;
    }

    

    void Update()
    {
        HandleMovementReal();

        // TEMP INTERACT VISUALIZER CODE

        if(heldItem != null)
        {
            Vector3 heldItemPos = gameObject.transform.position;
            heldItemPos.y += 1;
            heldItem.transform.position = heldItemPos;
        }
        
        // END TEMP INTERACT VISUALIZER CODE

    }

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private void HandleMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleMovementReal()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Quaternion diagonal = Quaternion.Euler(0, 45, 0);
        controller.Move(diagonal * input * Time.deltaTime * playerSpeed);

        //look vector
        //gameObject.transform.forward = 
        transform.LookAt(GetMouseDir());
        //GetMouseDir();
        
        //playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Function is called when item is interacted with
    // All gameobjects passed here are guaranteed to implment IInteractable
    public void HandleInteract(GameObject interactedObject)
    {
        interactedObject.GetComponent<IInteractable>().Interact();

        //Debug.Log("Interacted with " + interactedObject.name);
        if(interactedObject.GetComponent<IPickupable>() != null)
        {
            if(heldItem == null) {
                interactedObject.GetComponent<IPickupable>().Pickup();
                heldItem = interactedObject;
                //Debug.Log("Now holding " + interactedObject.name);
            }
            // If interact key was hit again while holding item (Meaning Drop item)
            else if(heldItem == interactedObject)
            {
                // Drop item
                Vector3 dropItemPos = gameObject.transform.forward;
                dropItemPos.y += 1;
                heldItem.transform.position = dropItemPos;
                heldItem = null;
            }else {
                //Debug.Log("HAND IS FULL!");
                // Do other logic here?
            }
        }
        // Other interactions here

    }

    Vector3 GetMouseDir() {
        Plane playerLevelPlane = new Plane(Vector3.up, -transform.position.y);

        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0;
        if (playerLevelPlane.Raycast(mouseRay, out hitDistance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDistance);

            //Debug.DrawRay(mouseRay.origin, mouseRay.direction * hitDistance, Color.green);
            
            return hitPoint;
        }

        return Vector3.zero;
    }

}
