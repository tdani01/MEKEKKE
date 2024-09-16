using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public float leftcoordinata, rightcoordinata;
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > leftcoordinata && player.transform.position.x < rightcoordinata)
        {
            camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        }

    }
}
