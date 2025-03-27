using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sculpture : MonoBehaviour, IInteractable, IPickupable
{
    // Ice Location Quality
    private int quality = -1;
    private float timeMined = -1;
    private float timeLeftFrozen = -1;

    // Where does this value come from?
    private int sculptureQuality = -1;

    // isOnTable means you can't pick it up until 
    private bool isOnTable = false;
    private bool isFinished = false;
    private int modelType = -1;
    private float progress = 0f;


    public void CreateSculpture(Ice iceData)
    {
        this.quality = iceData.GetQuality();
        this.timeMined  = iceData.GetTimeMined();
        this.timeLeftFrozen = iceData.GetTimeLeftFrozen();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(melt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator melt()
    {
        // melt one time unit (s) (timer ticking down in seconds)
        yield return new WaitForSeconds(1f);
        if (timeLeftFrozen > 0)
        {
            StartCoroutine(melt());
        }
    }

    public float GetSellValue()
    {
        return (timeLeftFrozen * quality);
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Pickup()
    {
        if(isOnTable)
        {

        }
        throw new System.NotImplementedException();
    }
}
