using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // target center position
    [SerializeField] private Transform targetTransform;
    // Isometric view offset
    [SerializeField] private Vector3 cameraViewOffset;
    [SerializeField] private float smoothTime;
    private Vector3 currrentVelocity = Vector3.zero;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 targetPos = cameraViewOffset + targetTransform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currrentVelocity, smoothTime);
    }

    public void setTarget(Transform target)
    {
        targetTransform = target;
    }

}
