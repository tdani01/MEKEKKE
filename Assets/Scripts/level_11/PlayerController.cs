using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float movSpeed, wallCD, wallDecay;
    [SerializeField] private Animator animator;
    [SerializeField] private Direction dir;

    public Transform movePoint;
    public LayerMask noMovementLayer, utilLayer, enemyLayer;

    public int util_Counter = 0;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        movePoint.parent = null;
        Vector3 np = new Vector3(GridManager.Instance.playerSpawner.transform.position.x, GridManager.Instance.playerSpawner.transform.position.y);
        gameObject.transform.position = np;
           // gameObject.transform.position = new Vector3(2, 2);
        gameObject.transform.rotation = Quaternion.identity;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        Debug.Log(go.name);
        /*if (collision.gameObject.layer == utilLayer) // utility collision
        {
            collision.gameObject.GetComponent<Utility>().GetUtil();
        }
        if (collision.gameObject.layer == enemyLayer) // enemy collision
        {
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
        */
        if (gameObject.GetComponent<Collider2D>().bounds.Intersects(collision.gameObject.GetComponent<Collider2D>().bounds) && collision.gameObject.name.StartsWith("Shield"))
        {
            util_Counter++;
            Destroy(collision.gameObject);
        }
    }

    void Update()
    { 
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movSpeed * Time.deltaTime);
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        if (Vector3.Distance(transform.position, movePoint.position) == 0f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .3f, noMovementLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }                
            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .3f, noMovementLayer))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //transform.position = transform.position + movement * Time.deltaTime * movSpeed;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {            
            dir = Direction.LEFT;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            dir = Direction.RIGHT;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            dir = Direction.UP;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            dir = Direction.DOWN;
        }
    }

    private void SpawnWall()
    {

    }
}
enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
