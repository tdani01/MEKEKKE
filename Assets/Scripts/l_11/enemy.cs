using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float movSpeed = 3f;
    public float movInterval = 2f;

    private Vector2 movDir;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(ChangeDir), 0, movInterval);
    }

    void ChangeDir()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case 0: movDir = Vector2.up; break;
            case 1: movDir = Vector2.down; break;
            case 2: movDir = Vector2.left; break;
            case 3: movDir = Vector2.right; break;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movDir * movSpeed * Time.fixedDeltaTime);
    }
}
