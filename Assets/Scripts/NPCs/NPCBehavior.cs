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
        StartCoroutine(WaitAndMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitAndMove() {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        float startTime = Time.time;
        Vector3 initialPos = gameObject.transform.position;
        Vector3 targetPos = new(Random.Range(minX, maxX), initialPos.y, Random.Range(minZ, maxZ));

        gameObject.transform.LookAt(targetPos);
        while(Time.time - startTime <= Random.Range(minWalkTime, maxWalkTime)) {
            gameObject.transform.position = Vector3.Lerp(initialPos, targetPos, Time.time - startTime);
            yield return 1;
        }
        StartCoroutine(WaitAndMove());
    }
}
