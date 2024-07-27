using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tomlyn;

public class SaveContent
{
    public string name { get; set; }
    public ulong Size { get; set; }
    public List<Achievement> completedAchievements { get; set; }
    public int MyProperty { get; set; }
}

public class Save : MonoBehaviour
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
