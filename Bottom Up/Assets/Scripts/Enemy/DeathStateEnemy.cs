using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateEnemy : BaseStateEnemy
{
    public float disapearTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        disapearTimer = enemy.deleteTimer;

        // drop a number of hp/blood to player
        // grants temp speed boost to player
        // if the enemy is stunned when killed, drop more hp/blood
        // UNDONE
        //enemy.DeleteOnDeath();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        disapearTimer -= Time.deltaTime;
        if (disapearTimer <= 0)
        {
            Delete(enemy);
        }
    }

    private void Delete(EnemyStateManager enemy)
    {
        enemy.DeleteOnDeath();
    }
}