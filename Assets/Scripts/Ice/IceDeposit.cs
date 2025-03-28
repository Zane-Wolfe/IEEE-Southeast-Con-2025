using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDeposit : MonoBehaviour, IInteractable
{
    // How many hits does the pickaxe need to harvest ice cubes
    private int hitsPerDrop = 5;
    // How many hits remaining until next ice cube drop
    private int hitsBeforeNextIceDrop;

    [SerializeField] private GameObject iceCubePrefab;

    void Start()
    {
        hitsBeforeNextIceDrop = hitsPerDrop;
    }

    void Update()
    {

    }

    public void Interact()
    {
        //
        // Play Ice mine sound here
        //

        hitsBeforeNextIceDrop--;
        if (hitsBeforeNextIceDrop == 0)
        {
            DropIce();
            hitsBeforeNextIceDrop = hitsPerDrop;
            return;
        }
    }

    private void DropIce()
    {
        // Spawn ice slightly inside the deposit
        Vector3 iceSpawnLoc = transform.position + new Vector3(-0.2f, 0, -0.2f);

        GameObject newIceObj = Instantiate(iceCubePrefab, iceSpawnLoc, Quaternion.identity);
        Ice newIce = newIceObj.GetComponent<Ice>();

        int iceQuality = 5;
        newIce.CreateIceWithRandomSize(iceQuality);
        IceMeltManager.Instance.AddIce(newIce);

        // Add force to push the ice out
        Rigidbody rb = newIceObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
        }

        // TODO: Add particle effect

        // TODO: Add sound effect

        // TODO: Record the pick quality
    }

    
}
