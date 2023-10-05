using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IPoolable, IUpdateable
{
    private GameObject thisGameObject;
    public bool Active { get; set; }

    public delegate void BulletCollision(Bullet _bullet);
    public event BulletCollision OnBulletCollsion;

    public Bullet(GameObject thisGameObject, bool active)
    {
        this.thisGameObject = thisGameObject;
        Active = active;
    }

    public void DisablePoolabe()
    {
        this.Active = false;
    }

    public void EnablePoolabe()
    {
        this.Active = true;
    }

    public void SetPosition(Vector2 _pos)
    {
        thisGameObject.transform.position = _pos;    
    }

    public void OnUpdate()
    {

    }
}
