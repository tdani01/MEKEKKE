using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject menu;
    public GameObject makers;

    public void gameStart()
    {
        
        SceneManager.LoadScene(1);
    }
    public void Makers()
    {
        menu.SetActive(false);
        makers.SetActive(true);
    }

    public void Exit()
    {

    }
    
    public void Back()
    {
        makers.SetActive(false);
        menu.SetActive(true);
    }

}
