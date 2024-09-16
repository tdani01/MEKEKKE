using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject makers;
    public Button button;
    public Sprite img;

    public GameObject[] CutScene;
    public VideoClip[] vp;
    private float videoTimer = 0;
    int i = 0;
    bool videoStart = false;

    private void Update()
    {
        if (videoStart)
        {
            videoTimer += Time.deltaTime;
        }

        if (videoTimer >= vp[i].length + 3d && videoStart)
        {
            Debug.Log(PolicePoints.Police_Point);

            i++;
            if(i != 2)
            {
                CutScene[i].SetActive(true);
            }
            videoTimer = 0;
            if(i == 2)
            {
                SceneManager.LoadScene(1);
            }

        }
    }

    public void gameStart()
    {
        videoStart = true;
        CutScene[i].SetActive(true);
        menu.SetActive(false);
        
    }
    public void Makers()
    {
        makers.SetActive(true);
        menu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void BackToMenu()
    {
        makers.SetActive(false);
        menu.SetActive(true);
        button.image.sprite = img;
    }


}
