using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButton : MonoBehaviour
{
    
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToCity()
    {
        SceneManager.LoadScene(2);
        
    }

    public void GoCity(int gameId)
    {
        PolicePoints.AddPoint(gameId);
        SceneManager.LoadScene(2);
    }

    public void OpenMinigames(int gameId)
    {
        SceneManager.LoadScene(gameId);
    }
    public GameObject FireWall;
    public void LeavePolice()
    {
        FireWall.SetActive(false);
    }

}
