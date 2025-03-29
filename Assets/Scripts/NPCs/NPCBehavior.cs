using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Transform walkingArea;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    private float minWalkTime = 1;
    private float maxWalkTime = 3;
    private float minDelay = 1;
    private float maxDelay = 3;

    // Start is called before the first frame update
    void Start()
    {
        float xRadius = walkingArea.localScale.x/2;
        float zRadius = walkingArea.localScale.z/2;
        minX = walkingArea.position.x - xRadius;
        maxX = walkingArea.position.x + xRadius;
        minZ = walkingArea.position.z - zRadius;
        maxZ = walkingArea.position.z + zRadius;
        Debug.Log(gameObject.transform.position);
        StartCoroutine(WaitAndMove());
        // InvokeRepeating("MoveNPC", 1, 3);
        // Debug.Log("");
    }

    // Update is called once per frame
    void Update()
    {
        // npcTransform.LookAt(new Vector3(0, 0, 0));
        // gameObject.transform.LookAt(new Vector3(minX, gameObject.transform.position.y, maxZ));
    }

    IEnumerator WaitAndMove() {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        float startTime = Time.time;
        Vector3 initialPos = gameObject.transform.position;
        Vector3 targetPos = new Vector3(Random.Range(minX, maxX), initialPos.y, Random.Range(minZ, maxZ));

        gameObject.transform.LookAt(targetPos);
        while(Time.time - startTime <= Random.Range(minWalkTime, maxWalkTime)) {
            gameObject.transform.position = Vector3.Lerp(initialPos, targetPos, Time.time - startTime);
            yield return 1;
        }
        Debug.Log("Finished");
        StartCoroutine(WaitAndMove());
    }

    // void MoveNPC() {
    //     Debug.Log("Start");
    //     float startTime = Time.time;
    //     Vector3 initialPos = gameObject.transform.position;
    //     Vector3 targetPos = new Vector3(Random.Range(minX, maxX), initialPos.y, Random.Range(minZ, maxZ));
    //     while(Time.time - startTime <= 1) {
    //         gameObject.transform.position = Vector3.Lerp(initialPos, targetPos, Time.time - startTime);
    //         // yield return 1;
    //     }
    //     Debug.Log("Finish");
    // }
}
