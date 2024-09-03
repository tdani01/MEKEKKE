using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ln_spawn : MonoBehaviour
{
    public GameObject[] lines;
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
            int rand = Random.Range(0, lines.Length);
            Instantiate(lines[rand], transform.position, Quaternion.identity);
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
