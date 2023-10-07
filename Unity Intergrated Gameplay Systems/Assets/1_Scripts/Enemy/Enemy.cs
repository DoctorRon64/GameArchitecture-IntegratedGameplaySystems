using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamagable, IFixedUpdateable, IInstantiatable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }
    public StateMachine<Enemy> fsm { get; private set; }
    public GameSettings GameSettings { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public Enemy(GameObject _prefab, Transform _parent, GameSettings _gameSettings)
    {
        GameSettings = _gameSettings;

        Instantiate(_prefab, _parent);
        Rigidbody = Instance.GetComponent<Rigidbody2D>();

        //FSM
        fsm = new StateMachine<Enemy>(this);
        fsm.AddState("EnemyIdleState", new EnemyIdleState());
        fsm.AddState("EnemyAttackState", new EnemyAttackState());
        fsm.SwitchState("EnemyIdleState");
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    public void OnFixedUpdate()
    {
        fsm.OnUpdate();
    }

    public void TakeDamage(int amount)
    {

    }

    public void Die()
    {

    }
}
