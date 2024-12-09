using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedStateEnemy : BaseStateEnemy
{
    private Rigidbody2D rb;
    private float knockbackDuration;
    private float elapsedTime;
    private Vector2 knockbackForce;

    public DamagedStateEnemy(Rigidbody2D rb, float knockbackDuration, Vector2 knockbackForce)
    {
        this.rb = rb;
        this.knockbackDuration = knockbackDuration;
        this.knockbackForce = knockbackForce;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Damaged State");
        elapsedTime = 0f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //Debug.Log("Enter Damaged Update");
        elapsedTime += Time.deltaTime;
        enemy.StopPosition();

        if (elapsedTime >= knockbackDuration)
        {
            // Transition to the next state (e.g., IdleState)
            enemy.SwitchState(enemy.idleState);
        }
    }
}
