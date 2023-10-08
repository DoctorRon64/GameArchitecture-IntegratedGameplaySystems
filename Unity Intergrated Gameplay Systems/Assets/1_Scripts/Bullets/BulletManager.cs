using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletManager : IFixedUpdateable
{
    private int bulletPoolSize;
    private ObjectPool<Bullet> bulletPool;
    private BulletFactory bulletFactory;

    //References
    private GridManager gridManager;
    private GameSettings gameSettings;
    private EnemyManager enemyManager;

    public BulletManager(GridManager _gridManager, GameSettings _gameSettings, EnemyManager _enemyManager)
    {
        gridManager = _gridManager;
        gameSettings = _gameSettings;
        enemyManager = _enemyManager;

        bulletFactory = new BulletFactory(_gameSettings);
        bulletPool = new ObjectPool<Bullet>();
        bulletPoolSize = gameSettings.BulletPoolSize;

        if (bulletPoolSize <= 0)
        {
            Debug.LogError("BulletPoolSize is zero");
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

    public void FireBullet(Vector2 _pos, Vector2 _direction)
    {
        Bullet bullet = bulletPool.RequestObject(_pos);
        bullet.FireBullet(_direction);
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

        //If Other is a Enemy
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            Enemy currentEnemy = enemyManager.GetEnemy(other.gameObject);
            currentEnemy.TakeDamage(gameSettings.BulletDamage);
        }

        //If Other is a Bullet
        if (other.gameObject.layer != LayerMask.NameToLayer("Bullet"))
        {
            bulletPool.DeactivateItem(_instance);
            _instance.OnCollision -= OnBulletCollision;
        }
    }
}
