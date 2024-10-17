using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private int currentTarget = 0;
    public EnemyIdleState(Enemy _enemy) : base(_enemy)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy Entering Idle");

        if (enemy.idlePoints.Length > 0)
        {
            enemy.navAgent.destination = enemy.idlePoints[currentTarget].position;
        }
    }

    public override void OnStateLeave()
    {
        Debug.Log("Enemy Leaving Idle");
    }

    public override void OnStateUpdate()
    {

        if (enemy == null || enemy.idlePoints.Length <= 0) return;

        if (enemy.navAgent.remainingDistance < 0.8f)
        {
            currentTarget++;
            if (currentTarget >= enemy.idlePoints.Length)
            {
                currentTarget = 0;
            }
            enemy.navAgent.destination = enemy.idlePoints[currentTarget].position;
        }
    }
}
