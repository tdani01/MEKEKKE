using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class keyMapEditor : MonoBehaviour
{
    public GameObject keyMapPanel;
    public GameObject keyMapButtonPrefab;
    public GameObject keyLabelPrefab;
    private GameObject QuitButtonPrefab;

    public Dictionary<KeyHandler.KeyAction, (Button primaryKey, Button secondaryKey)> keyMapButtons;
    private KeyHandler.KeyAction currentModify;
    private bool modifyprimaryKey;

    private bool isUIInitialized = false;

    private void Start()
    {
        // add quit button
    }

    public void ShowKeyMapEditor()
    {
        if (!isUIInitialized)
        {
            InitializeKeyMapEditorUI();
            isUIInitialized = true;
        }
        keyMapPanel.SetActive(true);
    }

    public void HideKeyMapEditor() { keyMapPanel.SetActive(false); }

    private void InitializeKeyMapEditorUI()
    {
        keyMapButtons = new Dictionary<KeyHandler.KeyAction, (Button, Button)>();

        foreach (KeyHandler.KeyAction action in Enum.GetValues(typeof(KeyHandler.KeyAction)))
        {
            CreateKeyMappingButtonsRow(action);
        }

        GameObject quitButtonObj = Instantiate(QuitButtonPrefab, keyMapPanel.transform);
        Button quitButton = quitButtonObj.GetComponent<Button>();
        Text quitButtonText = quitButton.GetComponentInChildren<Text>();
        quitButtonText.text = "Vissza";

        quitButton.onClick.AddListener(() => HideKeyMapEditor());
    }

    private void CreateKeyMappingButtonsRow(KeyHandler.KeyAction keyAction)
    {
        GameObject row = new GameObject(keyAction.ToString() + "_Row");
        row.transform.SetParent(keyMapPanel.transform, false);

        HorizontalLayoutGroup layoutGroup = row.AddComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing = 20;

        GameObject labelObj = Instantiate(keyLabelPrefab, row.transform);
        Text label = labelObj.GetComponent<Text>();
        label.text = SetActionName(keyAction.ToString());

        GameObject primaryButtonObj = Instantiate(keyMapButtonPrefab, row.transform);
        Button primaryButton = primaryButtonObj.GetComponent<Button>();
        Text primaryButtonText = primaryButton.GetComponentInChildren<Text>();
        KeyCode[] keys = KeyHandler.Instance.getKeys(keyAction);
        primaryButtonText.text = $"Elsõdleges: {keys[0]}";
        //--
        GameObject secondaryButtonObj = Instantiate(keyMapButtonPrefab, row.transform);
        Button secondaryButton = secondaryButtonObj.GetComponent<Button>();
        Text secondaryButtonText = secondaryButton.GetComponentInChildren<Text>();
        secondaryButtonText.text = $"Másodlagos: {keys[1]}";

        keyMapButtons[keyAction] = (primaryButton, secondaryButton);

        primaryButton.onClick.AddListener(() => OnKeyMapButtonClicked(keyAction, true));
        secondaryButton.onClick.AddListener(() => OnKeyMapButtonClicked(keyAction, false));
    }

    private string SetActionName(string actionName)
    {
        if (!string.IsNullOrEmpty(actionName))
        {
            if (actionName.Length > 15)
                return actionName.Substring(0,15);
            return actionName;
        }
        else
            Debug.LogWarning("SetActionName got a null string from action Enum");
        return "";
    }

    private void OnKeyMapButtonClicked(KeyHandler.KeyAction keyAction, bool isPrimaryKey)
    {
        currentModify = keyAction;
        modifyprimaryKey = isPrimaryKey;

        foreach (var (primaryButton, secondaryButton) in keyMapButtons.Values)
        {
            primaryButton.interactable = false;
            secondaryButton.interactable = false;
        }

        StartCoroutine(WaitForKeyInput());
    }

    private IEnumerator WaitForKeyInput()
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
            if (modifyprimaryKey)
            {
                KeyHandler.Instance.ModifyMap(currentModify, newKey1: newKey);
                keyMapButtons[currentModify].primaryKey.GetComponentInChildren<Text>().text = $"Elsõdleges: {newKey}";
            }
            else
            {
                KeyHandler.Instance.ModifyMap(currentModify, newKey2: newKey);
                keyMapButtons[currentModify].primaryKey.GetComponentInChildren<Text>().text = $"Másodlagos: {newKey}";
            }
        }

        foreach (var (primaryButton, secondaryButton) in keyMapButtons.Values)
        {
            primaryButton.interactable = true;
            secondaryButton.interactable = true;
        }
    }

    private IEnumerator CheckForEscapeKey()
    {
        while (true)
        {
            if (KeyHandler.Instance.GetKeyDown(KeyHandler.KeyAction.i_Back))
            {
                HideKeyMapEditor();
                break;
            }
            yield return null;
        }
    }
}