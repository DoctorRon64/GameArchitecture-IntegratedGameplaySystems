using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsInScene", menuName = "ScriptableObject/ObjectsInScene")]
public class ObjectsInScene : ScriptableObject
{
    [Header("Player")]
    public PlayerData player;
}
