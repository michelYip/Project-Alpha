using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerController> peList = new List<PlayerController>();

    void Awake()
    {
        fillPlayerEntities();

        int i = 0;
        int j = 0;
        List<PlayerController> peListCopy = new List<PlayerController>(peList); // For Debugging purpose
        while (peListCopy.Count != 0)
        {
            j = i % peListCopy.Count;
            if (peListCopy[j].isInit)
                peListCopy.RemoveAt(j);
            else
                peListCopy[j].init();
            i++;
        }
    
    }

    private void fillPlayerEntities()
    {
        Transform playerEntities = transform.Find("PlayerEntities");
        foreach(Transform child in playerEntities)
        {
            PlayerController pe = child.GetComponent<PlayerController>();
            if (pe != null)
                peList.Add(pe);
            else
                Debug.Log("You might have forgotten to add a PlayerController here.");
        }
    }
}
