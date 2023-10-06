using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObject/GameSettings")]
public class GameSettings : ScriptableObject
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

    [Header("Bullet Settings")]
    public int bulletPoolSize;
    public int bulletDamage;
}
