using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Controll : MonoBehaviour
{

    public List<GameObject> Maps;
    private GameObject activeGame;
    public GameObject Victory;

    // Start is called before the first frame update
    void Start()
    {
        MapLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapLoad()
    {
        try
        {
            if (activeGame != null)
            {
                activeGame.SetActive(false);
            }

            int rnd = Random.Range(0, Maps.Count);
            Maps[rnd].SetActive(true);
            activeGame = Maps[rnd];
            Maps.RemoveAt(rnd);
        }
        catch (System.Exception)
        {
            Debug.Log("V�ge");
            Victory.SetActive(true);
        }
    }

    

}
