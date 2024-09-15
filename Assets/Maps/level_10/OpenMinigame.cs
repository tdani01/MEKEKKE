using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMinigame : MonoBehaviour
{
    //public GameObject GameInfo;
    private bool StandObject = false;
    //private bool stop = false;
    private string StandingName = "";
    public GameObject[] Helps;
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && StandObject)
        {
            //Debug.Log(StandingName);
            switch (StandingName)
            {
                case "Hospital":
                    Helps[1].SetActive(true);
                    break;
                case "PlayGround":
                    Helps[2].SetActive(true);
                    break;
                case "CityHall":
                    Helps[3].SetActive(true);
                    break;
                case "Museum":
                    Helps[4].SetActive(true);
                    break;
                case "Police":
                    //if(Police_Points < 5)
                    Helps[0].SetActive(true);
                    break;
                default:
                    Debug.Log("Hiba");
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MiniGame"))
        {
            //Debug.Log(collision.name);
            StandObject = true;
            StandingName = collision.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MiniGame"))
        {
            //Debug.Log("Lelépett");
            StandingName = "";
            StandObject = false;
        }
    }

}
