using UnityEngine;

// default idle state of enemy
public class IdleStateEnemy : BaseStateEnemy
{
    float aggroDistance;

    public IdleStateEnemy(float _aggroDistance)
    {
        aggroDistance = _aggroDistance;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Idle State");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        // starting running to original position
        // if no player stop moving and wait for player to get into range
        // so do nothing, do idle anim
        enemy.MoveOriginalPosition();


        //Debug.Log("Enter Idle Update");

        // if enemy is in range for aggro
        if (enemy.RayCastCheck(aggroDistance))
            enemy.SwitchState(enemy.aggroState);
    }
}