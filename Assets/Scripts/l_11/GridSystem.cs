using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridCell
{
    public GameObject storedObject;  // The object placed in this grid cell
    public Vector2Int gridPosition;  // The position of this cell in the grid

    public GridCell(Vector2Int position)
    {
        gridPosition = position;
        storedObject = null;  // Initially, the cell is empty
    }

    public void PlaceObject(GameObject obj)
    {
        storedObject = obj;
    }

    public void RemoveObject()
    {
        storedObject = null;
    }

    public bool IsEmpty()
    {
        return storedObject == null;
    }
}
public class GridSystem : MonoBehaviour
{
    public int gridWidth = 10;    // The width of the grid
    public int gridHeight = 10;   // The height of the grid
    public float cellSize = 1f;   // Size of each grid cell

    public GridCell[,] gridCells;  // The grid of cells

    private void Start()
    {
        InitializeGrid();
    }

    // Initializes the grid
    public void InitializeGrid()
    {
        gridCells = new GridCell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridCells[x, y] = new GridCell(new Vector2Int(x, y));
            }
        }

        Debug.Log("Grid Initialized!");
    }

    // Convert world position to grid coordinates
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    // Convert grid coordinates to world position (center of the cell)
    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x * cellSize + cellSize / 2f;
        float y = gridPosition.y * cellSize + cellSize / 2f;
        return new Vector3(x, y, 0f);
    }

    // Place an object in the grid
    public void PlaceObjectInGrid(GameObject obj, Vector2Int gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < gridWidth && gridPosition.y >= 0 && gridPosition.y < gridHeight)
        {
            GridCell cell = gridCells[gridPosition.x, gridPosition.y];
            if (cell.IsEmpty())
            {
                cell.PlaceObject(obj);
                obj.transform.position = GetWorldPosition(gridPosition);  // Move object to the cell's position
                Debug.Log("Object placed in grid at " + gridPosition);
            }
            else
            {
                Debug.LogWarning("Grid cell is not empty!");
            }
        }
        else
        {
            Debug.LogWarning("Grid position out of bounds!");
        }
    }

    // Remove an object from the grid
    public void RemoveObjectFromGrid(Vector2Int gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < gridWidth && gridPosition.y >= 0 && gridPosition.y < gridHeight)
        {
            GridCell cell = gridCells[gridPosition.x, gridPosition.y];
            if (!cell.IsEmpty())
            {
                GameObject obj = cell.storedObject;
                cell.RemoveObject();
                Debug.Log("Object removed from grid at " + gridPosition);
            }
        }
    }
}