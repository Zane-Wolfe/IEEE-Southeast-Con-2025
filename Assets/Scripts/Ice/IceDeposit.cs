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
            return;
        }
    }

    private void DropIce()
    {
        Vector3 iceSpawnLoc = gameObject.transform.forward;
        iceSpawnLoc.y += 1;

        GameObject newIceObj = Instantiate(iceCubePrefab, iceSpawnLoc, Quaternion.identity);
        Ice newIce = newIceObj.GetComponent<Ice>();

        int iceQuality = 5;
        float frozenLength = 300;
        newIce.CreateIce(iceQuality, frozenLength);
    }

    
}
