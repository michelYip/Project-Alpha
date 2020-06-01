using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected EntityStateController state;

    // Movement Attributes
    [SerializeField]
    protected float movementSpeed;
    protected float horizontalMovement = 0.0f;
    protected float verticalMovement = 0.0f;

    [SerializeField]
    protected Vector3 localRight = Vector3.right;
    [SerializeField]
    protected Vector3 localForward = Vector3.forward;

    // Orientation Attributes
    [SerializeField]
    protected Vector3 lookAt;
    [SerializeField]
    protected Plane eyeLevel;
    
    // Replace Start() to avoid conflit on executing order
    public virtual bool InitEntity()
    {
        state = transform.GetComponent<EntityStateController>();

        eyeLevel = new Plane(Vector3.up, this.transform.position);
        lookAt = Vector3.zero;

        state.SetIsInit(true);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        eyeLevel.SetNormalAndPosition(Vector3.up, transform.position);
        SetLookAt();

        state.SetIsIdle(state.IsGrounded() && horizontalMovement == 0 && verticalMovement == 0);
    }

    void FixedUpdate()
    {
        transform.LookAt(lookAt, Vector3.up);
        transform.position += new Vector3(horizontalMovement, 0, verticalMovement) * movementSpeed * Time.deltaTime;
    }

    public bool IsInit()
    {
        if (state == null)
            return false;
        return state.IsInit();
    }

    public virtual void SetLookAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0;
        if (eyeLevel.Raycast(ray, out enter))
        {
            lookAt = ray.GetPoint(enter);
        }
    }

    public Vector3 GetLookAt()
    {
        return lookAt;
    }

    public Vector3 GetLocalRight()
    {
        return localRight;

    }

    public Vector3 GetLocalforward()
    {
        return localForward;
    }

    /*
     * DEBUG
     */
    void OnDrawGizmos()
    {
        // local up
        DrawHelperAtCenter(this.transform.up, Color.green, 2f);
        // local forward
        DrawHelperAtCenter(localForward, Color.blue, 2f);
        // local right
        DrawHelperAtCenter(localRight, Color.red, 2f);

        // target orientaion
        DrawHelperAtCenter(lookAt - transform.position, Color.magenta, 1f);
    }

    public virtual void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
