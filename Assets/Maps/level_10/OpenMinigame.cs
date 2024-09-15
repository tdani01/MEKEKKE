using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpenMinigame : MonoBehaviour
{
    //public GameObject GameInfo;
    private bool StandObject = false;
    //private bool stop = false;
    private string StandingName = "";
    public GameObject[] Helps;
    public GameObject FinalCutScene;

    public VideoClip vp;

    private float videoTimer = 0;
    private bool end = false;

    private void Start()
    {
        if(PolicePoints.Police_Point == 4)
        {
            Helps[7].SetActive(true);
        }
        else if(PolicePoints.Police_Point == 0)
        {
            Helps[0].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (end)
        {
            videoTimer += Time.deltaTime;
        }

        if(videoTimer >= vp.length + 3d && end)
        {
            Debug.Log(PolicePoints.Police_Point);
            
            
            SceneManager.LoadScene(0);

        }

        if (Input.GetKeyDown(KeyCode.Space) && StandObject && !end)
        {
            //Debug.Log(StandingName);
            switch (StandingName)
            {
                case "Police":
                    if (PolicePoints.Police_Point < 4)
                    {
                        Helps[5].SetActive(true);
                    }
                    else
                    {
                        PolicePoints.Clear();
                        end = true;
                        FinalCutScene.SetActive(true);//VégeCutscene
                    }
                    break;
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
                case "Library":
                    Helps[6].SetActive(true);
                    break;
                case "Opera":
                    Helps[6].SetActive(true);
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
