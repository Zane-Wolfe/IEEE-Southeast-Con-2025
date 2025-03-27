using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour, IInteractable, IPickupable 
{

    /**
     * Quality (int) (get better by getting better ice)
       TimeSinceMined (long)
       TimeLeftFrozen (long)
       Pickupable
     * 
     */

    [SerializeField] private int quality = -1;
    private float timeMined = -1;
    private float timeLeftFrozen= -1;

    // Call this function from IceDeposit to create ice variables
    /*
     * quality = DEFINE HERE
     * frozenLength = time in seconds that ice will be frozen for 
     */
    public void CreateIce(int quality, float frozenLength)
    {
        this.quality = quality;
        this.timeMined = Time.timeSinceLevelLoad;
        this.timeLeftFrozen = frozenLength;
    }

    public void Interact()
    {
        // Do nothing, pickup interface
    }

    public void Pickup()
    {
        // Run any logic here when picked up
        Debug.Log("Ice picked up");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
