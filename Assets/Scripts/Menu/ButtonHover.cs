using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Button button;
    public Sprite img1;
    public Sprite img2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.sprite = img1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.sprite = img2;
    }
}
