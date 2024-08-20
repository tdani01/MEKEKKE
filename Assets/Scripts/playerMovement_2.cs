/*This code is from a previous project, here for testing*/
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private int jumpCounter;
    private Collider2D platformCollider;
    [SerializeField] private bool isDoubleJump = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        if (isDoubleJump)
        {
            jumpCounter = 0;
        }
    }

    void Update()
    {
        if (jumpCounter > 0 && IsGrounded())
            jumpCounter = 0;
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !isDoubleJump)
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        else if (isDoubleJump && Input.GetKeyDown(KeyCode.Space) && jumpCounter < 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpCounter++;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        /*add crouch*/

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}