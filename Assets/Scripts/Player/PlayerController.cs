using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerController : MonoBehaviour
{
    private GameObject heldItem = null;
    private Camera mainCamera = null;

    [SerializeField]
    private int throwForce = 5;

    [SerializeField]
    private Animator playerAnimator = null;

    public GameObject GetHeldItem()
    {
        return heldItem;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        heldItem = null;
        mainCamera = Camera.main;
        freezePlayer = false;
    }

    void Update()
    {
        HandleMovementReal();

        if(heldItem != null)
        {
            Vector3 heldItemPos = heldItem.transform.position;
            Quaternion heldItemRot = heldItem.transform.rotation;
            Rigidbody heldRigidbody = heldItem.GetComponent<Rigidbody>();
            heldItemPos = transform.forward + transform.position;
            heldItemRot = transform.rotation;
            heldItem.transform.position = heldItemPos;
            heldItem.transform.rotation = heldItemRot;
            heldRigidbody.isKinematic = true;
            // Prevent the held item from affecting the player's physics
            heldRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            heldRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            heldRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public bool freezePlayer = false;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    
    private void HandleMovementReal()
    {
        if (freezePlayer) return;
        Quaternion diagonal = Quaternion.Euler(0, 45, 0);
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //look vector
        //gameObject.transform.forward = 
        Vector3 dir = GetMouseDir() - transform.position;
        Debug.Log(Vector3.Dot(dir, transform.forward));
        if (Vector3.Dot(dir, transform.forward) > 0)
        {
            Debug.Log("Bleh");
            playerAnimator.SetFloat("Blend", (Vector3.SignedAngle(dir, transform.forward, Vector3.up)) * Mathf.Deg2Rad / (-0.5f * Mathf.PI));
        }
        else
        {
            transform.LookAt(GetMouseDir());
        }
        //transform.LookAt(GetMouseDir());
        //GetMouseDir();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        controller.Move(diagonal * input * Time.deltaTime * playerSpeed);
        Debug.Log(input.magnitude);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (input.magnitude > 0.01f)
        {
            playerAnimator.SetBool("isWalking", true);
            playerAnimator.speed = playerVelocity.magnitude * 10;
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
            playerAnimator.speed = 1;
        }
        Debug.Log(playerVelocity.magnitude);
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
                //Vector3 dropItemPos = transform.forward + transform.position;
                //heldItem.transform.position = dropItemPos;
                Rigidbody heldRigidbody = heldItem.GetComponent<Rigidbody>();
                heldRigidbody.isKinematic = false;
                // Reset physics settings when dropped
                heldRigidbody.interpolation = RigidbodyInterpolation.None;
                heldRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                heldRigidbody.constraints = RigidbodyConstraints.None;
                heldRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
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
