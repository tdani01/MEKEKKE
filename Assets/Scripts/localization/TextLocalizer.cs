using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocalizer : MonoBehaviour
{
    public string key;

    private void Start()
    {
        LocalizeText();
    }

    void LocalizeText()
    {
        Text textComponent = GetComponent<Text>();
        if (textComponent != null)
        {            
            textComponent.text = LocalizationManager.instance.GetText(key);
        }
    }       
}
