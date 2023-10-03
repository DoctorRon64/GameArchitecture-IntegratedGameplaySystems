using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObject/GridSettings")]
public class GridSettings : ScriptableObject
{
    [Header("Grid Settings")]
    public Vector2Int Size;
    public int PlanetRadius;
}
