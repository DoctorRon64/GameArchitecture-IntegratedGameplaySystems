using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : IPoolable, IInstantiatable, ICollidable, IUpdateable
{
    public bool Active { get; set; }
    public GameObject Instance { get; set; }
    public Collider2D collider { get; set; }
    public Action<Collider2D> OnCollision { get; set; }

    public Bullet(GameObject prefab, Transform _parent)
    {
        //Instance
        Instance = GameObject.Instantiate(prefab);

        //Parent
        Instance.transform.SetParent(_parent);

        //collider
        collider = Instance.GetComponent<Collider2D>();
    }

    public void DisablePoolabe()
    {
        Instance.SetActive(false);
    }

    public void EnablePoolabe()
    {
        Instance.SetActive(true);
    }

    public void FireBullet()
    {

    }

    public void CheckCollisions()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f);

        foreach (Collider2D otherCollider in colliders)
        {
            if (otherCollider != collider)
            {
                OnCollision?.Invoke(otherCollider);
            }
        }
    }

    public void OnUpdate()
    {
        CheckCollisions();
    }
}
