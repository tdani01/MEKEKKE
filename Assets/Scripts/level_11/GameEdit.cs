using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static Cinemachine.DocumentationSortingAttribute;

public class GameEdit : MonoBehaviour
{
    public static GameEdit instance;
    private static string indexPath;
    private static string path;
    public int index__;
    public string name__;
    public bool isSaving = false;

    void Awake()
    {
        instance = this;
    }

    void CreateMapFile(string name, int level, string[] content, int index)
    {
        string _content = "";
        foreach (string s in content)
        {
            _content += s + "\n";
        }
        path = Application.persistentDataPath + $"/{name}_level_{level}.txt";
        indexPath = Application.persistentDataPath + "/mapIndex.txt";
        if (!File.Exists(path))
            File.WriteAllText(path, _content);
        else
        {
            File.Delete(path);
            File.WriteAllText(path, _content);
        }

        if (!File.Exists(indexPath))
            File.WriteAllText(indexPath, "0\t/l_11/base.txt");
        else
            File.AppendAllText(indexPath, $"{index}\t/l_11/{name}_level_{level}.txt");
        //TODO ne lehessen duplikálni az index rekordokat
        isSaving = false;
    }

    public string[] LoadMap(int index)
    {
        indexPath = Application.persistentDataPath + "/mapIndex.txt";
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


        List<string> data = new List<string>();
        string temppath = Path.Combine(Application.persistentDataPath, "fasz_level_1.txt");
        if (File.Exists(temppath))
        {
            try
            {
                using (FileStream stream = new FileStream(temppath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (reader.Peek() != -1)
                            data.Add(reader.ReadLine());
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("fasz");
            }
            return data.ToArray();
        }
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && !isSaving)
        {
            isSaving = true;
            Debug.Log("SaveStarted");
            CreateMapFile(name__, 1, GridManager.Instance.GetGridData(), index__);
        }
    }
}