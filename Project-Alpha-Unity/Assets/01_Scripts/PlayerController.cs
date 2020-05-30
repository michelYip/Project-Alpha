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

    public Vector3 localRight   = Vector3.right;
    public Vector3 localForward = Vector3.forward;

    public virtual bool initEntity()
    {
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
        // local up
        DrawHelperAtCenter(this.transform.up, Color.green, 2f);
        // local forward
        DrawHelperAtCenter(this.transform.forward, Color.blue, 2f);
        // local right
        DrawHelperAtCenter(this.transform.right, Color.red, 2f);
    }

    public virtual void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
