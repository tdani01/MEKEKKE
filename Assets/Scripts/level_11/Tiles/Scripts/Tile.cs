using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private GameObject highlight;
    public TileType tileType;
    private bool isInside = false;

    public void Init(bool isOffset)
    {
        _sr.color = isOffset ? _baseColor : _offsetColor;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        isInside = true;
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        isInside = false;
    }

    private void OnMouseDown()
    {
        if (highlight.activeSelf)
        {
            Tile spawnedTile;
            switch (tileType)
            {
                case TileType.normal:
                    Debug.Log("+");
                    spawnedTile = Instantiate(GridManager.Instance._wallTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Wall {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.wall;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                case TileType.wall:
                    spawnedTile = Instantiate(GridManager.Instance._borderTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Border {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.border;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                case TileType.border:
                    spawnedTile = Instantiate(GridManager.Instance._enemySpawnTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Enemy Spawn {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.enemySpawn;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                case TileType.enemySpawn:
                    spawnedTile = Instantiate(GridManager.Instance._playerSpawnTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Player Spawn {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.playerSpawn;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                case TileType.playerSpawn:
                    spawnedTile = Instantiate(GridManager.Instance._utilitySpawnTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Utility Spawn {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.utilitySpawn;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                case TileType.utilitySpawn:
                    spawnedTile = Instantiate(GridManager.Instance._baseTile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                    spawnedTile.name = $"Tile {gameObject.transform.position.x} {gameObject.transform.position.y}";
                    spawnedTile.tileType = TileType.normal;
                    spawnedTile.Init((gameObject.transform.position.x % 2 == 0 && gameObject.transform.position.y % 2 != 0) ||
                        (gameObject.transform.position.x % 2 != 0 && gameObject.transform.position.y % 2 == 0));
                    break;

                default:
                    return;
            }
            Destroy(gameObject);
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
