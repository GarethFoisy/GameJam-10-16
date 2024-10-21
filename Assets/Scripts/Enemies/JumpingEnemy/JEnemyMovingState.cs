using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JEnemyMovingState : JEnemyState
{
    private float timer;
    private float speed;
    private Vector3 startpos;

    private Vector3 jumpAreaA, jumpAreaB;
    public JEnemyMovingState(JumpingEnemy enemy) : base(enemy)
    {

    }

    public override void OnStateEnter()
    {
        timer = 0;
        SetJumpRange();
        SetJumpPoints();
    }

    public override void OnStateLeave()
    {

    }

    public override void OnStateUpdate()
    {
        //Slows speed down close to apex of jump arc
        speed = (Mathf.Abs(timer - 0.5f) + 1) * enemy.speed;

        if (timer < 1)
        {
            timer += Time.deltaTime * speed;
        }
        else
        {
            timer = 1;
        }

        if(timer >= 1)
        {
            //After finishing the jump, the enemy attacks
            enemy.ChangeState(new JEnemyAttackState(enemy));
        }

        Jump();
    }

    void Jump()
    {
        if (enemy.jumpPoint == null || startpos == null) return;

        //Calculates Bezier curve for jump
        Vector3 posAC = Vector3.Lerp(startpos, enemy.curvePoint, timer);
        Vector3 posCB = Vector3.Lerp(enemy.curvePoint, enemy.jumpPoint, timer);

        //Find final position of vertex
        Vector3 finalVertexPos = Vector3.Lerp(posAC, posCB, timer);

        //Set position of vertex
        enemy.transform.position = finalVertexPos;
    }

    void SetJumpPoints()
    {
        if(Vector3.Distance(enemy.transform.position, enemy.target.position) <= enemy.jumpDistance)
        {
            enemy.jumpPoint = enemy.target.position;
        }
        else
        {
            enemy.jumpPoint = Vector3.Lerp(jumpAreaA, jumpAreaB, Random.value);
        }

        enemy.curvePoint = 
            Vector3.Lerp(enemy.transform.position, enemy.jumpPoint, 0.5f) 
            + new Vector3 (0, enemy.jumpHeight, 0);
    }

    void SetJumpRange()
    {
        //Sets 2 points, the enemy can jump anywhere between these 2 points
        startpos = enemy.transform.position;

        Vector3 pointA = enemy.transform.position + new Vector3(1.5f, 0, 0) + enemy.transform.forward * enemy.jumpDistance;
        Vector3 pointB = enemy.transform.position + new Vector3(-1.5f, 0, 0) + enemy.transform.forward * enemy.jumpDistance;

        jumpAreaA = new Vector3(pointA.x, enemy.transform.position.y, pointA.z);
        jumpAreaB = new Vector3(pointB.x, enemy.transform.position.y, pointB.z);
    }
}
