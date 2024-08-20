using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hints : MonoBehaviour
{
    static public List<Hint> hints = new List<Hint>();

    public Hint addHint()
    {
        return null;
    }

    public bool showHint(string key)
    {
        return false;
    }

    public bool hideHint()
    {
        return false;
    }

    public Hint getHint(string key)
    {
        return null;
    }
}

public class Hint
{
    public string key { get; set; }
    public string desc { get; set; }
    public int mapIndex { get; set; }

    private int _sizeX, _sizeY;


    public Hint(string _key, string _desc, int _index)
    {
        this.key = _key;
        this.desc = _desc;
        this.mapIndex = _index;
    }
}
