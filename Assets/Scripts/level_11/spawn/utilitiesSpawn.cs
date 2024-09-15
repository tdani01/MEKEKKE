using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilitiesSpawn : MonoBehaviour
{
    public GameObject[] utilities;

    private void Start()
    {
        Instantiate(utilities[Random.Range(0, utilities.Length)], gameObject.transform);
    }
}
