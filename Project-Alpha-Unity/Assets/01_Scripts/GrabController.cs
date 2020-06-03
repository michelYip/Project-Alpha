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
            RaycastHit hit;
            //if the entity is close enough to a pickable object, pick it
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance, GameController.PickableLayerMask))
            {
                pickedObject = hit.transform;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                //LeanTween.moveLocal(pickedObject.transform.gameObject, transform.position + new Vector3(0, 4, 0), 2f).setEaseInQuad();
                state.SetIsGrabing(true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ThrowObject(ref pickedObject);
            //pickedObject = null;
            //state.SetIsGrabing(false);
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
            //Debug.Log("Holding object");
            pickedObject.transform.position = transform.position + new Vector3(0, 3, 0);
        }
        
    }

    void OnDrawGizmos()
    {
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

    }

}
