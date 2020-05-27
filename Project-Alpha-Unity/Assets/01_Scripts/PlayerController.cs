using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float horizontalMovement = 0.0f;
    public float verticalMovement = 0.0f;

    public bool isReflection; 
    public GameObject player = null;


    void Start()
    {
        if (gameObject.name != "Player")
        {
            player = GameObject.Find("Player");
            Debug.Log("Found Player prefab");
        }
        if (player != null && isReflection)
        {
            transform.position.Set(player.transform.position.x, player.transform.position.y, -player.transform.position.z);
            Debug.Log("Repositioning Reflection Prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        if (isReflection)
            verticalMovement = Input.GetAxisRaw("Vertical") * -movementSpeed;
        else
            verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;
    }

    void FixedUpdate()
    {

        transform.position += new Vector3(horizontalMovement, 0, verticalMovement) * movementSpeed * Time.deltaTime;
    }
}
