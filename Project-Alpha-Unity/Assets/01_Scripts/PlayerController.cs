using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected EntityStateController state;
    protected CharacterController controller;
    
    // Orientation Attributes
    [SerializeField]
    protected Vector3 lookAt;
    [SerializeField]
    protected Plane eyeLevel;
    protected float smoothRotationVelocity;
    protected float smoothRotationTime = 0.1f;

    // Movement Attributes
    protected Vector3 moveDir;
    protected Vector3 localRight = Vector3.right;
    protected Vector3 localForward = Vector3.forward;
    protected float movementSpeed = 10.0f;
    protected float horizontalMovement = 0.0f;
    protected float verticalMovement = 0.0f;

    //Jump Attribute
    private bool hit;
    private RaycastHit hitInfo;
    private float extraHeight = 0.8f;
    protected float verticalVelocity;
    private float gravity = 25;
    private float jumpForce = 12f;


    public bool IsInit()
    {
        if (state == null)
            return false;
        return state.IsInit();
    }

    // Replace Start() to avoid conflit on executing order
    public virtual bool InitEntity()
    {
        controller = this.GetComponent<CharacterController>();
        state = transform.GetComponent<EntityStateController>();

        eyeLevel = new Plane(Vector3.up, this.transform.position);
        lookAt = Vector3.zero;

        state.SetIsInit(true);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        eyeLevel.SetNormalAndPosition(Vector3.up, transform.position);
        SetLookAt();
        if (!state.IsGrounded())
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else if (state.IsGrounded())
        {
            if (verticalVelocity < -gravity)
            {
                verticalVelocity = -gravity;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }

        state.SetIsIdle(state.IsGrounded() && horizontalMovement == 0 && verticalMovement == 0);
        hit = Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.down, out hitInfo, transform.rotation, extraHeight);//, GameController.ObstacleLayerMask);
        state.SetIsGrounded(hit);
    }

    void FixedUpdate()
    {
        moveDir = new Vector3(horizontalMovement, 0, verticalMovement).normalized * movementSpeed;
        Vector3 jumpMotion = new Vector3(0, verticalVelocity, 0);
        controller.Move((moveDir + jumpMotion) * Time.deltaTime);

        Vector3 lookAtAngle = lookAt - transform.position;
        float targetAngle = Mathf.Atan2(lookAtAngle.x, lookAtAngle.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity, smoothRotationTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //transform.LookAt(lookAt, Vector3.up);
        //Debug.Log(lookAt * smoothRotation);
    }

    public EntityStateController GetState() => state;
    
    public virtual void SetLookAt()
    {
        Ray ray = GameController.mainCamera.ScreenPointToRay(Input.mousePosition);
        float enter = 0;
        if (eyeLevel.Raycast(ray, out enter))
        {
            lookAt = ray.GetPoint(enter);
        }
    }

    public Vector3 GetLookAt() => lookAt;

    public Vector3 GetLocalRight() => localRight;

    public Vector3 GetLocalforward() => localForward;

    public Vector3 GetMoveDir() => moveDir;

    public void SetMoveSpeed(float moveSpeed) => this.movementSpeed = moveSpeed;

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
        //DrawHelperAtCenter(lookAt - transform.position, Color.magenta, 1f);

        //Check if there has been a hit yet
        /*if (hit)
        {
            Gizmos.color = Color.green;
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, Vector3.down * hitInfo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + Vector3.down * hitInfo.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            Gizmos.color = Color.red;
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, Vector3.down * extraHeight);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + Vector3.down * extraHeight, transform.localScale);
        }*/
    }

    public virtual void DrawHelperAtCenter(Vector3 direction, Color color, float scale)
    {
        Gizmos.color = color;
        Vector3 destination = transform.position + direction * scale;
        Gizmos.DrawLine(transform.position, destination);
    }
}
