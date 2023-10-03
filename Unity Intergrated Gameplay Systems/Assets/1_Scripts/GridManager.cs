using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

[System.Serializable]
public class GridManager : IUpdateable
{
    private GridSettings gs;
    private GameObject parentGameObject;

    private Tile[,] grid;

    private GameObject dirtPrefab = Resources.Load<GameObject>("Prefabs/dirt");
    private GameObject stonePrefab = Resources.Load<GameObject>("Prefabs/stone");
    private GameObject hardStonePrefab = Resources.Load<GameObject>("Prefabs/hardStone");


    public GridManager(GridSettings _gridSettings)
    {
        gs = _gridSettings;
        grid = new Tile[gs.Size.x, gs.Size.y];

        //Tile Parent
        parentGameObject = new GameObject();
        parentGameObject.name = "Tiles";

        GeneratePlanet();
    }

    public void OnUpdate()
    {

    }

    public void AddTile(GameObject prefab, Vector2Int pos)
    {
        if (CheckIfIsInGridBounds(pos))
        {
            Tile currentTile = grid[pos.x, pos.y];

            if (currentTile != null)
            {
                RemoveTile(pos);
            }

            currentTile = new Tile(prefab, pos, parentGameObject);
            currentTile.OnDied += RemoveTile;

        }
        else
        {
            Debug.LogError("AddTile: " + pos.x + ", " + pos.y + " Is out of bounds");
        }
    }

    public void RemoveTile(Vector2Int pos)
    {
        if (CheckIfIsInGridBounds(pos))
        {
            GameObject.Destroy(grid[pos.x, pos.y].GameObjectInstance);
            grid[pos.x, pos.y].OnDied -= RemoveTile;
            grid[pos.x, pos.y] = null;
        }
        else
        {
            Debug.LogError("RemoveTile: " + pos.x + ", " + pos.y + " Is out of bounds");
        }
    }

    private void GeneratePlanet()
    {
        Vector2Int gridMiddlePoint = new Vector2Int(gs.Size.x / 2, gs.Size.y / 2);

        int dirtEndRadius = gs.PlanetRadius;
        int dirtStartRadius = dirtEndRadius - (gs.PlanetRadius * gs.DirtPercentage / 100);

        int stoneEndRadius = dirtStartRadius;
        int stoneStartRadius = stoneEndRadius - (gs.PlanetRadius * gs.StonePercentage / 100);

        int hardStoneEndRadius = stoneStartRadius;
        int hardStoneStartRadius = hardStoneEndRadius - (gs.PlanetRadius * gs.HardStonePercentage / 100);

        GenerateCircle(dirtPrefab, dirtEndRadius, dirtStartRadius, gridMiddlePoint);
        GenerateCircle(stonePrefab, stoneEndRadius, stoneStartRadius, gridMiddlePoint);
        GenerateCircle(hardStonePrefab, hardStoneEndRadius, hardStoneStartRadius, gridMiddlePoint);
    }

    private void GenerateCircle(GameObject prefab, int endRadius, int startRadius, Vector2Int middlePoint)
    {
        //StartRadius is where the circle starts generating from the inside out
        //EndRadius is where the circlestops generation from inside out

        //Check if circle fits in the grid
        if (!CheckIfIsInGridBounds(new Vector2Int(middlePoint.x - endRadius, middlePoint.y - endRadius)) ||
            !CheckIfIsInGridBounds(new Vector2Int(middlePoint.x + endRadius, middlePoint.y + endRadius))) { Debug.LogError("Planet radius too big"); }
        else
        {
            for (int y = middlePoint.y - endRadius; y < middlePoint.y + endRadius; y++)
            {
                for (int x = middlePoint.x -endRadius; x < middlePoint.x + endRadius; x++)
                {
                    if (Vector2.Distance(new Vector2(x, y), new Vector2(middlePoint.x, middlePoint.y)) <= endRadius &&
                        Vector2.Distance(new Vector2(x, y), new Vector2(middlePoint.x, middlePoint.y)) >= startRadius)
                    {
                        AddTile(prefab, new Vector2Int(x, y));
                    }
                }
            }
        }

        //TODO add some randomness
    }

    private bool CheckIfIsInGridBounds(Vector2Int pos)
    {
        if (pos.y >= 0 && pos.y <= gs.Size.y)
        {
            if (pos.x >= 0 && pos.x <= gs.Size.x)
            {
                return true;
            }
        }
        return false;
    }
}
