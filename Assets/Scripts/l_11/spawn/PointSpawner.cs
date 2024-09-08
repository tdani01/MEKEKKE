using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public GameObject pointPrefab;
    private void Start()
    {
        Instantiate(pointPrefab, gameObject.transform);
    }
}
