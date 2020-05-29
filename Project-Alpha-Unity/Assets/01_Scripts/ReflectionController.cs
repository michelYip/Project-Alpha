using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : PlayerController
{
    public PlayerController player;
    public GameObject mirror;

    [SerializeField]
    private Vector3 playerToMirror;
    private float angleDiff;

    /**
     * Don't forget to initialize a mirror and a player tager first
     */
    public override bool init()
    {
        //Debug.Log("Reflection init...");
        if (player != null && mirror != null == player.isInit)
        {
            MeshFilter filter = (MeshFilter)mirror.GetComponent(typeof(MeshFilter));
            Vector3 normal;

            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
            Plane mirrorPlane = new Plane(normal, mirror.transform.position);

            Vector3 pointOfSymmetry = mirrorPlane.ClosestPointOnPlane(player.transform.position);
            playerToMirror = pointOfSymmetry - player.transform.position;
            Vector3 reflectionStartPosition = pointOfSymmetry + playerToMirror;

            //Debug.Log("Point of Symmetry : " + pointOfSymmetry);
            //Debug.Log("Vector from player to mirror : " + playerToMirror);
            //Debug.Log("Reflection start position : " + reflectionStartPosition);

            angleDiff = Vector3.SignedAngle(Vector3.right, playerToMirror.normalized, Vector3.up);

            Debug.Log(angleDiff);
            //Debug.Log(Quaternion.Euler(0, angleDiff * 2, 0));

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


        Vector3 move = new Vector3(-horizontalMovement, 0, verticalMovement) * movementSpeed * Time.deltaTime;
        //Vector3 moveDir = Quaternion.AngleAxis(angleDiff * 2, Vector3.up) * move;

        transform.position += Quaternion.Euler(0, angleDiff * 2, 0) * move;
    }

    void OnDrawGizmos()
    {
        Vector3 move = new Vector3(-horizontalMovement, 0, verticalMovement) * movementSpeed * Time.deltaTime;
        Vector3 moveDir = Quaternion.Euler(0, angleDiff * 2, 0) * move;
        DrawHelperAtCenter(moveDir.normalized, Color.magenta, 2f);
    }

    public override void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }

}
