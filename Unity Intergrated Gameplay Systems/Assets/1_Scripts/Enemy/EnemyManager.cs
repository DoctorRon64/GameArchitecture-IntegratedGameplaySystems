using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IFixedUpdateable
{
    private EnemyFactory enemyFactory;
    private List<Enemy> enemies;
    private List<Enemy> enemiesToDelete;

    //References
    private PlayerManager playerManager;
    private GameSettings gameSettings;

    public EnemyManager(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings;

        enemyFactory = new EnemyFactory(_gameSettings);
        enemies = new List<Enemy>();
        enemiesToDelete = new List<Enemy>();
    }

    public void OnFixedUpdate()
    {
        foreach (Enemy enemy in enemiesToDelete)
        {
            enemies.Remove(enemy);
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.OnFixedUpdate();
        }
    }

    public Enemy GetEnemy(GameObject _instance)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].Instance == _instance)
            {
                return enemies[i];
            }
        }

        Debug.LogError("EnemyManager Couldn't find Enemy");
        return null;
    }

    public void AddPlayerManager(PlayerManager _playerManager)
    {
        playerManager = _playerManager;
    }

    public void AddEnemy(string _enemyType, Vector2 _pos)
    {
        Enemy newEnemy = enemyFactory.Create(_enemyType);
        newEnemy.Instance.transform.position = new Vector3(_pos.x, -_pos.y, 0);
        enemies.Add(newEnemy);
        newEnemy.OnDied += RemoveEnemy;
        newEnemy.OnCollision += OnEnemyCollision;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemiesToDelete.Add(enemy);
        enemy.OnDied -= RemoveEnemy;
        enemy.OnCollision -= OnEnemyCollision;
        GameObject.Destroy(enemy.Instance);
        enemy = null;
    }
    public void OnEnemyCollision(Collider2D other, Enemy _instance)
    {
        //If Other is a Player
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            Player player = playerManager.GetPlayer();
            _instance.OnCollision -= OnEnemyCollision;
            player.TakeDamage(gameSettings.EnemyDamage);
            _instance.Die();
        }

    }

}
