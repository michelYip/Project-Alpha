using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        EventManager.current.onDoorwayTriggerClose += OnDoorwayClose;
    }

    private void OnDoorwayOpen(int id)
    {
        if (this.id == id)
        {
            LeanTween.moveLocalY(gameObject, -0.3f, 1f).setEaseOutQuad();
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (this.id == id)
        {
            LeanTween.moveLocalY(gameObject, -10f, 1f).setEaseInQuad();
        }
    }
    
}
