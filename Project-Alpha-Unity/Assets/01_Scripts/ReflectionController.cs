using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : PlayerController
{
    public PlayerController target;
    public GameObject mirror;

    [SerializeField]
    private Vector3 targetToMirror;
    private Plane mirrorPlane;
    private float angleDiffX;
    private float angleDiffY;

    // Replace Start() to avoid conflit on executing order
    // NB : When integrating, don't forget to add a mirror and a player target first
    public override bool InitEntity()
    {
        if (target != null && mirror != null == target.isInit)
        {
            MeshFilter filter = (MeshFilter)mirror.GetComponent(typeof(MeshFilter));
            Vector3 normal;

            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
            mirrorPlane = new Plane(normal, mirror.transform.position);

            Vector3 pointOfSymmetry = mirrorPlane.ClosestPointOnPlane(target.transform.position);
            targetToMirror = pointOfSymmetry - target.transform.position;
            Vector3 reflectionStartPosition = pointOfSymmetry + targetToMirror;

            //Debug.Log("Point of Symmetry : " + pointOfSymmetry);
            //Debug.Log("Vector from player to mirror : " + targetToMirror);
            //Debug.Log("Reflection start position : " + reflectionStartPosition);

            angleDiffX = Vector3.SignedAngle(target.localRight, targetToMirror, Vector3.up);
            angleDiffY = Vector3.SignedAngle(target.localForward, targetToMirror, Vector3.up);
            localRight      = Quaternion.Euler(0, angleDiffX * 2, 0) * - target.localRight;
            localForward    = Quaternion.Euler(0, angleDiffY * 2, 0) * - target.localForward;
            
            //Debug.Log(angleDiff);
            //Debug.Log(lookDir);

            transform.position = new Vector3(reflectionStartPosition.x, reflectionStartPosition.y, reflectionStartPosition.z);
            isInit = true;
            return true;
        }
        else
        {
            //Debug.Log("You should initialize a mirror and a player in my attributes !");
            isInit = false;
            return false;
        }
    }
    
    public override void SetLookAt()
    {
        Vector3 pointOfSymmetry = mirrorPlane.ClosestPointOnPlane(target.getLookAt());
        targetToMirror = pointOfSymmetry - target.getLookAt();
        lookAt = eyeLevel.ClosestPointOnPlane(pointOfSymmetry + targetToMirror);
    }

    void FixedUpdate()
    {
        transform.LookAt(lookAt, Vector3.up);
        Vector3 move = (localRight.normalized * horizontalMovement) + (localForward.normalized * verticalMovement);
        transform.position += move * movementSpeed * Time.deltaTime;
    }

    
    /*
     * DEBUG
     */ 
    void OnDrawGizmos()
    {
        // local forward
        DrawHelperAtCenter(localForward.normalized, Color.blue, 2f);
        // local right
        DrawHelperAtCenter(localRight.normalized, Color.red, 2f);

        // target orientaion
        DrawHelperAtCenter(lookAt - transform.position, Color.cyan, 1f);
    }

    public override void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }

}
