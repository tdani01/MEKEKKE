using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_detect : MonoBehaviour
{
    public GameObject line;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("detector") && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Siker");
            Destroy(line);
        }
    }
}
