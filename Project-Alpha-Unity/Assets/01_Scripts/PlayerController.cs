using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool     isInit = false;
    protected float   movementSpeed       = 2.5f;
    protected float   horizontalMovement  = 0.0f;
    protected float   verticalMovement    = 0.0f;

    public virtual bool init()
    {
        //Debug.Log("Player Init...");
        isInit = true;
        return true;
    }

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

    void OnDrawGizmos()
    {
        Color color;
        color = Color.green;
        // local up
        DrawHelperAtCenter(this.transform.up, color, 2f);
        color = Color.blue;
        // local forward
        DrawHelperAtCenter(this.transform.forward, color, 2f);
        color = Color.red;
        // local right
        DrawHelperAtCenter(this.transform.right, color, 2f);

        /*
        color.g -= 0.5f;
        // global up
        DrawHelperAtCenter(Vector3.up, color, 1f);
        color.b -= 0.5f;
        // global forward
        DrawHelperAtCenter(Vector3.forward, color, 1f);
        color.r -= 0.5f;
        // global right
        DrawHelperAtCenter(Vector3.right, color, 1f);
        */
    }

    private void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
