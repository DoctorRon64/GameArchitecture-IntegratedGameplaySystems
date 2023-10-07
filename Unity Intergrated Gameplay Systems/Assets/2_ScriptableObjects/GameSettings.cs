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
    public TileSettings Dirt;
    public TileSettings Stone;
    public TileSettings HardStone;

    [Header("Bullet Settings")]
    public int BulletPoolSize;
    public int BulletDamage;
    public float bulletSpeed;
    public float ShootCooldown;

    [Header("Enemy Settings")]
    public int EnemyAttackRange;
    public float EnemySpeed;
    public int EnemySpawnChance;
}
