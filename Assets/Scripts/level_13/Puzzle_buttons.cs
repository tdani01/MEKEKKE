using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle_buttons : MonoBehaviour
{

    public GameObject button;
    private bool selected = false;
    public UnityEvent Select = new UnityEvent();
    public UnityEvent Unselect = new UnityEvent();


    public void OnMouseDown()
    {
        if (selected)
        {
            button.transform.localScale = new Vector3(button.transform.localScale.x - 0.2f, button.transform.localScale.y - 0.2f, button.transform.localScale.z);
            selected = false;
            Unselect.Invoke();
        }
        else
        {
            button.transform.localScale = new Vector3(button.transform.localScale.x + 0.2f, button.transform.localScale.y + 0.2f, button.transform.localScale.z);
            selected = true;
            Select.Invoke();
        }

        
        
    }


}
