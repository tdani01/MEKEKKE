using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
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
            Debug.Log(rand);
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
