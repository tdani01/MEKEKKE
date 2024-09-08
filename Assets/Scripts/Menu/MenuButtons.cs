using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject menu;
    public GameObject makers;
    public GameObject notImplemented;

    private GameObject lastOpened;
    private GameObject current;

    private void Start()
    {
       lastOpened = menu;
       current = menu;
    }

    public void gameStart()
    {   
        notImplemented.SetActive(true);
        //SceneManager.LoadScene(1);
    }
    public void Makers()
    {
        Switch(menu, makers);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Back()
    {
        GameObject temp = current;
        lastOpened.SetActive(true);
        current.SetActive(false);
        current = lastOpened;
        lastOpened = temp;
    }

    public void back2()
    {
        notImplemented.SetActive(false);
    }

    public void Switch(GameObject prev, GameObject next)
    {
        lastOpened = prev;
        current = next;
        prev.SetActive(false);
        next.SetActive(true);
    }
}
