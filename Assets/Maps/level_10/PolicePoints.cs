using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PolicePoints : MonoBehaviour
{

    public static int Police_Point = 3;
    public static bool[] Minigames = { false, false, false, false };
    public static void AddPoint(int gameID)
    {
        if (!Minigames[gameID])
        {
            Police_Point++;
            Minigames[gameID] = true;
        }
    }


}
