using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamagable, IFixedUpdateable, IInstantiatable, ICollidable<Enemy>
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }
    public StateMachine<Enemy> movementFSM { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public GameSettings GameSettings { get; private set; }
    public Collider2D collider { get; set; }
    public Action<Collider2D, Enemy> OnCollision { get; set; }

    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied OnDied;


    public Enemy(GameObject _prefab, Transform _parent, GameSettings _gameSettings)
    {
        GameSettings = _gameSettings;

        Instantiate(_prefab, _parent);
        Instance.layer = LayerMask.NameToLayer("EnemyLayer");
        Rigidbody = Instance.GetComponent<Rigidbody2D>();
        collider = Instance.GetComponent<Collider2D>();

        MaxHealth = GameSettings.EnemyHealth;
        Health = MaxHealth;

        //FSM
        movementFSM = new StateMachine<Enemy>(this);
        movementFSM.AddState("EnemyIdleState", new EnemyIdleState());
        movementFSM.AddState("EnemyAttackState", new EnemyAttackState());
        movementFSM.SwitchState("EnemyIdleState");
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    public void OnFixedUpdate()
    {
        movementFSM.OnUpdate();
        CheckCollisions();
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDied(this);
    }

    public void CheckCollisions()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(collider.bounds.center, 1);

        foreach (Collider2D otherCollider in colliders)
        {
            if (otherCollider != collider)
            {
                OnCollision?.Invoke(otherCollider, this);
            }
        }
    }
}
