using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : PlayerController
{
    public PlayerController target;
    public GameObject mirror;

    [SerializeField]
    public float angleDiffX;
    public float angleDiffY;

    /**
     * Don't forget to initialize a mirror and a player tager first
     */
    public override bool initEntity()
    {
        if (target != null && mirror != null == target.isInit)
        {
            MeshFilter filter = (MeshFilter)mirror.GetComponent(typeof(MeshFilter));
            Vector3 normal;

            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
            Plane mirrorPlane = new Plane(normal, mirror.transform.position);

            Vector3 pointOfSymmetry = mirrorPlane.ClosestPointOnPlane(target.transform.position);
            Vector3 playerToMirror = pointOfSymmetry - target.transform.position;
            Vector3 reflectionStartPosition = pointOfSymmetry + playerToMirror;

            //Debug.Log("Point of Symmetry : " + pointOfSymmetry);
            //Debug.Log("Vector from player to mirror : " + playerToMirror);
            //Debug.Log("Reflection start position : " + reflectionStartPosition);

            angleDiffX = Vector3.SignedAngle(target.localRight, playerToMirror, Vector3.up);
            angleDiffY = Vector3.SignedAngle(target.localForward, playerToMirror, Vector3.up);
            localRight      = Quaternion.Euler(0, angleDiffX * 2, 0) * - target.localRight;
            localForward    = Quaternion.Euler(0, angleDiffY * 2, 0) * - target.localForward;

            //Debug.Log(angleDiff);

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



    void FixedUpdate()
    {
        //Fix reflection of reflection
        //Vector3 inputDir = new Vector3(horizontalMovement, 0, verticalMovement);
        //inputDir = Quaternion.Euler(0,0,180) * inputDir;
        //inputDir = Quaternion.Euler(0, angleDiff * 2, 0) * inputDir;

        Vector3 move = (localRight.normalized * horizontalMovement) + (localForward.normalized * verticalMovement);

        //Vector3 move = inputDir * movementSpeed * Time.deltaTime;
        //move = Quaternion.Euler(0, Vector3.SignedAngle(localRight, , Vector3.up), 0)

        transform.position += move * movementSpeed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        DrawHelperAtCenter(localRight.normalized, Color.red, 2f);
        DrawHelperAtCenter(localForward.normalized, Color.blue, 2f);
    }

    public override void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }

}
