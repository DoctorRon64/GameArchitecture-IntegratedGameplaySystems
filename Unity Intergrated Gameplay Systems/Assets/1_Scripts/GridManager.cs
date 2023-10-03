using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridManager : IUpdateable
{
    GridSettings gridSettings;

    private Tile[,] grid;

    private GameObject floor = Resources.Load<GameObject>("Prefabs/Floor");


    /////////////////////////////////////////////////////////////////

    public GridManager(GridSettings _gridSettings)
    {
        gridSettings = _gridSettings;
        grid = new Tile[gridSettings.Size.x, gridSettings.Size.y];

        GeneratePlanet();
    }

    public void OnUpdate()
    {

    }

    public void AddTile(GameObject prefab, Vector2Int pos)
    {
        if (CheckIfIsIngridBounds(pos))
        {
            grid[pos.x, pos.y] = new Tile(prefab, pos);
        }
        else
        {
            Debug.LogError("AddTile: " + pos.x + ", " + pos.y + " Is out of bounds");
        }
    }

    public void RemoveTile(Vector2Int pos)
    {
        if (CheckIfIsIngridBounds(pos))
        {
            grid[pos.x, pos.y] = null;
        }
        else
        {
            Debug.LogError("AddTile: " + pos.x + ", " + pos.y + " Is out of bounds");
        }
    }

    /////////////////////////////////////////////////////////////////

    private void GeneratePlanet()
    {
        int gridMiddlePoint = ((gridSettings.Size.x / 2) + (gridSettings.Size.y / 2)) / 2;
        int radiusBegin = gridMiddlePoint - gridSettings.PlanetRadius;
        int radiusEnd = gridMiddlePoint + gridSettings.PlanetRadius;

        if (gridMiddlePoint - gridSettings.PlanetRadius < 0) { Debug.LogError("Planet radius too big"); }

        for (int y = radiusBegin; y < radiusEnd; y++)
        {
            for (int x = radiusBegin; x < radiusEnd; x++)
            {
                //if (Vector2.Distance(new Vector2(x, y), new Vector2(gridMiddlePoint, gridMiddlePoint)))
                //{
                //    AddTile(floor, new Vector2Int(x, y));
                //}
            }
        }
    }

    private bool CheckIfIsIngridBounds(Vector2Int pos)
    {
        if (pos.y >= 0 && pos.y <= gridSettings.Size.y)
        {
            if (pos.x >= 0 && pos.x <= gridSettings.Size.x)
            {
                return true;
            }
        }
        return false;
    }
}
