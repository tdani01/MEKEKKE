using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedWalls : MonoBehaviour
{
    public float timeToDecay = 5f;

    void Start()
    {
        Destroy(gameObject, timeToDecay);
    }
}
