using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find_obj : MonoBehaviour
{
    
    [SerializeField] public GameObject winScreen;
    [SerializeField] public Color normalColor = new(255, 255, 255, 1);
    [SerializeField] public Color hoverColor;
    public GameVariables gv = new GameVariables();
    private bool clicked = false; // random bugfix, dont touch
    private bool inside = false;
    private SpriteRenderer sr;
    private GameObject bg;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bg = GameObject.Find("bg");
    }

    private void Update()
    {
        if (gv.counter >= 4 || (gv.counter == 3 && gv.help))
        {
            winScreen.SetActive(true);
            bg.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
        inside = true;
    }

    private void OnMouseExit()
    {
        sr.color = normalColor;
        inside = false;

    }
    private void OnMouseDown()
    {
        clicked = true;
    }

    private void OnMouseUp()
    {
        GameObject icon;
        if (inside && clicked) {
            switch (gameObject.name)
            {
                case "rex_obj":
                    icon = GameObject.Find("rex_icon");
                    break;

                case "stego_obj":
                    icon = GameObject.Find("stego_icon");
                    break;

                case "trike_obj":
                    icon = GameObject.Find("trike_icon");
                    break;

                case "bronto_obj":
                    icon = GameObject.Find("bronto_icon");
                    break;

                case "bg":
                    if (!gv.help) 
                        GetSomeHelp();
                    
                    icon = new GameObject();
                    break;

                default:
                    icon = new GameObject();
                    break;
            }
            if (gv.help) return;
            gv.counter++;
            icon.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void GetSomeHelp() // he needs some milk
    {
        GameObject icon;
        GameObject[] shits = new GameObject[4] { GameObject.Find("trike_obj"), GameObject.Find("stego_obj"), GameObject.Find("bronto_obj"), GameObject.Find("rex_obj") };
        foreach (GameObject item in shits)
        {
            if (item.active)
            {
                switch (gameObject.name)
                {
                    case "rex_obj":
                        icon = GameObject.Find("rex_icon");
                        icon.SetActive(false);
                        item.SetActive(false);
                        break;

                    case "stego_obj":
                        icon = GameObject.Find("stego_icon");
                        icon.SetActive(false);
                        item.SetActive(false);
                        break;

                    case "trike_obj":
                        icon = GameObject.Find("trike_icon");
                        icon.SetActive(false);
                        item.SetActive(false);
                        break;

                    case "bronto_obj":
                        icon = GameObject.Find("bronto_icon");
                        icon.SetActive(false);
                        item.SetActive(false);
                        break;
                }
            }
        }
        Debug.LogWarning("No other objects to set false");
        gv.help = true;
    }
}
