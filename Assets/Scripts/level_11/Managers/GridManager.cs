using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private float _width, _height;
    [SerializeField] public Tile _baseTile, _wallTile, _borderTile, _enemySpawnTile, _playerSpawnTile, _utilitySpawnTile;
    [SerializeField] private Transform _cam;
    [SerializeField] private Dictionary<Vector2, Tile> _tiles;
    [SerializeField] private List<string> tileData;
    void Start()
    {
        GenerateField();
    }

    void Awake()
    {
        Instance = this;
    }

    public void GenerateField()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x <= _width; x++)
        {
            for (int y = 0; y <= _height; y++)
            {
                var spawnedTile = Instantiate(_baseTile, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.tileType = TileType.normal;
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)(_width/2), (float)(_height/2), -10);

        GameManager.Instance.ChangeState(GameState.SpawnPlayer);
    }

    public Tile GetTileAtPos(Vector2 pos)
    {
        try
        {
            if (_tiles.TryGetValue(pos, out var tile)) 
                return tile;
            return null;
        }
        catch (System.Exception e)
        {

            Debug.Log(e.ToString());
            return null;
        }
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

