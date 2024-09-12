using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BoardPress : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Pressed", true);
        }
        else
        {
            animator.SetBool("Pressed", false);
        }
    }
}
