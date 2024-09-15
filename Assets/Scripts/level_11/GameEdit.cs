using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameEdit : MonoBehaviour
{
    public static GameEdit instance;
    private static string indexPath;
    private static string path;

    void Awake()
    {
        instance = this;
    }
    void CreateMapFile(string name, int level, string content, int index)
    {
        path = Application.dataPath + $"/l_11/level_{level}/{name}.txt";
        indexPath = Application.dataPath + "mapIndex.txt";
        if (!File.Exists(path))
            File.WriteAllText(path,content);

        if (!File.Exists(indexPath))
            File.WriteAllText(indexPath, "0\t/l_11/level_0/base.txt");
        else
            File.AppendAllText(indexPath, $"{index}\t/l_11/level_{level}/{name}.txt");
    }

    string[] LoadMap(int index)
    {
        string[] lines = File.ReadAllLines(indexPath);
        string mapPath = "";
        foreach (string line in lines)
        {
            string[] parts = line.Split('\t');

            if (parts.Length > 0 && int.TryParse(parts[0], out int number) && number == index)
            {
                mapPath = parts[1];
                break;
            }            
        }

        if (File.Exists(mapPath))
        {
            string[] mapData = File.ReadAllLines(mapPath);
            return mapData;
        }
        else return null;
    }

    void SaveMap()
    {

    }

    string FormatLine(int x, int y, TileType type)
    {
        return $"{x} {y}\t{nameof(type)}";
    }
}