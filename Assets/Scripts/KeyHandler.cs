using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tomlyn;
using Tomlyn.Model;

public class KeyHandler : MonoBehaviour
{
    private Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();
    private string filePath;
    
    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "usersettings.toml");
        LoadKeyBinds();
    }

    private void LoadKeyBinds()
    {
        if (File.Exists(filePath))
        {
            string tomlContent = File.ReadAllText(filePath);
            TomlTable table = Toml.ToModel(tomlContent);
            foreach (var key in table.Keys)
            {
                keyBinds[key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), table[key].ToString());
            }
        }
        else
        {
            // Defa binds
            keyBinds["Left_0"] = KeyCode.A;
            keyBinds["Left_1"] = KeyCode.LeftArrow;

            keyBinds["Forwards_0"] = KeyCode.W;
            keyBinds["Forwards_1"] = KeyCode.UpArrow;

            keyBinds["Right_0"] = KeyCode.D;
            keyBinds["Right_1"] = KeyCode.RightArrow;

            keyBinds["Down_0"] = KeyCode.S;
            keyBinds["Down_1"] = KeyCode.DownArrow;

            // .... more

            SaveKeyBinds();
        }
    }

    private void SaveKeyBinds()
    {
        TomlTable table = new TomlTable();
        foreach (var key in keyBinds)
        {
            table[key.Key] = key.Value.ToString();
        }
        string tomlContent = Toml.FromModel(table);
        File.WriteAllText(filePath, tomlContent);
    }
    /// <summary>
    /// élkél
    /// </summary>
    public void valami() { }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
