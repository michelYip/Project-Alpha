using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    
    private float smoothSpeed = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private Vector3 offsetPos;
    [SerializeField]
    private Quaternion offsetRot;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = offsetRot;

        Vector3 targetPosition = target.position + offsetPos;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
