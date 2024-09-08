using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    GameObject enemyPrefab;

    public void Start()
    {
        Instantiate(enemyPrefab, gameObject.transform);
    }
}
