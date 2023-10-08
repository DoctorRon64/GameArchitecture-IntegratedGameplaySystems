using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : IDamagable, IInstantiatable
{
    public Vector2Int Pos 
    {
        get => pos;

        set
        {
            Instance.transform.position = new Vector3(value.x, -value.y, 0);
            pos = value;
        }
    }
    private Vector2Int pos;

    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }

    //Events
    public delegate void TileDied(Vector2Int pos);
    public event TileDied OnDied;

    public Tile(GameObject _prefab, Transform _parent, int _maxHealth)
    {
        MaxHealth = _maxHealth;
        Health = MaxHealth;

        Instantiate(_prefab, _parent);

        Instance.layer = LayerMask.NameToLayer("TileLayer");
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    public void TakeDamage(int _damageAmount)
    {
        Health -= _damageAmount;

        if (Health <= 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        OnDied(pos);
    }
}
