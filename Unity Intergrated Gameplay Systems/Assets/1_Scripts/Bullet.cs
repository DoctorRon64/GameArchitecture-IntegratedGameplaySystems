using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : IPoolable, IInstantiatable
{
    public bool Active { get; set; }
    public GameObject Instance { get; set; }

    public Bullet(Sprite _sprite, Transform _parent)
    {
        //Instance
        Instance = new GameObject();
        Instance.transform.SetParent(_parent);

        //Renderer
        SpriteRenderer renderer = Instance.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite;
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
