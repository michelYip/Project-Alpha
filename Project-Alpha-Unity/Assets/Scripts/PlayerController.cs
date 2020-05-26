using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float horizontalMovement = 0.0f;
    public float verticalMovement = 0.0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    void FixedUpdate()
    {

        transform.position += new Vector3(horizontalMovement, 0, verticalMovement) * movementSpeed * Time.deltaTime;
    }
}
