using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public Tile _baseTile, _wallTile, _spawnedWallTile, _borderTile, _enemySpawnTile, _playerSpawnTile, _utilitySpawnTile;
    [SerializeField] public float _width, _height;
    [SerializeField] private Transform _cam;
    [SerializeField] private Dictionary<Vector2, Tile> _tiles;
    [SerializeField] private bool doLoadMap = false;
    public List<GameObject> enemySpawners, utilitySpawners;
    public GameObject playerSpawner;
    public void GridManagerStart()
    {
        enemySpawners = new List<GameObject>();
        utilitySpawners = new List<GameObject>();
        if (doLoadMap)
            LoadMap(GameEdit.instance.LoadMap(1));
        else
            GenerateField();       
    }

    void Awake()
    {
        Instance = this;
    }

    public void LoadMap(string[] mapData)
    {
        Tile _tile;
        float _x = 0;
        float _y = 0;
        TileType _type;
        _tiles = new Dictionary<Vector2, Tile>();
        for (int i = 0; i < mapData.Length; i++)
        {
            string line = mapData[i];
            if (!string.IsNullOrEmpty(line))
            {
                
                _x = int.Parse(line.Split(' ')[0].Trim());
                _y = int.Parse(line.Split(' ')[1].Split('\t')[0].Trim());
                _type = (TileType)Enum.Parse(typeof(TileType), line.Split('\t')[1].Trim()); 

                switch (_type)
                {
                    case TileType.normal:
                        _tile = Instantiate(_baseTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Tile {_x} {_y}";
                        _tile.tileType = TileType.normal;
                        break;

                    case TileType.border:
                        _tile = Instantiate(_borderTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Border {_x} {_y}";
                        _tile.tileType = TileType.border;
                        break;

                    case TileType.wall:
                        _tile = Instantiate(_wallTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Wall {_x} {_y}";
                        _tile.tileType = TileType.wall;
                        break;

                    case TileType.enemySpawn:
                        _tile = Instantiate(_baseTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Enemy Spawn {_x} {_y}";
                        _tile.tileType = TileType.enemySpawn;
                        _tile.isEnemySpawn = true;
                        enemySpawners.Add(_tile.gameObject);

                        break;

                    case TileType.playerSpawn:
                        _tile = Instantiate(_baseTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Player Spawn {_x} {_y}";
                        _tile.tileType = TileType.playerSpawn;
                        _tile.isPlayerSpawn = true;
                        playerSpawner = _tile.gameObject;
                        break;
                    case TileType.utilitySpawn:
                        _tile = Instantiate(_baseTile, new Vector3(_x, _y), Quaternion.identity);
                        _tile.name = $"Utility Spawn {_x} {_y}";
                        _tile.tileType = TileType.utilitySpawn;
                        _tile.isUtilitySpawn = true;
                        utilitySpawners.Add(_tile.gameObject);
                        break;
                    default:
                        return;
                }

                _tile.Init((_x % 2 == 0 && _y % 2 != 0) || (_x % 2 != 0 && _y % 2 == 0));
                _tiles[new Vector2(_x, _y)] = _tile;
            }
        }
        _cam.transform.position = new Vector3((float)(_x / 2), (float)(_y / 2), -10);
        GameManager.Instance.ChangeState(GameState.SpawnPlayer);
    }

    public void GenerateField()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        Tile spawnedTile;
        for (int x = 0; x <= _width; x++)
        {
            for (int y = 0; y <= _height; y++)
            {
                if (x == _width ||  y == _height || x == 0 || y == 0)
                {
                    spawnedTile = Instantiate(_borderTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Border {x} {y}";
                    spawnedTile.tileType = TileType.border;
                }
                else
                {
                    spawnedTile = Instantiate(_baseTile, new Vector3(x,y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";
                    spawnedTile.tileType = TileType.normal;
                }
                spawnedTile.Init((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0));

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
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }

    public void ChangeTileInDict(Tile tile)
    {
        _tiles[new Vector2(tile.transform.position.x, tile.transform.position.y)] = tile;
    }
    public string[] GetGridData()
    {
        string[] mapData = new string[(int)((_width+1) * (_height+1))];
        int i = 0;
        for (int x = 0; x <= _width; x++)
        {
            for (int y = 0; y <= _height; y++)
            {
                Vector2 key = new Vector2(x,y);
                mapData[i] = $"{x} {y}\t{Enum.GetName(typeof(TileType), _tiles[key].tileType)}";
                i++;
            }
        }
        return mapData;
    }
}

