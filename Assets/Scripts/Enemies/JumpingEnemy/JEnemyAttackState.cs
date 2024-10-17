using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JEnemyAttackState : JEnemyState
{
    private float damage;
    private float attackTime;

    public JEnemyAttackState(JumpingEnemy enemy) : base(enemy)
    {
        
    }

    public override void OnStateEnter()
    {
        this.damage = enemy.damage;
        attackTime = 0;
        Attack();
    }

    public override void OnStateLeave()
    {

    }

    public override void OnStateUpdate()
    {
        attackTime += Time.deltaTime;

        //Returns enemy back to idle state after attack 'animation' is finished
        if (attackTime > 1.0f) enemy.ChangeState(new JEnemyIdleState(enemy));
    }

    void Attack()
    {
        if(Vector3.Distance(enemy.transform.position, enemy.target.position) <= enemy.attackRange)
        {
            Debug.Log($"Attacked Player for {damage}");
            //Insert damage function for player
        }

    }
}
