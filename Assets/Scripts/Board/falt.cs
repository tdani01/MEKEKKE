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
            if (item.activeSelf)
            {
                hpwatch++;
            }
        }
        //Debug.Log(healtpoint);
        healtpoint = hpwatch;
        if (healtpoint <= 0 && !gameover.activeSelf)
        {

            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("MLine") || collision.CompareTag("SLine") || collision.CompareTag("LLine")) && !winCanvas.activeSelf)
        {
            healtpoint -= 1;
            if(healtpoint >= 0 && !gameover.activeSelf)
            {
                HP[healtpoint].SetActive(false);
            }
            //Debug.Log(healtpoint);
            

        }
        Destroy(collision.gameObject);
    }

    private void GameOver()
    {
        
        gopoint.text = point.text;
        gameover.SetActive(true);
        bgspawner.SetActive(false);
        linespawner.SetActive(false);
    }

}
