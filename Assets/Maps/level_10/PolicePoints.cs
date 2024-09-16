using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePoints : MonoBehaviour
{

    public static int Police_Point = 0;
    public static bool[] Minigames = { false, false, false, false };
    public static void AddPoint(int gameID)
    {
        if (!Minigames[gameID])
        {
            Police_Point++;
            Minigames[gameID] = true;
        }
    }

    public static void Clear()
    {
        Police_Point = 0;
        Minigames = new bool[] { false, false, false, false };
    }

}
