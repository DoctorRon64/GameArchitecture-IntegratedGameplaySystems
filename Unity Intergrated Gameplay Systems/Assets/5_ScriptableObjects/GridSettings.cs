using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObject/GridSettings")]
public class GridSettings : ScriptableObject
{
    [Header("Grid Settings")]
    public Vector2Int Size;

    [Header("Planet Settings")]
    public int PlanetRadius;
    public int DirtPercentage;
    public int StonePercentage;
    public int HardStonePercentage;

    [Header("Tile Settings")]
    public TileSettings dirt;
    public TileSettings stone;
    public TileSettings hardStone;
}
