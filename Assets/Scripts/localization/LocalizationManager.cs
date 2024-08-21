using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour
{
    public enum Locals
    {
        en,
        hu
    }

    public static LocalizationManager instance;

    public Dictionary<string, string> localizedText;
    public static Locals selectedLocal = Locals.hu;
    public static string filePath = Path.Combine(Application.persistentDataPath, $"Locals/local_{selectedLocal}.txt");
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        LoadLocalText(selectedLocal);
    }

    private void LoadLocalText(Locals local)
    {
        localizedText = new Dictionary<string, string>();
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    if (!localizedText.ContainsKey(parts[0].Trim()))
                    {
                        localizedText[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
        }
        else
            Debug.LogError($"Local file not found: {filePath}");
    }

    /// <summary>
    /// Gets the text for the selected language
    /// </summary>
    /// <param name="key">Key of localized text</param>
    /// <returns>the value of the localized text if it exists. If not then returns 'NOT FOUND' -> in this case please check if you entered the right key or you saved the local file</returns>
    public string GetText(string key)
    {
        if (localizedText.ContainsKey(key))
            return localizedText[key];
        return "NOT FOUND";
    }
}
