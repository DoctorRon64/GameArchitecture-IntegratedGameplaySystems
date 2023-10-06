using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.tvOS;

[System.Serializable]
public class GridManager
{
    private Tile[,] grid;
    private TileFactory tileFactory;

    //References
    private GameSettings gameSettings;

    public GridManager(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;
        grid = new Tile[gameSettings.Size.x, gameSettings.Size.y];
        tileFactory = new TileFactory(gameSettings);

        GeneratePlanet();
    }

    public Tile GetTile(Vector2Int _pos)
    {
        if (CheckIfIsInGridBounds(_pos))
        {
            return grid[_pos.x, _pos.y];
        }

        Debug.LogError("GetTile: " + _pos.x + ", " + _pos.y + " Is Not In Grid Bounds");
        return null;
    }

    public void AddTile(string _tileType, Vector2Int _pos)
    {
        if (CheckIfIsInGridBounds(_pos))
        {
            if (grid[_pos.x, _pos.y] != null)
            {
                RemoveTile(_pos);
            }

            //Instance
            grid[_pos.x, _pos.y] = tileFactory.Create(_tileType);
            grid[_pos.x, _pos.y].Pos = _pos;
            grid[_pos.x, _pos.y].OnDied += RemoveTile;
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
            GameObject.Destroy(grid[_pos.x, _pos.y].Instance);
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
        Vector2Int gridMiddlePoint = new Vector2Int(gameSettings.Size.x / 2, gameSettings.Size.y / 2);

        int dirtEndRadius = gameSettings.PlanetRadius;
        int dirtStartRadius = dirtEndRadius - (gameSettings.PlanetRadius * gameSettings.DirtPercentage / 100);

        int stoneEndRadius = dirtStartRadius;
        int stoneStartRadius = stoneEndRadius - (gameSettings.PlanetRadius * gameSettings.StonePercentage / 100);

        int hardStoneEndRadius = stoneStartRadius;
        int hardStoneStartRadius = hardStoneEndRadius - (gameSettings.PlanetRadius * gameSettings.HardStonePercentage / 100);

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
        if (_pos.y >= 0 && _pos.y <= gameSettings.Size.y)
        {
            if (_pos.x >= 0 && _pos.x <= gameSettings.Size.x)
            {
                return true;
            }
        }
        return false;
    }
}