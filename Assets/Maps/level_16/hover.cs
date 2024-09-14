using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(PolygonCollider2D))]
public class hover : MonoBehaviour
{
    [SerializeField] public Color normalColor = new(255, 255, 255, 1);
    [SerializeField] public Color hoverColor;
    private bool inside = false;
    private bool clicked = false;
    private SpriteRenderer sr;
    public GameObject icon;

    private GameObject Manager;
    public help mgrScript;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        Manager = GameObject.Find("HelpBTN");
        mgrScript = Manager.GetComponent<help>();
        InitLevel();
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
        if (clicked && inside)
        {
            mgrScript.Found(gameObject, icon);
        }
    }

    private void OnEnable()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        if (gameObject.activeSelf)
            mgrScript.elements.Add(gameObject);
    }
}
