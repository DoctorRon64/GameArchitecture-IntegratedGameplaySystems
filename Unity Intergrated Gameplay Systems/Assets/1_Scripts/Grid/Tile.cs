using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : IDamagable, IInstantiatable
{
    public Vector2Int Pos 
    {
        get { return pos; }

        set
        {
            Instance.transform.position = new Vector3(value.x, -value.y, 0);
            pos = value;
        }
    }
    private Vector2Int pos;

    //IInstantiateble
    public GameObject Instance { get; set; }

    //IDamagable
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public delegate void TileDied(Vector2Int pos);
    public event TileDied OnDied;

    public Tile(Sprite _sprite, Transform _parent, int _maxHealth)
    {
        //Instance
        Instance = new GameObject();
        Instance.transform.SetParent(_parent);

        SpriteRenderer renderer = Instance.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite;

        BoxCollider2D collider = Instance.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        Instance.layer = LayerMask.NameToLayer("TileLayer");

        MaxHealth = _maxHealth;
        Health = MaxHealth;
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
        OnDied(pos);
    }
}
