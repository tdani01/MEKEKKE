using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject makers;
    public Button button;
    public Sprite img;

    public void gameStart()
    {   
        
        SceneManager.LoadScene(1);
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
