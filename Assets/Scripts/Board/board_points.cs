using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class board_points : MonoBehaviour
{
    private int points;
    public Text pointtext;
    private GameObject line;
    private bool detected = false;
    public GameObject[] HP;
    public GameObject win;
    public Text winpoint;
    private int minpoint = 1050;


    public GameObject bgspawner;
    public GameObject linespawner;

    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && detected)
        {
            if (line.CompareTag("SLine"))
            {
                points += 30;
                Destroy(line);
            }
            else if(line.CompareTag("MLine"))
            {
                points += 20;
                Destroy(line);
            }
            else if (line.CompareTag("LLine"))
            {
                points += 10;
                Destroy(line);
            }
            else
            {
                Debug.Log("Invalid object!");
                Destroy(line);
            }
            detected = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !win.activeSelf)
        {
            try
            {
                int hp = 0;
                foreach (GameObject item in HP)
                {
                    if (item.activeSelf)
                    {
                        hp++;
                    }
                }
                hp--;
                HP[hp].SetActive(false);
            }
            catch (System.Exception)
            {

            }

        }        
        if(points >= minpoint)
        {
            Victory();
        }
        pointtext.text = points.ToString() + "/" + minpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MLine") || collision.CompareTag("SLine") || collision.CompareTag("LLine"))
        {
            detected = true;
            line = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MLine") || collision.CompareTag("SLine") || collision.CompareTag("LLine"))
        {
            detected = false;
        }
    }

    public void Victory()
    {
        winpoint.text = points.ToString();
        win.SetActive(true);
        bgspawner.SetActive(false);
        linespawner.SetActive(false);        
    }
}
