using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tomlyn;
using Tomlyn.Model;
using UnityEngine.SocialPlatforms.Impl;

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
    public enum MiniGames
    {
        count
    }
    public static string SaveName = "Unnamed";
    public static int SaveCount = 0;
    public static int KeepSaveCount = 3;
    public static Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();
    public static Dictionary<string, KeyCode> joyStick = new Dictionary<string, KeyCode>();
    public static List<MiniGames> completedMinigames = new List<MiniGames>();
    public static int Points = 0;
    public static TomlTable userSettings;
    public static string saveFilePath = $"{SaveName}-{SaveCount}.toml";
}
