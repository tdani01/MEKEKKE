using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class board_points : MonoBehaviour
{
    private int points;
    public Text pointtext;
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        pointtext.text = points.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            if (collision.CompareTag("SLine"))
            {
                points += 3;
            }
            if (collision.CompareTag("MLine"))
            {
                points += 2;
            }
            if (collision.CompareTag("LLine"))
            {
                points += 1;
            }
        }
    }
}
