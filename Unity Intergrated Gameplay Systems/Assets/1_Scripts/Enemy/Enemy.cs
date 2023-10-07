using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IDamagable, IFixedUpdateable, IInstantiatable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }
    public StateMachine<Enemy> movementFSM { get; private set; }
    public GameSettings GameSettings { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public Enemy(GameObject _prefab, Transform _parent, GameSettings _gameSettings)
    {
        GameSettings = _gameSettings;

        Instantiate(_prefab, _parent);
        Rigidbody = Instance.GetComponent<Rigidbody2D>();

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
    }

    public void TakeDamage(int amount)
    {

    }

    public void Die()
    {

    }
}
