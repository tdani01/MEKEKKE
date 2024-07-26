using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_TopDown : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;
    public Sprite[] playerSprites = new Sprite[4];
    private Rigidbody2D rb;
    private Animator anim;
    // 0 - Left
    // 1 - Up
    // 2 - Right
    // 3 - Down
    public float moveSpeed = 5f;

    private Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>
    {
        {"Forward", Vector2.up }
        ,{"Left", Vector2.left}
        ,{"Right", Vector2.right }
        ,{"Down", Vector2.down }
    };

    Vector2 movement;
    void Start()
    {
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        playerInstance.AddComponent<Rigidbody2D>();
        playerInstance.AddComponent<SpriteRenderer>();
        playerInstance.AddComponent<Movement_TopDown>();
        playerInstance.AddComponent<Animator>();

        if (rb == null )
            rb = GetComponent<Rigidbody2D>();
        if (anim == null )
            anim = GetComponent<Animator>();   
        
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Vector2 moveDir = new Vector2();
        foreach (var dir in directions)
            if (Input.GetKey(Globals.keyBinds[$"{dir.Key}_0"].ToString())
                || Input.GetKey(Globals.keyBinds[$"{dir.Key}_1"].ToString()))
                moveDir += dir.Value;
        // implement joystick again cuz earlier version was shit
        rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
// w.i.p
public class Movement_Platformer : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;
    public Sprite[] playerSprites = new Sprite[4];
    private Rigidbody2D rb;
    private Animator anim;
    // 0 - Left
    // 1 - Up
    // 2 - Right
    // 3 - Down
    public float moveSpeed = 5f;

    private Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>
    {
        {"Jump", Vector2.up }
        ,{"Left", Vector2.left}
        ,{"Right", Vector2.right }
        //,{"Down", Vector2.down }        
    };

    Vector2 movement;
    void Start()
    {
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        playerInstance.AddComponent<Rigidbody2D>();
        playerInstance.AddComponent<SpriteRenderer>();
        playerInstance.AddComponent<Movement_TopDown>();
        playerInstance.AddComponent<Animator>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Vector2 moveDir = new Vector2();
        foreach (var dir in directions)
            if (Input.GetKey(Globals.keyBinds[$"{dir.Key}_0"].ToString())
                || Input.GetKey(Globals.keyBinds[$"{dir.Key}_1"].ToString()))
                moveDir += dir.Value;
        rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
