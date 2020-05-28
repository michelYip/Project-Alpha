using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField]
    public List<PlayerController> playerEntities = new List<PlayerController>();

    void Awake()
    {
        int i = 0;
        int j = 0;
        while (playerEntities.Count != 0)
        {
            j = i % playerEntities.Count;
            if (playerEntities[j].isInit) {
                playerEntities.RemoveAt(j);
            }
            else
            {
                playerEntities[j].init();
            }
            i++;
        }
    
    }
}
