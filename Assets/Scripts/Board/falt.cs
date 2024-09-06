using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class falt : MonoBehaviour
{

    private int healtpoint;
    public GameObject gameover;
    public GameObject pause;
    public Text point;
    public Text gopoint;
    public GameObject bgspawner;
    public GameObject linespawner;

    private void Start()
    {
        healtpoint = 3;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MLine") || collision.CompareTag("SLine") || collision.CompareTag("LLine"))
        {
            healtpoint -= 1;
            Debug.Log(healtpoint);
            if(healtpoint <= 0)
            {
                GameOver();
            }

        }
    }

    private void GameOver()
    {
        gopoint.text = "A pontod:\n" + point.text.ToString();
        point.enabled = false;
        gameover.SetActive(true);
        bgspawner.SetActive(false);
        linespawner.SetActive(false);
    }

}
