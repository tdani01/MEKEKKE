using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_4 : MonoBehaviour
{
    public byte _part = 0;
    public GameObject[] obj = new GameObject[6];
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && obj[_part].GetComponent<Collider2D>().OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)))
        {

        }
    }

    public void Found()
    {

    }
}
