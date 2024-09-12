using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portals : MonoBehaviour
{
    public GameObject player;

    public GameObject top_portal;
    public GameObject bot_portal;

    public void enterPortal(GameObject enterObj, GameObject destinationObj)
    {
        if (enterObj != null && enterObj != destinationObj)
        {
            
        }
        else return;
    }
}
