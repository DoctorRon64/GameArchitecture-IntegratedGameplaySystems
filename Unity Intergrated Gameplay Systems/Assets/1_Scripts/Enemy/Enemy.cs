using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamagable, IFixedUpdateable, IInstantiatable, ICollidable<Enemy>
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }
    public Collider2D collider { get; set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public StateMachine<Enemy> movementFSM { get; private set; }

    //Events
    public delegate void EnemyDied(Enemy enemy);
    public event EnemyDied OnDied;
    public Action<Collider2D, Enemy> OnCollision { get; set; }

    //References
    public GameSettings GameSettings { get; private set; }

    public Enemy(GameObject _prefab, Transform _parent, GameSettings _gameSettings)
    {
        GameSettings = _gameSettings;
        MaxHealth = GameSettings.EnemyHealth;
        Health = MaxHealth;

        //Instance
        Instantiate(_prefab, _parent);
        Instance.layer = LayerMask.NameToLayer("EnemyLayer");
        Rigidbody = Instance.GetComponent<Rigidbody2D>();
        collider = Instance.GetComponent<Collider2D>();

        //FSM
        movementFSM = new StateMachine<Enemy>(this);
        movementFSM.AddState("EnemyIdleState", new EnemyIdleState());
        movementFSM.AddState("EnemyAttackState", new EnemyAttackState());
        movementFSM.SwitchState("EnemyIdleState");
    }

    public void OnFixedUpdate()
    {
        movementFSM.OnUpdate();
        CheckCollisions();
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    public void TakeDamage(int _amount)
    {
        Health -= _amount;

        if (Health <= 0)
        {
            OnDie();
        }
    }

    public void OnDie()
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
