using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public float timeBetweenAction = 1f;
    public float timer;
    public Transform movePoint;
    float m_x, m_y;
    public LayerMask noMovementLayer;
    private void Start()
    {
        timer = timeBetweenAction;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer == 0.0f)
        {
            int rnd = Random.Range(0, 2);
            switch (rnd)
            {
                case 0:
                    m_x = Random.Range(-1, 1);
                    break;

                case 1:
                    m_y = Random.Range(-1, 1);
                    break;
            }
            timer = timeBetweenAction;
        }
        
        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            if (Mathf.Abs(m_y) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(m_y, 0f, 0f), .3f, noMovementLayer))
                {
                    movePoint.position += new Vector3(m_y, 0f, 0f);
                }
            }

            else if (Mathf.Abs(m_x) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, m_x, 0f), .3f, noMovementLayer))
                {
                    movePoint.position += new Vector3(0f, m_x, 0f);
                }
            }
        }
    }
}
