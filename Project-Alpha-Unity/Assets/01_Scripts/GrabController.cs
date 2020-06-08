using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private EntityStateController state;
    [SerializeField]
    private Transform pickedObject = null;
    
    private float grabDistance = 5f;
    private float throwPower = 10f;

    private void Start()
    {
        state = transform.GetComponent<EntityStateController>();
    }

    private void Update()
    {
        //When holding left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, GameController.PickableLayerMask))
            {
                if ((hit.transform.position - transform.position).magnitude <= grabDistance)
                {
                    pickedObject = hit.transform;
                    pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                    state.SetIsGrabing(true);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ThrowObject(ref pickedObject);
        }

    }

    private void ThrowObject(ref Transform throwable)
    {
        if (throwable != null)
        {
            Rigidbody obj = throwable.GetComponent<Rigidbody>();
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            obj.AddForce((transform.TransformDirection(Vector3.forward) + new Vector3(0,0.5f,0)) * throwPower, ForceMode.Impulse);
        }
        throwable = null;
        state.SetIsGrabing(false);
    }

    private void FixedUpdate()
    {
        if (state.IsGrabing() && pickedObject != null)
        {
            pickedObject.transform.position = transform.position + new Vector3(0, 3, 0);
        }        
    }

    void OnDrawGizmos()
    {
        /*
        if (pickedObject != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(pickedObject.transform.position, (transform.TransformDirection(Vector3.forward) + Vector3.up) * throwPower);
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * grabDistance);
        */
    }

}
