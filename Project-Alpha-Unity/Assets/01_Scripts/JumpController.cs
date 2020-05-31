using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private EntityStateController state;

    private bool hit;
    private RaycastHit hitInfo;
    private float extraHeight = 0.1f;

    //Jump Attribute
    public float verticalVelocity;
    private float gravity = 25f;
    private float jumpForce = 15f;

    private void Start()
    {
        state = transform.GetComponent<EntityStateController>();
    }

    void Update()
    {
        if (verticalVelocity > 0)
        {
           verticalVelocity -= gravity * Time.deltaTime;
        }
        

        if (Input.GetKeyDown(KeyCode.Space) && state.IsGrounded())
        {
            verticalVelocity = jumpForce;
        }

        hit = Physics.BoxCast(transform.position + new Vector3(0, extraHeight, 0), transform.localScale / 2, Vector3.down, out hitInfo, transform.rotation, extraHeight);
        state.SetIsGrounded(hit);

    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        //Check if there has been a hit yet
        if (hit)
        {
            Gizmos.color = Color.green;
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, Vector3.down * hitInfo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + Vector3.down * hitInfo.distance, transform.localScale/2);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            Gizmos.color = Color.red;
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, Vector3.down * extraHeight);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + Vector3.down * extraHeight, transform.localScale);
        }
    }
}
