using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class help : MonoBehaviour
{
    [Header("Basic settings")]
    [SerializeField] private byte _part = 0;
    [SerializeField] private byte counter = 0;
    private Camera mainCamera;
    public bool isHelped = false;
    public int score = 0;
    public int levelCount = 1;
    public List<GameObject> elements = new List<GameObject>();
    [Header("Maps")]
    public GameObject lv1Map;
    public GameObject lv2Map;
    public GameObject lv3Map;
    public GameObject lv4Map;
    public GameObject lv5Map;
    public GameObject lv6Map;
    [Header("Win")]
    public GameObject winScreen;
    public TextMeshProUGUI score_object;
    [Header("Win/Buttons")]
    public GameObject btnNext;
    public GameObject btnRetry;
    [Header("help")]
    public GameObject helpbtn;
    private void Start()
    { 
        mainCamera = Camera.main;
        
    } // 0-3 obj, 4-7 icon
    private void SwitchState()
    {
        counter = 0;
        isHelped = false;
        switch (_part)
        {
            case 0:
                lv1Map.SetActive(false);
                lv2Map.SetActive(true);
                _part++;
                return;
            case 1:
                lv2Map.SetActive(false);
                lv3Map.SetActive(true);
                _part++;
                return;
            case 2:
                lv3Map.SetActive(false);
                lv4Map.SetActive(true);
                _part++;
                return;
            case 3:
                lv4Map.SetActive(false);
                lv5Map.SetActive(true);
                _part++;
                return;
            case 4:
                lv5Map.SetActive(false);
                lv6Map.SetActive(true);
                _part++;
                return;
            case 5: // win case
                lv6Map.SetActive(false);
                if (score == 24)
                    btnRetry.SetActive(false);
                helpbtn.GetComponent<SpriteRenderer>().sprite = null;
                score_object.text = score.ToString();
                winScreen.SetActive(true);
                return;
        }       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _part = 5;
            counter = 4;
            score = 23;
        }
        if (counter >= 4)
        {
            Debug.Log($"Score: {score}\nCounter: {counter}");
            SwitchState();
            
        }
        if (!isHelped && Input.GetKeyDown(KeyCode.Mouse0) && gameObject.GetComponent<Collider2D>().OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition)))
        {
            Found(elements[0], elements[0].GetComponent<hover>().icon, true);       
            isHelped = true;
        }
    }

    public void Found(GameObject obj, GameObject icon, bool isHelp = false)
    {
        icon.SetActive(false);
        obj.SetActive(false);
        elements.Remove(obj);
        if (!isHelp)
            score++;
        counter++;
    }

    public void BTN_Next()
    {
        //SceneManager
    }

    public void BTN_Retry()
    {
        counter = 0;
        _part = 0;
        score = 0;
        isHelped = false;
        winScreen.SetActive(false);
    }
}
