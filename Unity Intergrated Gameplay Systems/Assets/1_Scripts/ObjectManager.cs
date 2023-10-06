using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IUpdateable, IFixedUpdateable
{
    private GameObject playerPrefab;
    private GameObject bulletPrefab;
    private ObjectPool<Bullet> bulletPool;
    [SerializeField] private int maxBulletAmount;


    public ObjectManager(GameObject _player, GameObject _bullet)
    {
        playerPrefab = _player;
        bulletPrefab = _bullet;

        bulletPool = new ObjectPool<Bullet>();
        playerPrefab.GetComponent<PlayerData>().OnFireGun += HandleFireGun;

        for (int i = 0; i < maxBulletAmount; i++)
        {
            GameObject newBullet = Object.Instantiate(bulletPrefab);
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
