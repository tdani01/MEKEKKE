using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tomlyn;
using Tomlyn.Model;

public class KeyHandler : MonoBehaviour
{
    private static int DefaultKeyCount = 4 * 2; // 2 keybinds / action
    private string filePath;
    
    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "usersettings.toml");
        LoadKeyBinds();
    }

    private Dictionary<string, KeyCode> LoadKeyBinds()
    {
        Dictionary<string, KeyCode> tempKeyBinds = new Dictionary<string, KeyCode>();
        if (Globals.keyBinds.Count == 0 || File.Exists(filePath))
        {
            string tomlContent = File.ReadAllText(filePath);
            TomlTable table = Toml.ToModel(tomlContent);
            foreach (var key in table.Keys)
            {
                tempKeyBinds[key] = (KeyCode)System.Enum.Parse(typeof(KeyCode), table[key].ToString());
            }
        }
        else
        {
            // Defa binds
            tempKeyBinds["Left_0"] = KeyCode.A;
            tempKeyBinds["Left_1"] = KeyCode.LeftArrow;

            tempKeyBinds["Forwards_0"] = KeyCode.W;
            tempKeyBinds["Forwards_1"] = KeyCode.UpArrow;

            tempKeyBinds["Right_0"] = KeyCode.D;
            tempKeyBinds["Right_1"] = KeyCode.RightArrow;

            tempKeyBinds["Down_0"] = KeyCode.S;
            tempKeyBinds["Down_1"] = KeyCode.DownArrow;

            tempKeyBinds["Jump_0"] = KeyCode.Space;
            tempKeyBinds["Jump_1"] = KeyCode.Return;

            tempKeyBinds["Toggle_Walk_0"] = KeyCode.LeftShift;
            tempKeyBinds["Toggle_Walk_1"] = KeyCode.RightShift;

            // .... more

            SaveKeyBinds(tempKeyBinds);
        }
        return tempKeyBinds;
    }

    private bool SaveKeyBinds(Dictionary<string, KeyCode> keys)
    {
        TomlTable table = new TomlTable();
        foreach (var key in keys)
        {
            table[key.Key] = key.Value.ToString();
        }
        string tomlContent = Toml.FromModel(table);
        File.WriteAllText(filePath, tomlContent);
        return File.Exists(filePath);
    }


    public void SetKeyBind(string action, KeyCode key)
    {
        Globals.keyBinds[action] = key;
    }

    public KeyCode GetKeyBind(string action)
    {
        return Globals.keyBinds[action];
    }

    public string GetKeyBindString(string action)
    {
        return Globals.keyBinds[action].ToString();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
}
