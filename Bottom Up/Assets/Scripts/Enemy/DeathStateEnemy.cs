using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateEnemy : BaseStateEnemy
{
    public float disappearTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        disappearTimer = enemy.deleteTimer;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            Delete(enemy);
        }
    }

    private void Delete(EnemyStateManager enemy)
    {
        enemy.DeleteOnDeath();
    }
}