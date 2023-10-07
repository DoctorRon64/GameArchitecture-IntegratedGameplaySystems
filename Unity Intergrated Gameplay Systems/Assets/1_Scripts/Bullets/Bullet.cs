using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable, IInstantiatable, ICollidable<Bullet>, IUpdateable
{
    public bool Active { get; set; }
    public GameObject Instance { get; set; }
    public Collider2D collider { get; set; }
    public Action<Collider2D, Bullet> OnCollision { get; set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private GameSettings gameSettings;

    public Bullet(GameObject _prefab, Transform _parent, GameSettings _gameSettings)
    {
        Instantiate(_prefab, _parent);

        //collider
        Rigidbody = Instance.GetComponent<Rigidbody2D>();
        collider = Instance.GetComponent<Collider2D>();

        gameSettings = _gameSettings;
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    public void DisablePoolabe()
    {
        Instance.SetActive(false);
    }

    public void EnablePoolabe()
    {
        Instance.SetActive(true);
    }

    public void FireBullet(Vector2 _direction)
    {
        Rigidbody.AddForce(_direction * gameSettings.bulletSpeed, ForceMode2D.Impulse);
    }

    public void CheckCollisions()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f);

        foreach (Collider2D otherCollider in colliders)
        {
            if (otherCollider != collider)
            {
                OnCollision?.Invoke(otherCollider, this);
            }
        }
    }

    public void OnUpdate()
    {
        CheckCollisions();
    }

    public void SetPosition(Vector2 _pos)
    {
        Instance.transform.position = _pos;
    }
}
