using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    public Enemy StateOwner { get; set; }

    public void OnStart()
    {

    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(StateOwner.Instance.transform.position, StateOwner.GameSettings.EnemyAttackRange);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
            {
                StateOwner.movementFSM.SwitchState("EnemyAttackState");
            }
        }
    }

    public void SetOwner(Enemy _stateOwner)
    {
        StateOwner = _stateOwner;
    }
}
