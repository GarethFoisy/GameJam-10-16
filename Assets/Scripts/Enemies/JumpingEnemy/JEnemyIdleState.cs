using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JEnemyIdleState : JEnemyState
{
    private float rotation;
    private float detectionDistance = 10f;
    public JEnemyIdleState(JumpingEnemy enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateLeave()
    {

    }

    public override void OnStateUpdate()
    {

        if(enemy.target != null)
        {
            enemy.ChangeState(new JEnemyMovingState(enemy));
        }

        //360 scan for a player
        rotation += Time.deltaTime * 720;
        if(rotation >= 360) rotation = 0;

        Ray ray = new Ray(enemy.transform.position, Quaternion.Euler(0, rotation, 0) * enemy.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, detectionDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                enemy.target = hit.transform;
            }
        }
    }
}
