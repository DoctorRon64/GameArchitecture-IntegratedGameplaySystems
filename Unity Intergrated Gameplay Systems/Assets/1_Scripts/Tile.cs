using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : IDamagable
{
    public Vector2Int Pos { get; private set; }
    public GameObject GameObjectPrefab { get; private set; }
    public GameObject GameObjectInstance { get; private set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    //Events
    public delegate void TileDied(Vector2Int pos);
    public event TileDied OnDied;

    public Tile(GameObject prefab, Vector2Int _pos, GameObject parent)
    {
        Pos = _pos;
        GameObjectPrefab = prefab;
        GameObjectInstance = GameObject.Instantiate(prefab, new Vector3(_pos.x, -_pos.y, 0), Quaternion.identity);
        GameObjectInstance.transform.SetParent(parent.transform);
    }

    //IDamagable
    public void TakeDamage(int _damageAmount)
    {
        Health -= _damageAmount;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDied(Pos);
    }
}
