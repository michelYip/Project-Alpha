using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        EventManager.current.DoorwayTriggerEnter(id);
    }

    private void OnTriggerExit(Collider other)
    {
        EventManager.current.DoorwayTriggerClose(id);
    }
}
