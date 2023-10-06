using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : IPoolable, IInstantiatable
{
    public bool Active { get; set; }
    public GameObject Instance { get; set; }

    public Bullet(GameObject prefab, Transform _parent)
    {
        //Instance
        Instance = GameObject.Instantiate(prefab);

        //Parent
        Instance = new GameObject();
        Instance.transform.SetParent(_parent);
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
