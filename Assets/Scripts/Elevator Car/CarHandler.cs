using UnityEngine;

public class CarHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform posTop;
    [SerializeField] private Transform posBottom;
    // True is car is at the top of the moutain
    private bool goingUp;
    [SerializeField] private float speed;
    private float startTime;
    private float journeyLength;
    [SerializeField] private bool carMoving = false;

    [SerializeField] private CameraController cameraController;
    private LineRenderer cableLineRender;
    [SerializeField] private Vector3 lineOffset;

    [SerializeField] private Transform carDoor;
    private Animator doorAnimatorController;

    [SerializeField] GameData gameData;

    void Start()
    {
        this.cableLineRender = GetComponent<LineRenderer>();
        // Car starts at the bottom of the moutain
        goingUp = false;
        this.doorAnimatorController = carDoor.GetComponent<Animator>();
    }

    // Move to the target end position.
    void Update()
    {
        cableLineRender.SetPosition(0, posBottom.position + lineOffset);
        cableLineRender.SetPosition(1, posTop.position + lineOffset);
        if (carMoving)
        {
            Transform startPos = posBottom;
            Transform endPos = posTop;
            if (!goingUp)
            {
                // Switch direction if going down
                startPos = posTop;
                endPos = posBottom;
            }
            
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);
            if(fractionOfJourney >= 0.99f)
            {
                // snap to finish
                transform.position = endPos.position;
                carMoving = false;

                // switch direction for next use
                goingUp = !goingUp;

                // make player back to normal
                playerController.freezePlayer = false;
                playerController.transform.parent = null;
                cameraController.setTarget(playerController.transform);

                // Animate the door opening
                Debug.Log("Car has stopped moving");
                this.doorAnimatorController.SetTrigger("OpenDoor");
            }
        }
    }

    public void startCarMoving()
    {
        if (carMoving) return;
        carMoving = true;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(posBottom.position, posTop.position);

        // Animate the door closing
        this.doorAnimatorController.SetTrigger("CloseDoor");

        // Decrease money
        gameData.playerMoney -= 100;
    }

    public bool isCarMoving()
    {
        return this.carMoving;
    }

    [SerializeField] private PlayerController playerController;

    // Press Button
    public void Interact()
    {
        playerController.freezePlayer = true;
        playerController.transform.parent = this.transform;
        cameraController.setTarget(this.transform);

        startCarMoving();
    }
}
