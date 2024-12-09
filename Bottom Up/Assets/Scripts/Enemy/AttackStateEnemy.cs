using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateEnemy : BaseStateEnemy
{
    float attackDistance;

    public Animator animator;

    public AttackStateEnemy(float _attackDistance)
    {
        attackDistance = _attackDistance;
    }
    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Attack State");
        enemy.StopPosition();

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Attack Update");
        if (enemy.DetectPlayer())
            Debug.Log("hit player");
        else
            enemy.LookAtPlayer();

        // if enemy is out of range for attack
        if (!(enemy.RayCastCheck(attackDistance)))
        {
            enemy.SwitchState(enemy.aggroState);
        }
    }
}