using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroStateEnemy : BaseStateEnemy
{
    public float deaggroDistance;
    public float attackDistance;

    public Animator animator;

    public AggroStateEnemy(float _deaggroDistance, float _attackDistance)
    {
        deaggroDistance = _deaggroDistance;
        attackDistance = _attackDistance;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Aggro State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Aggro Update");

        // moves towards player until in/out of range
        enemy.MoveTowardsPlayer();

        // if enemy is out of deaggro range
        if (!(enemy.RayCastCheck(deaggroDistance)))
        {
            enemy.Idle(); 
        }
        // if enemy is in range for attack
        if (enemy.RayCastCheck(attackDistance))
        {
            enemy.SwitchState(enemy.attackState);
        }
    }
}