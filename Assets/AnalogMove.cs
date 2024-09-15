using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogMove : MonoBehaviour
{
    public GameObject player;
    public float movementspeed;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        animator.SetFloat("Horizontal", movement.x);
        transform.position = transform.position + movement * Time.deltaTime * movementspeed;
    }
}
