using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridManager
{
    private Tile[,] grid;
    private TileFactory tileFactory;

    //References
    private GameSettings gameSettings;
    private EnemyManager enemyManager;
    private GameManager gameManager;

    public GridManager(GameSettings _gameSettings, EnemyManager _enemyManager, GameManager _gameManager)
    {
        enemyManager = _enemyManager;
        gameSettings = _gameSettings;
        gameManager = _gameManager;

        grid = new Tile[gameSettings.Size.x, gameSettings.Size.y];
        tileFactory = new TileFactory(gameSettings);

        GeneratePlanet();
        GenerateBorders();
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
            grid[_pos.x, _pos.y].OnDied += RemoveTileOnDied;
            grid[_pos.x, _pos.y].OnDied += MaybeSummonEnemy;
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
            grid[_pos.x, _pos.y].OnDied -= RemoveTileOnDied;
            grid[_pos.x, _pos.y].OnDied -= MaybeSummonEnemy;
            grid[_pos.x, _pos.y] = null;
        }
        else
        {
            Debug.LogError("RemoveTile: " + _pos.x + ", " + _pos.y + " Is out of bounds");
        }
    }

    public void RemoveTileOnDied(Vector2Int _pos)
    {
        if (CheckIfIsInGridBounds(_pos))
        {
            gameManager.Score += gameSettings.TileKillScore;
            RemoveTile(_pos);
        }
        else
        {
            Debug.LogError("RemoveTileOnDied: " + _pos.x + ", " + _pos.y + " Is out of bounds");
        }
    }

    private void MaybeSummonEnemy(Vector2Int _pos)
    {
        int randomInt = Random.Range(1, 100);

        if (randomInt <= gameSettings.EnemySpawnChance)
        {
            enemyManager.AddEnemy("Enemy", GetEmptyTileAroundTile(new Vector2Int(_pos.x, _pos.y), 1));
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

    private void GenerateBorders()
    {
        for (int y = 0; y < gameSettings.Size.y; y++)
        {
            for (int x = 0; x < gameSettings.Size.x; x++)
            {
                if (y == 0 || x == 0 || y == gameSettings.Size.y - 1 || x == gameSettings.Size.x - 1)
                {
                    AddTile("Invis", new Vector2Int(x, y));
                }

                if (y == 1 || x == 1 || y == gameSettings.Size.y - 2 || x == gameSettings.Size.x - 2)
                {
                    AddTile("Invis", new Vector2Int(x, y));
                }
            }
        }
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
    }

    private Vector2Int GetEmptyTileAroundTile(Vector2Int target, int radius)
    {
        for (int y = target.y - radius; y < target.y + radius; y++)
        {
            for (int x = target.x - radius; x < target.x + radius; x++)
            {
                if (CheckIfIsInGridBounds(new Vector2Int(x, y)))
                {
                    if (GetTile(new Vector2Int(x, y)) == null)
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }
        }

        Debug.LogError("Couldn't Find Empty Tile Around " + target + " with radius " + radius);
        return new Vector2Int(0, 0);
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