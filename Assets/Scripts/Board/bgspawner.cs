using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgspawner : MonoBehaviour
{
    public GameObject[] bgobjects;
    private float spawnKoztiIdo;
    public float kezdesSpawnIdo;
    public float csokkenesIdo;
    public float minIdo;

    // Update is called once per frame
    void Update()
    {

        if (spawnKoztiIdo <= 0)
        {
            //rnd = new Random();
            int rand = Random.Range(0, bgobjects.Length);
            Instantiate(bgobjects[rand], transform.position, Quaternion.identity);

            spawnKoztiIdo = kezdesSpawnIdo;
            if (kezdesSpawnIdo > minIdo)
            {
                kezdesSpawnIdo -= csokkenesIdo;
            }
        }
        else
        {
            spawnKoztiIdo -= Time.deltaTime;
        }
        
    }
}
