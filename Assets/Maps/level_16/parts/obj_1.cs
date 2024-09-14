using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_1 : MonoBehaviour
{
    public byte _part = 0;
    public GameObject[] obj = new GameObject[6];
    
    void Start()
    {
        
    }

    public void Found()
    {
        gameObject.SetActive(false);

    }

    public void OnMouseUp()
    {
        Found();

    }
}
