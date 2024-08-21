using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class TextLocalizerEditor : EditorWindow
{
    private TextLocalizer textLocalizer;
    private string[] keys;

    [MenuItem("Window/Text Localizer")]
    public static void ShowWindow()
    {
        GetWindow<TextLocalizerEditor>("Text Localizer");
    }

    void LoadLocalTexts()
    {
        if (LocalizationManager.instance == null)
        {
            GameObject localizationManagerGO = GameObject.Find("LocalizationManager");
            if (localizationManagerGO != null)
            {
                LocalizationManager.instance = localizationManagerGO.GetComponent<LocalizationManager>();
                if (LocalizationManager.instance != null)
                {
                    LocalizationManager.instance.LoadLocalText(LocalizationManager.Locals.hu);
                    keys = LocalizationManager.instance.localizedText.Keys.ToArray();
                }
            }
        }
    }

    void OnGUI()
    {
        textLocalizer = Selection.activeGameObject?.GetComponent<TextLocalizer>();
        if (textLocalizer == null)
        {
            EditorGUILayout.HelpBox("Select a GameObject with Textlocalizer component", MessageType.Info);
            return;
        }

        LoadLocalTexts();

        if (keys == null || keys.Length == 0)
        {
            EditorGUILayout.HelpBox("No localized texts available.", MessageType.Warning);
            return;
        }

        if (LocalizationManager.instance == null)
        {
            EditorGUILayout.HelpBox("Localizationmanager is null",MessageType.Warning);
            return;
        }

        if ( LocalizationManager.instance.localizedText == null)
        {
            EditorGUILayout.HelpBox("localized text is not available", MessageType.Warning);
            return;
        }
        EditorGUILayout.HelpBox(LocalizationManager.filePath,MessageType.Warning);
        //keys = new string[LocalizationManager.instance.localizedText.Keys.Count];
        //LocalizationManager.instance.localizedText.Keys.CopyTo(keys, 0);
        int selectedIndex = Mathf.Max(Array.IndexOf(keys, textLocalizer.key),0);

        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUILayout.Popup("Select Key", selectedIndex, keys);
        if (EditorGUI.EndChangeCheck())
        {
            textLocalizer.key = keys[selectedIndex];
            EditorUtility.SetDirty(textLocalizer);
        }
    }
}
