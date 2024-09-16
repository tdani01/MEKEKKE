using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AnalogMove : MonoBehaviour
{
    public GameObject player;
    public float movementspeed;
    public Animator animator;

    public Rigidbody2D rg;
    public float jump;
    bool jumping;

    

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            transform.position = transform.position + movement * Time.deltaTime * movementspeed;

            if (Input.GetButtonDown("Jump") && !jumping)
            {
                rg.AddForce(new Vector2(rg.velocity.x, jump));
                animator.SetBool("Jump", true);
                jumping = true;
            }

        

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetBool("Jump", false);
        jumping = false;
    }


}
