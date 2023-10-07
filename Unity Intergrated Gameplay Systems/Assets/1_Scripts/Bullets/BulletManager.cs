using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletManager : IUpdateable, IFixedUpdateable
{
    private int bulletPoolSize;
    private ObjectPool<Bullet> bulletPool;
    private BulletFactory bulletFactory;

    //References
    private GridManager gridManager;
    private GameSettings gameSettings;

    public BulletManager(GridManager _gridManager, GameSettings _gameSettings)
    {
        bulletFactory = new BulletFactory();
        bulletPool = new ObjectPool<Bullet>();
        gridManager = _gridManager;
        gameSettings = _gameSettings;
        bulletPoolSize = gameSettings.BulletPoolSize;

        if (bulletPoolSize <= 0)
        {
            Debug.LogError("BulletPoolSize is too small");
        }

        for (int i = 0; i < bulletPoolSize; i++)
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
        
    }

    public void FireBulletOutofObjectPool(Vector2 _pos)
    {
        Bullet bullet = bulletPool.RequestObject(_pos);
        bullet.OnCollision += OnBulletCollision;
    }

    public void OnBulletCollision(Collider2D other, Bullet _instance)
    {
        //If Other is a Tile
        if (other.gameObject.layer == LayerMask.NameToLayer("TileLayer"))
        {
            Vector2Int tilePos = new Vector2Int((int)other.gameObject.transform.position.x, -(int)other.gameObject.transform.position.y);
            Tile otherTile = gridManager.GetTile(tilePos);

            if (otherTile != null)
            {
                otherTile.TakeDamage(gameSettings.BulletDamage);
            }
            else
            {
                Debug.LogError("BulletManager can't find tile in GridManager");
            }
        }

        bulletPool.DeactivateItem(_instance);
        _instance.OnCollision -= OnBulletCollision;
    }
}
