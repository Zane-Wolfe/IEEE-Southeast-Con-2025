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
    [SerializeField] private AudioClip[] effortSounds; // Array of effort sound effects
    [SerializeField] private AudioClip[] depositSounds; // Array of deposit sound effects
    private AudioSource audioSource;

    void Start()
    {
        hitsBeforeNextIceDrop = hitsPerDrop;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {

    }

    public void Interact()
    {
        // Play a random effort sound effect
        if (effortSounds != null && effortSounds.Length > 0)
        {
            AudioClip randomSound = effortSounds[Random.Range(0, effortSounds.Length)];
            audioSource.PlayOneShot(randomSound);
        }

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

        // Play a random deposit sound effect
        if (depositSounds != null && depositSounds.Length > 0)
        {
            AudioClip randomSound = depositSounds[Random.Range(0, depositSounds.Length)];
            audioSource.PlayOneShot(randomSound);
        }

        // TODO: Add particle effect
        // TODO: Record the pick level used
        GameData gameData = Resources.Load<GameData>("GameData");
        newIce.ToolQualityMultiplier = gameData.pickLevel * 1.0f;
    
    }
}
