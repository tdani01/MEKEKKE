using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_move : MonoBehaviour
{
    public float sebesseg;
    public GameObject obj;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(this.transform.position.x - sebesseg, this.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("destroyer"))
        {
            Destroy(obj);
        }
    }
}
