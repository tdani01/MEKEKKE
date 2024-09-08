using UnityEngine;
public class playerController : MonoBehaviour
{
    public float movSpeed = 5f;
    public GameObject firePrefab;
    public LayerMask fireLayerMask;

    private Vector2 movInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandlePlacement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movInput = new Vector2 (moveX, moveY).normalized;
    }

    void HandlePlacement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dir = new Vector2(movInput.x, movInput.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, fireLayerMask);
            if (!hit.collider) 
            {
                Vector3 spawnPos = transform.position + new Vector3(movInput.x, movInput.y, 0f);
                Instantiate(firePrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movInput * movSpeed * Time.fixedDeltaTime);
    }
}
