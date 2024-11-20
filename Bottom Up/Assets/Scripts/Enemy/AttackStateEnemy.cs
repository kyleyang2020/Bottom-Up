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
        // NOT DONE
        // "plants their feet and only rotates from their position to aim and shoot projectiles at the player character"
        // stop moving then rotates to face player and shoot projectile at them
        // projectile travels straight and does some damage
        //Debug.Log("enemy stopped, shoot now");

        if (enemy.DetectPlayer())
            enemy.ShootBullet();
        else
            enemy.LookAtPlayer();


        //Debug.Log("Enter Attack Update");

        // if enemy is out of range for attack
        if (!(enemy.RayCastCheck(attackDistance)))
        {
            enemy.SwitchState(enemy.aggroState);
        }
    }
}