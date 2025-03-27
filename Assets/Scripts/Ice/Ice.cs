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

    // Ice Location Quality
    private int quality = -1;
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

    void Start()
    {
        StartCoroutine(melt());
    }

    void Update()
    {
        
    }

    private IEnumerator melt()
    {
        // melt one time unit (s) (timer ticking down in seconds)
        yield return new WaitForSeconds(1f);
        if(timeLeftFrozen > 0 )
        {
            StartCoroutine(melt());
        }
    }

    public void Interact()
    {
        // Do nothing, Pickup() handles this logic 
    }

    public void Pickup()
    {
        // Run any logic here when picked up
        Debug.Log("Ice picked up");
    }

    public int GetQuality()
    {
        return this.quality;
    }

    public float GetTimeMined()
    {
        return this.timeMined;
    }

    public float GetTimeLeftFrozen()
    {
        return this.timeLeftFrozen;
    }

}
