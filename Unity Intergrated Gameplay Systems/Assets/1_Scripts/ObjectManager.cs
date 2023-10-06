using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IUpdateable, IFixedUpdateable
{
    private GameObject playerPrefab;
    private ObjectPool<Bullet> bulletPool;
    [SerializeField] private int maxBulletAmount;
    private BulletFactory bulletFactory;

    public ObjectManager(GameObject _player)
    {
        bulletFactory = new BulletFactory();

        playerPrefab = _player;

        bulletPool = new ObjectPool<Bullet>();
        playerPrefab.GetComponent<PlayerData>().OnFireGun += HandleFireGun;

        for (int i = 0; i < maxBulletAmount; i++)
        {
            GameObject newBullet = bulletFactory.Create("Bullet");
            bulletPool.DeactivateItem(newBullet.GetComponent<Bullet>());
        }
    }

    private void HandleFireGun()
    {
        bulletPool.RequestObject(playerPrefab.transform.position);
    }

    public void OnUpdate()
    {
        playerPrefab.GetComponent<PlayerData>().OnUpdate();
        bulletPool.UpdateItem();
    }

    public void OnFixedUpdate()
    {
        playerPrefab.GetComponent<PlayerData>().OnFixedUpdate();
    }
}
