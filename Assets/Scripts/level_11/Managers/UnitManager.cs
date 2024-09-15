using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public GameObject player, playerPrefab;
    public int utilityPerSpawn, enemyPerSpawn;
    public List<GameObject> enemies, utilities, enemyPrefabs, utilityPrefabs;

    private List<GameObject> availableEnemySpawners, availableUtilitySpawners;
    void Awake()
    {
        Instance = this;
    }
    public void InitUnitManager()
    {
        enemies = new List<GameObject>();
        utilities = new List<GameObject>();
        availableEnemySpawners = GridManager.Instance.enemySpawners;
        availableUtilitySpawners = GridManager.Instance.utilitySpawners;
        if (enemyPerSpawn > GridManager.Instance.enemySpawners.Count)
            enemyPerSpawn = GridManager.Instance.enemySpawners.Count;
        if (utilityPerSpawn > GridManager.Instance.utilitySpawners.Count)
            utilityPerSpawn = GridManager.Instance.utilitySpawners.Count;
    }

    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(GridManager.Instance.playerSpawner.transform.position.x, GridManager.Instance.playerSpawner.transform.position.y), Quaternion.identity);
        player.transform.parent = null;
        GameManager.Instance.ChangeState(GameState.SpawnUtilities);
    }

    public void SpawnEnemies() 
    {
        for (int i = 0; i < enemyPerSpawn; i++)
        {
            int spawn = UnityEngine.Random.Range(0, availableEnemySpawners.Count);
            enemies.Add(Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, 2)], new Vector3(availableEnemySpawners[spawn].transform.position.x, availableEnemySpawners[spawn].transform.position.y), Quaternion.identity));
            availableEnemySpawners.Remove(availableEnemySpawners[spawn]);
        }       
        availableEnemySpawners.Clear();
        availableEnemySpawners = GridManager.Instance.enemySpawners;
        GameManager.Instance.ChangeState(GameState.GameStart);
    }

    public void SpawnUtilites()
    {
        for (int i = 0; i < utilityPerSpawn; i++)
        {
            int spawn = UnityEngine.Random.Range(0, availableUtilitySpawners.Count);
            utilities.Add(Instantiate(utilityPrefabs[UnityEngine.Random.Range(0, 3)], new Vector3(availableUtilitySpawners[spawn].transform.position.x, availableUtilitySpawners[spawn].transform.position.y), Quaternion.identity));
            availableUtilitySpawners.Remove(availableUtilitySpawners[spawn]);
        }
        availableUtilitySpawners.Clear();
        availableUtilitySpawners = GridManager.Instance.utilitySpawners;
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }
}
