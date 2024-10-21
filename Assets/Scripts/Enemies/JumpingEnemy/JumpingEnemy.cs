using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class JumpingEnemy : MonoBehaviour
{
    public JEnemyState currentState;
    [HideInInspector] public Rigidbody rb;

    public Transform target;

    [Header("Jump Attributes")]
    public Vector3 jumpPoint;
    public Vector3 curvePoint;
    public float jumpHeight;
    public float jumpDistance;
    public float speed;

    [Header("Attack Attributes")]
    public float damage;
    public float attackRange;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentState = new JEnemyIdleState(this);
        currentState.OnStateEnter();
    }

    void Update()
    {
        if (target != null) transform.LookAt(new Vector3(target.position.x,
                                       transform.position.y,
                                       target.position.z));
        currentState.OnStateUpdate();
    }

    public void ChangeState(JEnemyState state)
    {
        currentState.OnStateLeave();
        currentState = state;
        currentState.OnStateEnter();
    }
}
