using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasButton : MonoBehaviour
{
    public GameObject segitsegPanel;
    public float ido = 5f;
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToCity()
    {
        //SceneManager.LoadScene();
    }
    public void SegitsegBezar()
    {
        Destroy(segitsegPanel);
        StartCoroutine(WaitUntilGameStarts(ido));
    }
    IEnumerator WaitUntilGameStarts(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
