using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.CompilerServices;

static class KeyFiles
{
    [SerializeField] public static string filepath = Path.Combine(Application.persistentDataPath, "keymap.dat");
}

public class KeyHandler : MonoBehaviour
{
    public static KeyHandler Instance { get; private set; }
    
    private KeyMap loadedKeyMap;
    public enum KeyAction
    {
        m_Forward,
        m_Back,
        m_Left,
        m_Right,
        m_Jump,
        m_Crouch,
        i_Interact,
        i_Back
    }

    public KeyCode[] getKeys(KeyAction action)
    {
        return loadedKeyMap.keymap[action];
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadOrInitializeKeyMap();
    }

    private void LoadOrInitializeKeyMap()
    {
        loadedKeyMap = LoadMap();

        if (loadedKeyMap == null)
        {
            KeyMap keybinds = new KeyMap();
            keybinds.keymap[KeyAction.m_Forward]    = new KeyCode[2] { KeyCode.W,       KeyCode.UpArrow     };
            keybinds.keymap[KeyAction.m_Back]       = new KeyCode[2] { KeyCode.S,       KeyCode.DownArrow   };
            keybinds.keymap[KeyAction.m_Left]       = new KeyCode[2] { KeyCode.A,       KeyCode.LeftArrow   };
            keybinds.keymap[KeyAction.m_Right]      = new KeyCode[2] { KeyCode.D,       KeyCode.RightArrow  };
            keybinds.keymap[KeyAction.m_Jump]       = new KeyCode[2] { KeyCode.Space,   KeyCode.None        };
            keybinds.keymap[KeyAction.i_Interact]   = new KeyCode[2] { KeyCode.E,       KeyCode.None        };
            keybinds.keymap[KeyAction.i_Back]       = new KeyCode[2] { KeyCode.Escape,  KeyCode.None        };

            SaveMap(keybinds);

            Debug.Log("Key map initialized!");
        }
        else
            Debug.Log("Loaded existing key map");
    }

    [Serializable]
    public class KeyMap
    {
        public Dictionary<KeyAction, KeyCode[]> keymap = new Dictionary<KeyAction, KeyCode[]>();
    }

    public bool GetKeyDown(KeyAction key)   { return Input.GetKeyDown(loadedKeyMap.keymap[key][0])  || Input.GetKeyDown(loadedKeyMap.keymap[key][1]);   }
    public bool GetKeyUp(KeyAction key)     { return Input.GetKeyUp(loadedKeyMap.keymap[key][0])    || Input.GetKeyUp(loadedKeyMap.keymap[key][1]);     }

    public KeyMap LoadMap()
    {
        if (!File.Exists(KeyFiles.filepath))
        {
            Debug.LogWarning("Keymap file not found"); // only if start has not run
            return null;
        }

        try
        {
            using (FileStream fileStream = new FileStream(KeyFiles.filepath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                KeyMap loadedKeyMap = (KeyMap)formatter.Deserialize(fileStream);
                Debug.Log("Keymap loaded successfully.");
                return loadedKeyMap;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load keymap: {ex.Message}");
            return null;
        }
    }

    public void SaveMap(KeyMap keyMap)
    {
        if (!string.IsNullOrEmpty(KeyFiles.filepath)) { return; } // throw error
        try
        {
            using (FileStream fileStream = new FileStream(KeyFiles.filepath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, keyMap);
                Debug.Log("Keymap saved..");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save keymap: {ex.Message}");
        }
    }

    public void ModifyMap(KeyAction action, KeyCode newKey1 = KeyCode.None, KeyCode newKey2 = KeyCode.None)
    {
        if (loadedKeyMap != null)
        {
            if (loadedKeyMap.keymap.ContainsKey(action))
            {
                if (newKey1 == KeyCode.None)
                    loadedKeyMap.keymap[action] = new KeyCode[2] { loadedKeyMap.keymap[action][0], newKey2 };
                else if (newKey2 == KeyCode.None)
                    loadedKeyMap.keymap[action] = new KeyCode[2] { newKey1, loadedKeyMap.keymap[action][1] };
                else
                {
                    Debug.LogWarning($"Keymodify got no values");
                    return;
                }                
                SaveMap(loadedKeyMap);
                Debug.Log($"Action: {action} successfully modified to: key_1: {newKey1} || key_2: {newKey2}");
            }
            else
                Debug.LogWarning($"Action {action} is not found in key mappings");
        }
        else
            Debug.Log("Key map is not initialized.");
    }
}
