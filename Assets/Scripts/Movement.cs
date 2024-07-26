using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_TopDown : MonoBehaviour
{
    public GameObject player;
    public Sprite[] playerSprites = new Sprite[4];
    // 0 - Left
    // 1 - Up
    // 2 - Right
    // 3 - Down

    void Start()
    {
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<SpriteRenderer>();
        player.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    void Update()
    {
        
    }
}

public class Movement_Platformer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
