using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private EntityStateController state;

    private GrabController grabController;
    private PushController pushController;

    private RaycastHit hit;
    private float interactionDistance = 4f;
    private float sphereCastRadius = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        state = transform.GetComponent<EntityStateController>();
        grabController = transform.GetComponent<GrabController>();
        pushController = transform.GetComponent<PushController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = GameController.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!state.IsInteracting() && 
            Physics.SphereCast(ray.origin, sphereCastRadius, ray.direction, out hit, Mathf.Infinity, (GameController.PickableLayerMask | GameController.PushableLayerMask | GameController.InteractableLayerMask)))
        {
            Debug.Log(hit.transform.gameObject);
            if ((hit.transform.position - transform.position).magnitude <= interactionDistance)
            {
                //ToDo : Display Interaction UI depending on the type of the object hit
                if (GameController.PickableLayerMask == 1 << hit.transform.gameObject.layer)
                {
                    //Debug.Log("Can pick this object : " + hit.transform.gameObject);
                }
                else if (GameController.PushableLayerMask == 1 << hit.transform.gameObject.layer)
                {
                    //Debug.Log("Can push this object : " + hit.transform.gameObject);
                }
                else if (GameController.InteractableLayerMask == 1 << hit.transform.gameObject.layer)
                {
                    //Debug.Log("Can interact with this object : " + hit.transform.gameObject);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    state.SetIsInteracting(true);
                    if (GameController.PickableLayerMask == 1 << hit.transform.gameObject.layer)
                    {
                        grabController.GrabObject(hit.transform);
                    }
                    else if (GameController.PushableLayerMask == 1 << hit.transform.gameObject.layer)
                    {
                        pushController.PushObject(hit.transform);
                    }
                    else if (GameController.InteractableLayerMask == 1 << hit.transform.gameObject.layer)
                    {
                        Debug.Log("Can interact with this object : " + hit.transform.gameObject);
                        //Interactable interaction = hit.transform.GetComponent<Interactable>();
                        //interaction.Interact();
                        //state.SetIsInteracting(false);
                    }
                }
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, (hit.point - transform.position).normalized * interactionDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hit.point, sphereCastRadius);
    }
}
