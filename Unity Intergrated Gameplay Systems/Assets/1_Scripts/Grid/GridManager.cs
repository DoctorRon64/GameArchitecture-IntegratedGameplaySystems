using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

[System.Serializable]
public class GridManager : IUpdateable
{
    private GridSettings gs;
    private Tile[,] grid;
    private TileFactory tileFactory;

    public GridManager(GridSettings _gridSettings)
    {
        gs = _gridSettings;
        grid = new Tile[gs.Size.x, gs.Size.y];
        tileFactory = new TileFactory();

        GeneratePlanet();
    }

    public void OnUpdate()
    {

    }

    public void AddTile(string _tileType, Vector2Int _pos)
    {
        if (CheckIfIsInGridBounds(_pos))
        {
            Tile currentTile = grid[_pos.x, _pos.y];

            if (currentTile != null)
            {
                RemoveTile(_pos);
            }

            currentTile = tileFactory.Create(_tileType);
            currentTile.Pos = _pos;
            currentTile.OnDied += RemoveTile;
        }
        else
        {
            Debug.LogError("AddTile: " + _pos.x + ", " + _pos.y + " Is out of bounds");
        }
    }

    public void RemoveTile(Vector2Int _pos)
    {
        if (CheckIfIsInGridBounds(_pos))
        {
            GameObject.Destroy(grid[_pos.x, _pos.y].instance);
            grid[_pos.x, _pos.y].OnDied -= RemoveTile;
            grid[_pos.x, _pos.y] = null;
        }
        else
        {
            Debug.LogError("RemoveTile: " + _pos.x + ", " + _pos.y + " Is out of bounds");
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

        GenerateCircle("Dirt", dirtEndRadius, dirtStartRadius, gridMiddlePoint);
        GenerateCircle("Stone", stoneEndRadius, stoneStartRadius, gridMiddlePoint);
        GenerateCircle("HardStone", hardStoneEndRadius, hardStoneStartRadius, gridMiddlePoint);
    }

    private void GenerateCircle(string tileType, int endRadius, int startRadius, Vector2Int middlePoint)
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
                        AddTile(tileType, new Vector2Int(x, y));
                    }
                }
            }
        }

        //TODO add some randomness
    }

    private bool CheckIfIsInGridBounds(Vector2Int _pos)
    {
        if (_pos.y >= 0 && _pos.y <= gs.Size.y)
        {
            if (_pos.x >= 0 && _pos.x <= gs.Size.x)
            {
                return true;
            }
        }
        return false;
    }
}
