using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SEnemyActiveState : SEnemyState
{
    private float rotation;
    private float timer;

    public SEnemyActiveState(StationaryEnemy enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        rotation = 360.0f / enemy.projectiles.Count;
    }

    public override void OnStateLeave()
    {

    }

    public override void OnStateUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            Attack();
            timer = 0;
            enemy.StartCoroutine(ReturnProjectilesCoroutine());
        }
    }

    private void Attack()
    {
        foreach (Rigidbody proj in enemy.projectiles)
        {
            proj.transform.position = enemy.eye.position;
            proj.gameObject.SetActive(true);
            proj.AddForce(enemy.eye.forward * enemy.distance, ForceMode.Impulse);

            enemy.eye.Rotate(0, rotation, 0);
        }
    }

    IEnumerator ReturnProjectilesCoroutine()
    {
        Debug.Log("Projectiles returning");
        yield return new WaitForSeconds(1);

        foreach (Rigidbody proj in enemy.projectiles)
        {
            proj.velocity = Vector3.zero;
            proj.transform.position = enemy.eye.position;
            proj.gameObject.SetActive(false);
        }
    }
}
