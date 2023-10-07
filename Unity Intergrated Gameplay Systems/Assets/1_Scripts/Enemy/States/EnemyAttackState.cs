using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyAttackState : IState<Enemy>
{
    public Enemy StateOwner { get; set; }
    private GameObject player;
    private float speed;

    public void OnStart()
    {
        speed = StateOwner.GameSettings.EnemySpeed;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(StateOwner.Instance.transform.position, StateOwner.GameSettings.EnemyAttackRange);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
            {
                player = collider.gameObject;
            }
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        Vector2 direction = (player.transform.position - StateOwner.Instance.transform.position).normalized;
        StateOwner.Rigidbody.AddForce(direction * speed);
    }

    public void SetOwner(Enemy _stateOwner)
    {
        StateOwner = _stateOwner;
    }
}
