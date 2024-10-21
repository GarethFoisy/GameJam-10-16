using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;

public class StationaryEnemy : MonoBehaviour
{
    public SEnemyState currentState;

    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private Rigidbody projPrefab;
    public List<Rigidbody> projectiles = new List<Rigidbody>();
    public Transform eye;

    public float distance;
    public float attackSpeed;
    public float damage;

    void Start()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Rigidbody tempProj = Instantiate(projPrefab, gameObject.transform);
            tempProj.gameObject.SetActive(false);
            tempProj.position = eye.position;
            projectiles.Add(tempProj);
        }
        currentState = new SEnemyActiveState(this);
        currentState.OnStateEnter();
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(SEnemyState state)
    {
        currentState.OnStateLeave();
        currentState = state;
        currentState.OnStateLeave();
    }
}
