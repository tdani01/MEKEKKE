using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyMapEditor : MonoBehaviour
{
    public GameObject keyMapPanel;
    public GameObject keyMapButtonPrefab;
    public GameObject keyLabel; // to add
    public Dictionary<KeyHandler.KeyAction, Button[]> keyMapButtons;
    private KeyHandler.KeyAction currentModify;

    private GameObject QuitButton;


    private bool isUIInitialized = false;

    public void ShowKeyMapEditor()
    {
        if (!isUIInitialized)
        {
            // init editor
            isUIInitialized = true;
        }
        keyMapPanel.SetActive(true);
    }

    public void HideKeyMapEditor() { keyMapPanel.SetActive(false); }

    private void InitializeKeyMapEditorUI()
    {
        keyMapButtons = new Dictionary<KeyHandler.KeyAction, Button[]>();

        foreach (KeyHandler.KeyAction action in Enum.GetValues(typeof(KeyHandler.KeyAction)))
        {
            GameObject buttonObj = Instantiate(keyMapButtonPrefab, keyMapPanel.transform);

            Button button1 = buttonObj.GetComponent<Button>();
            Text buttonText1 = buttonObj.GetComponent<Text>();

            Button button2 = buttonObj.GetComponent<Button>();
            Text buttonText2 = buttonObj.GetComponent<Text>();

            KeyCode[] keys = KeyHandler.Instance.getKeys(action);
            
            buttonText1.text = $"{action}: {keys[0]}";
            buttonText2.text = $"{action}: {keys[1]}";

            keyMapButtons[action] = new Button[2] { button1, button2 };

            button1.onClick.AddListener(() => OnKeyMapButtonClicked(action, 0));
            button2.onClick.AddListener(() => OnKeyMapButtonClicked(action, 1));
        }
    }

    private void OnKeyMapButtonClicked(KeyHandler.KeyAction keyAction, int key)
    {
        currentModify = keyAction;

        foreach (var buttons in keyMapButtons.Values)
        {
            buttons[0].interactable = false;
            buttons[1].interactable = false;
        }

        StartCoroutine(WaitForKeyInput(key));
    }

    private IEnumerator WaitForKeyInput(int key)
    {
        while (!Input.anyKeyDown)
            yield return null;

        KeyCode newKey = KeyCode.None;
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code))
            {
                newKey = code;
                break;
            }
        }

        if (newKey != KeyCode.None)
        {
            switch (key)
            {
                case 0:
                    KeyHandler.Instance.ModifyMap(currentModify, newKey1: newKey);
                    keyMapButtons[currentModify][0].GetComponentInChildren<Text>().text = $"{currentModify}: {newKey}";
                    break;

                case 1:
                    KeyHandler.Instance.ModifyMap(currentModify, newKey2: newKey);
                    keyMapButtons[currentModify][1].GetComponentInChildren<Text>().text = $"{currentModify}: {newKey}";
                    break;
            }            
        }

        foreach (var buttons in keyMapButtons.Values)
        {
            buttons[0].interactable = true;
            buttons[1].interactable = true;
        }
    }
}
