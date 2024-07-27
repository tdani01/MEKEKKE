using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Tomlyn;
using Tomlyn.Model;
using UnityEngine;
using System.IO;


[Serializable]
public class Achievement
{    
    public string id;
    public string title;
    public string description;
    private const string defaultPath = "";
    public string? iconPath;
    public bool isUnlocked = false;
    public UInt32 score;
    /// <summary>
    /// Achievement constructor
    /// </summary>
    /// <param name="id">ID of Achievement</param>
    /// <param name="title">Name (title) that shown on popup</param>
    /// <param name="description">Description that shown on popup</param>
    /// <param name="iconPath">Icon Path for icon on popup .. Can be null, in this case, it will go for default path</param>
    /// <param name="score">Score for entry, these will make the final score</param>
    public Achievement(string id, string title, string description, string iconPath, UInt32 score)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.iconPath = string.IsNullOrEmpty(iconPath) ? defaultPath : iconPath;
        this.score = score;
    }
    /// <summary>
    /// Unlocks an entry
    /// </summary>
    public void Unlock() { isUnlocked = true; }
    /// <summary>
    /// Locks an entry
    /// </summary>
    public void Lock() { isUnlocked = false; }
}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance { get; private set; }
    private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    private Dictionary<int, int> partCounters = new Dictionary<int, int>();
    private string configFilePath = "achievements_cfg.toml";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private string GenerateID(int part)
    {
        if (!partCounters.ContainsKey(part))
        {
            partCounters[part] = 0;
        }
        partCounters[part]++;
        return $"A{part}-{partCounters[part]:D3}";
    }

    /// <summary>
    /// Creates a new entry if id is not in the list already
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="iconPath"></param>
    /// <param name="score"></param>
    public void AddAchievement(int part, string title, string description, string iconPath, UInt32 score)
    {
        string id = GenerateID(part);
        if (!achievements.ContainsKey(id))
        {
            Achievement newAchievement = new Achievement(id, title, description, iconPath, score);
            achievements.Add(id, newAchievement);
            SaveAchievemntsToConfig();
        }
    }

    public void UnlockAchievement(string id)
    {
        if (achievements.ContainsKey(id) && !achievements[id].isUnlocked)
        {
            achievements[id].Unlock();
            // display
            UpdateCompletedAchievementsInSave(id);
        }
    }

    private void SaveAchievemntsToConfig()
    {
        var toml = new TomlTable();
        var achievementsList = new TomlTableArray();

        foreach (var entry in achievements.Values)
        {
            var achievementEntry = new TomlTable
            {
                ["id"] = entry.id,
                ["title"] = entry.title,
                ["description"] = entry.description,
                ["iconPath"] = entry.iconPath,
                ["score"] = entry.score,
                ["isUnlocked"] = entry.isUnlocked
            };
            achievementsList.Add(achievementEntry);
        }

        toml["Achievements"] = achievementsList;
        File.WriteAllText(configFilePath, Toml.FromModel(toml));
    }

    private void LoadAchievementsFromCfg()
    {
        if (!File.Exists(configFilePath)) return;

        var toml = Toml.ToModel(File.ReadAllText(configFilePath));
        var achievementsList = (TomlTableArray)toml["Achievements"];

        foreach (var entry in achievementsList)
        {
            var id = entry["id"].ToString();
            var title = entry["title"].ToString();  
            var description = entry["description"].ToString();
            var iconPath = entry["iconPath"].ToString();
            var score = Convert.ToUInt32(entry["score"]);
            var isUnlocked = (bool)entry["isUnlocked"];

            var achievement = new Achievement(id, title, description, iconPath, score) { isUnlocked = isUnlocked };
        }
    }

    public Achievement? GetAchievement(string id) => achievements.ContainsKey(id) ? achievements[id] : null;

    private void UpdateCompletedAchievementsInSave(string id)
    {
        TomlTable toml;
        if (File.Exists(Globals.saveFilePath))
            toml = Toml.ToModel(File.ReadAllText(Globals.saveFilePath));
        else
            toml = new TomlTable();

        if (!toml.ContainsKey("CompletedAchievements"))
            toml["CompletedAchievements"] = new TomlArray();

        var completedAchievements = (TomlArray)toml["CompletedAchievemnts"];
        if (!completedAchievements.Contains(id))
            completedAchievements.Add(id);

        File.WriteAllText(Globals.saveFilePath, Toml.FromModel(toml));
    }
}
