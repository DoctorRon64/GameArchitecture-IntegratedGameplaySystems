using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IUpdateable, IFixedUpdateable
{
    private int maxBulletAmount = 10;
    private ObjectPool<Bullet> bulletPool;
    private BulletFactory bulletFactory;



    public ObjectManager()
    {
        bulletFactory = new BulletFactory();
        bulletPool = new ObjectPool<Bullet>();

        for (int i = 0; i < maxBulletAmount; i++)
        {
            Bullet newBullet = bulletFactory.Create("Bullet");
            bulletPool.DeactivateItem(newBullet);
            newBullet.OnCollision += OnBulletCollsion;
        }
    }

    public void OnFixedUpdate()
    {


        bulletPool.UpdateItem();
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bulletPool.RequestObject(Vector2.zero);
        }
    }

    public void OnBulletCollsion(Collider2D other)
    {
        Debug.Log(other.gameObject + "collides");

        IDamagable Damagable = other.GetComponent<IDamagable>();

        if (Damagable != null)
        {
            Damagable.TakeDamage(100);
        }
    }

    //private void HandleFireGun()
    //{
    //    bulletPool.RequestObject(playerPrefab.transform.position);
    //}

    //public void OnUpdate()
    //{
    //    playerPrefab.GetComponent<PlayerData>().OnUpdate();
    //    bulletPool.UpdateItem();
    //}

    //public void OnFixedUpdate()
    //{
    //    playerPrefab.GetComponent<PlayerData>().OnFixedUpdate();
    //}
}
