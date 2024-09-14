using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class help : MonoBehaviour
{
    public byte _part = 0;
    public byte counter = 0;
    public Dictionary<byte, GameObject[]> elements = new Dictionary<byte, GameObject[]>();
    private Camera mainCamera;
    public bool isHelped = false;
    int score = 0;
    public byte levelCount = 6;
    private void Start()
    { 
        for (byte i = 0; i < levelCount; i++)
        {
            elements[i] = new[]
            {
                GameObject.Find($"level_{levelCount}").transform.GetChild(0).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(1).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(2).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(3).gameObject,

                GameObject.Find($"level_{levelCount}").transform.GetChild(4).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(5).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(6).gameObject,
                GameObject.Find($"level_{levelCount}").transform.GetChild(7).gameObject,

                GameObject.Find($"level_{levelCount}")
            };
        }
        mainCamera = Camera.main;
    } // 0-3 obj, 4-7 icon
    public void Help(byte part)
    {        
       
    }

    private void SwitchState()
    {
        elements[_part][8].SetActive(false);
        _part++;
        elements[_part][8].SetActive(true);
        counter = 0;
        isHelped = false;
    }

    private void Update()
    {
        if (counter >= 4)
        {
            SwitchState();
        }
        if (_part <= 6 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = 0; i < 4; i++)
            {
                if (elements[_part][i].GetComponent<Collider2D>().OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)))
                {
                    elements[_part][i].SetActive(false);
                    elements[_part][i+4].SetActive(false);
                    counter++;
                    score++;
                }
            }
            if (!isHelped && gameObject.GetComponent<Collider2D>().OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (elements[_part][i].activeSelf)
                    {
                        elements[_part][i].SetActive(false);
                        elements[_part][i+4].SetActive(false);
                        counter++;
                        isHelped = true;
                    }
                }
            }
        }
    }

    public void WinGame()
    {

    }

    /*public void LoseGame() Igazából nem tudja elveszíteni
    {

    }*/
}
