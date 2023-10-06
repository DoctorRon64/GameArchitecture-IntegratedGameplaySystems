using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : IUpdateable, IFixedUpdateable
{
    private int maxBulletAmount = 10;
    private ObjectPool<Bullet> bulletPool;
    private BulletFactory bulletFactory;

    public BulletManager()
    {
        bulletFactory = new BulletFactory();
        bulletPool = new ObjectPool<Bullet>();

        for (int i = 0; i < maxBulletAmount; i++)
        {
            Bullet newBullet = bulletFactory.Create("Bullet");
            bulletPool.DeactivateItem(newBullet);
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
            Bullet bullet = bulletPool.RequestObject(Vector2.zero);
            bullet.OnCollision += OnBulletCollsion;
        }
    }

    public  void OnBulletCollsion(Collider2D other, Bullet _instance)
    {
        Debug.Log(other.gameObject + "collides");

        IDamagable Damagable = other.GetComponent<IDamagable>();

        if (Damagable != null)
        {
            Damagable.TakeDamage(100);
        }

        bulletPool.DeactivateItem(_instance);
        _instance.OnCollision -= OnBulletCollsion;
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
