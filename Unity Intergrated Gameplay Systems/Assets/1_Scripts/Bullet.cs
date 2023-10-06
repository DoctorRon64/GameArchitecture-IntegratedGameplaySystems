using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : IPoolable
{
    public bool Active { get; set; }   

    public Bullet()
    {

    }

    public void DisablePoolabe()
    {
        this.Active = false;
    }

    public void EnablePoolabe()
    {
        this.Active = true;
    }

    public void FireBullet()
    {

    }
}
