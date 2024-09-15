using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private GameObject highlight;
    private float x,y;
    
    public TileType tileType;
    public bool isEnemySpawn, isPlayerSpawn, isUtilitySpawn;

    public void Init(bool isOffset)
    {
        _sr.color = isOffset ? _baseColor : _offsetColor;
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.GameState == GameState.GameEdit)
            highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    { 
        if (highlight.activeSelf)
        {
            Tile spawnedTile;
            switch (tileType)
            {
                case TileType.normal:
                    spawnedTile = Instantiate(GridManager.Instance._wallTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Wall {x} {y}";
                    spawnedTile.tileType = TileType.wall;
                    spawnedTile.Init((x % 2 == 0 && y % 2 != 0) ||
                        (x % 2 != 0 && y % 2 == 0));
                    GridManager.Instance.ChangeTileInDict(spawnedTile);
                    Destroy(gameObject);
                    break;

                case TileType.wall:
                    spawnedTile = Instantiate(GridManager.Instance._enemySpawnTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Enemy Spawn {x} {y}";
                    spawnedTile.tileType = TileType.enemySpawn;
                    spawnedTile.Init((x % 2 == 0 && y % 2 != 0) ||
                        (x % 2 != 0 && y % 2 == 0));
                    GridManager.Instance.ChangeTileInDict(spawnedTile);
                    Destroy(gameObject);
                    break;

                case TileType.border:
                    Debug.Log("You can't edit this tile");
                    break;

                case TileType.enemySpawn:
                    spawnedTile = Instantiate(GridManager.Instance._playerSpawnTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Player Spawn {x} {y}";
                    spawnedTile.tileType = TileType.playerSpawn;
                    spawnedTile.Init((x % 2 == 0 && y % 2 != 0) ||
                        (x % 2 != 0 && y % 2 == 0));
                    GridManager.Instance.ChangeTileInDict(spawnedTile);
                    Destroy(gameObject);
                    break;

                case TileType.playerSpawn:
                    spawnedTile = Instantiate(GridManager.Instance._utilitySpawnTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Utility Spawn {x} {y}";
                    spawnedTile.tileType = TileType.utilitySpawn;
                    spawnedTile.Init((x % 2 == 0 && y % 2 != 0) ||
                        (x % 2 != 0 && y % 2 == 0));
                    GridManager.Instance.ChangeTileInDict(spawnedTile);
                    Destroy(gameObject);
                    break;

                case TileType.utilitySpawn:
                    spawnedTile = Instantiate(GridManager.Instance._baseTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.tileType = TileType.normal;
                    spawnedTile.Init((x % 2 == 0 && y % 2 != 0) ||
                        (x % 2 != 0 && y % 2 == 0));
                    GridManager.Instance.ChangeTileInDict(spawnedTile);
                    Destroy(gameObject);
                    break;
            }
            
        }
        else
            Debug.Log("-");
    }
}
public enum TileType
{
    normal,
    border,
    wall,
    spawnedWall,
    enemySpawn,
    playerSpawn,
    utilitySpawn
}
