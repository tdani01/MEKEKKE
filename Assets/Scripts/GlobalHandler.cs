using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tomlyn;
using Tomlyn.Model;

public class GlobalHandler : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Story { }

public static class Minigame { }
public static class Globals {
    public static Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();
    public static Dictionary<string, KeyCode> joyStick = new Dictionary<string, KeyCode>();
    public static int Points = 0;
    public static TomlTable userSettings;
    public enum MiniGames
    {
        count
    }
}
