using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    // Events to open and close a doorway
    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }
    public event Action<int> onDoorwayTriggerClose;
    public void DoorwayTriggerClose(int id)
    {
        if (onDoorwayTriggerClose != null)
        {
            onDoorwayTriggerClose(id);
        }
    }
}
