using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Tomlyn.Syntax;
using System.Linq;

[CustomEditor(typeof(TextLocalizer))]
public class TextLocalizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TextLocalizer textLocalizer = (TextLocalizer)target;
        if (LocalizationManager.instance != null && LocalizationManager.instance.localizedText != null)
        {
            Dictionary<string, string> localtext = LocalizationManager.instance.localizedText;
            string[] values = new string[localtext.Values.Count];
            localtext.Values.CopyTo(values, 0);

            int selectedIndex = Mathf.Max(Array.IndexOf(values,LocalizationManager.instance.GetText(textLocalizer.key)), 0);
            selectedIndex = EditorGUILayout.Popup("Select Text", selectedIndex, values);

            textLocalizer.key = localtext.FirstOrDefault(x => x.Value == values[selectedIndex]).Key;
        }
        else
            EditorGUILayout.HelpBox("No LocalizationManager or localized texts available.", MessageType.Warning);
    }
}
