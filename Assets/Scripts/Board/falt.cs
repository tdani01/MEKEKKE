using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class falt : MonoBehaviour
{

    private int healtpoint;
    public GameObject gameover;
    public Text point;
    public Text gopoint;
    public GameObject bgspawner;
    public GameObject linespawner;
    public GameObject[] HP;
    public GameObject winCanvas;
    

    private void Start()
    {
        healtpoint = 3;
    }

    private void Update()
    {
        int hpwatch = 0;
        foreach (GameObject item in HP)
        {
            if (item.active)
            {
                hpwatch++;
            }
        }
        //Debug.Log(healtpoint);
        healtpoint = hpwatch;
        if (healtpoint <= 0 && !gameover.active)
        {

            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("MLine") || collision.CompareTag("SLine") || collision.CompareTag("LLine")) && !winCanvas.active)
        {
            healtpoint -= 1;
            if(healtpoint >= 0 && !gameover.active)
            {
                HP[healtpoint].SetActive(false);
            }
            //Debug.Log(healtpoint);
            

        }
        Destroy(collision.gameObject);
    }

    private void GameOver()
    {
        string[] validpoint = point.text.ToString().Split('/');
        gopoint.text = validpoint[0];
        gameover.SetActive(true);
        bgspawner.SetActive(false);
        linespawner.SetActive(false);
    }

}
