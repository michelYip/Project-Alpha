using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : PlayerController
{
    [SerializeField]
    private PlayerController target;
    [SerializeField]
    private GameObject mirror;

    private Vector3 targetToMirror;
    private Plane mirrorPlane;

    private float angleDiffX;
    private float angleDiffY;

    // Replace Start() to avoid conflit on executing order
    // NB : When integrating, don't forget to add a mirror and a player target first
    public override bool InitEntity()
    {
        controller = this.GetComponent<CharacterController>();
        state = transform.GetComponent<EntityStateController>();

        controller.enabled = false;

        if (target != null && mirror != null && target.IsInit())
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

            angleDiffX = Vector3.SignedAngle(target.GetLocalRight(), targetToMirror, Vector3.up);
            angleDiffY = Vector3.SignedAngle(target.GetLocalforward(), targetToMirror, Vector3.up);
            localRight      = Quaternion.Euler(0, angleDiffX * 2, 0) * - target.GetLocalRight();
            localForward    = Quaternion.Euler(0, angleDiffY * 2, 0) * - target.GetLocalforward();

            //Debug.Log(angleDiff);
            //Debug.Log(lookDir);
            Vector3 startPosition = new Vector3(reflectionStartPosition.x, reflectionStartPosition.y, reflectionStartPosition.z);

            transform.position = startPosition;
            controller.enabled = true;
            //Debug.Log(startPosition);
            state.SetIsInit(true);
            return true;
        }
        else
        {
            //Debug.Log("You should initialize a mirror and a player in my attributes !");
            if (state == null)
                return false;
            state.SetIsInit(false);
            return false;
        }
    }
    
    public override void SetLookAt()
    {
        Vector3 lookAtPoS = mirrorPlane.ClosestPointOnPlane(target.GetLookAt());
        Vector3 lookAtToPoS = lookAtPoS - target.GetLookAt();

        Vector3 targetPoS = mirrorPlane.ClosestPointOnPlane(target.transform.position);
        Vector3 targetToPoS = targetPoS - target.transform.position;

        Vector3 targetReflectionToCurrentPos = transform.position - (targetPoS + targetToPoS);

        lookAt = eyeLevel.ClosestPointOnPlane((lookAtPoS + lookAtToPoS) + targetReflectionToCurrentPos);
    }

    void FixedUpdate()
    {
        moveDir = ((localRight * horizontalMovement) + (localForward * verticalMovement)).normalized * movementSpeed;
        Vector3 jumpMotion = new Vector3(0, verticalVelocity, 0);
        controller.Move((moveDir + jumpMotion) * Time.deltaTime);


        Vector3 lookAtAngle = lookAt - transform.position;
        float targetAngle = Mathf.Atan2(lookAtAngle.x, lookAtAngle.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity, smoothRotationTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetTargetAndMirror(PlayerController target, GameObject mirror)
    {
        this.target = target;
        this.mirror = mirror;
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
        //DrawHelperAtCenter(lookAt - transform.position, Color.cyan, 1f);
    }

    public override void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
