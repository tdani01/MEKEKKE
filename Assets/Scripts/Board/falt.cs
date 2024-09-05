using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class falt : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("MLine") || collision.collider.CompareTag("SLine") || collision.collider.CompareTag("LLine"))
        {
            Debug.Log("Fail");
        }
    }

}
