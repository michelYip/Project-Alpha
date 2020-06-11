using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private EntityStateController state;
    [SerializeField]
    private Transform pickedObject = null;
    
    private float throwPower = 10f;

    private void Start()
    {
        state = transform.GetComponent<EntityStateController>();
    }

    private void Update()
    {
        if (pickedObject != null && Input.GetMouseButtonUp(0))
        {
            ThrowObject(ref pickedObject);
        }
    }

    public void GrabObject(Transform pickedObject)
    {
        this.pickedObject = pickedObject;
        this.pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        state.SetIsGrabing(true);
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
        state.SetIsInteracting(false);
    }

    private void FixedUpdate()
    {
        if (state.IsGrabing() && pickedObject != null)
        {
            pickedObject.transform.position = transform.position + new Vector3(0, 3, 0);
        }        
    }
}
