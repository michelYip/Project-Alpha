using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionController : PlayerController
{
    public PlayerController player;
    public GameObject mirror;


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
            Vector3 playerToMirror = pointOfSymmetry - player.transform.position;
            Vector3 reflectionStartPosition = pointOfSymmetry + playerToMirror;

            //Debug.Log("Point of Symmetry : " + pointOfSymmetry);
            //Debug.Log("Vector from player to mirror : " + playerToMirror);
            //Debug.Log("Reflection start position : " + reflectionStartPosition);

            transform.position = new Vector3(reflectionStartPosition.x, reflectionStartPosition.y, reflectionStartPosition.z);
            isInit = true;
            return true;
        }
        else
        {
            Debug.Log("You should initialize a mirror and a player in my attributes !");
            isInit = false;
            return false;
        }
    }

    void FixedUpdate()
    {
            
    }

}
