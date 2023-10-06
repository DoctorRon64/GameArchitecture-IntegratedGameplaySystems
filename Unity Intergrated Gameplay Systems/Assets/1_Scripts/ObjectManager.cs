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

            Debug.Log(newBullet);
        }
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
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
