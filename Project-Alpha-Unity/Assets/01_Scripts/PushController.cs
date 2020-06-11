using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    private EntityStateController state;
    private PlayerController player;

    private Transform pushedObject;
    private Vector3 inputMoveDir;
    private Vector3 moveDir;

    private float pushSpeed = 5f;
    private float walkSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        state = this.GetComponent<EntityStateController>();
        player = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pushedObject != null && Input.GetMouseButtonUp(0))
        {
            ReleaseObject();
        }
    }

    void FixedUpdate()
    {
        if (pushedObject != null)
        {
            pushedObject.Translate(player.GetMoveDir() * Time.deltaTime);
        }
    }

    public void PushObject(Transform pushedObject)
    {
        this.pushedObject = pushedObject;
        //this.pushedObject.GetComponent<Rigidbody>().isKinematic = true;
        state.SetIsPushing(true);
        player.SetMoveSpeed(pushSpeed);
    }

    public void ReleaseObject()
    {
        
        pushedObject = null;
        state.SetIsPushing(false);
        state.SetIsInteracting(false);
        player.SetMoveSpeed(walkSpeed);
    }
}
