using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
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
#if UNITY_ANDROID || UNITY_IOS
    private VirtualJoystick joystick;
#endif
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

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponent<Animator>();

        rb.freezeRotation = true;
#if UNITY_ANDROID || UNITY_IOS
        joystick = FindObjectOfType<VirtualJoystick>();
#endif
    }

    void FixedUpdate()
    {
        Vector2 moveDir = new Vector2();
#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
        if (joystick != null)
        {
            moveDir.x = joystick.Horizontal();
            moveDir.y = joystick.Vertical();
        }
#else
        foreach (var dir in directions)
            if (Input.GetKey(Globals.keyBinds[$"{dir.Key}_0"].ToString())
                || Input.GetKey(Globals.keyBinds[$"{dir.Key}_1"].ToString()))
                moveDir += dir.Value;

        rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime);
    }
#endif
}
